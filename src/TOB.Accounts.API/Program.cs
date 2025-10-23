using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TOB.Accounts.API.Exceptions;
using TOB.Accounts.API.Telemetry;
using TOB.Accounts.Domain.AppSettings;
using TOB.Accounts.Infrastructure.Data;
using TOB.Accounts.Infrastructure.Repositories;
using TOB.Accounts.Infrastructure.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Configure Azure Key Vault (only in non-development environments)
if (!builder.Environment.IsDevelopment())
{
    var keyVaultUri = builder.Configuration.GetSection("KeyVault:VaultUri").Value;

    if (!string.IsNullOrWhiteSpace(keyVaultUri))
    {
        builder.Configuration.AddAzureKeyVault(
            new Uri(keyVaultUri),
            new DefaultAzureCredential());
    }
}

// Configure Options Pattern with validation
builder.Services.AddOptions<ConnectionStringsOptions>()
    .Bind(builder.Configuration.GetSection(ConnectionStringsOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<AzureAdOptions>()
    .Bind(builder.Configuration.GetSection(AzureAdOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<OpenTelemetryOptions>()
    .Bind(builder.Configuration.GetSection(OpenTelemetryOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<CorsOptions>()
    .Bind(builder.Configuration.GetSection(CorsOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<KeyVaultOptions>()
    .Bind(builder.Configuration.GetSection(KeyVaultOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Get configuration options
var otelOptions = builder.Configuration.GetSection(OpenTelemetryOptions.SectionName).Get<OpenTelemetryOptions>()!;
var corsOptions = builder.Configuration.GetSection(CorsOptions.SectionName).Get<CorsOptions>()!;

// Configure OpenTelemetry
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(otelOptions.ServiceName, serviceVersion: otelOptions.ServiceVersion)
        .AddAttributes(new Dictionary<string, object>
        {
            ["deployment.environment"] = builder.Environment.EnvironmentName,
            ["host.name"] = Environment.MachineName
        }))
    .WithTracing(tracing =>
    {
        if (!otelOptions.EnableTracing) return;

        tracing
            .AddAspNetCoreInstrumentation(options =>
            {
                options.RecordException = true;
                options.EnrichWithHttpRequest = (activity, httpRequest) =>
                {
                    activity.SetTag("http.request.user_agent", httpRequest.Headers.UserAgent.ToString());
                };
                options.EnrichWithHttpResponse = (activity, httpResponse) =>
                {
                    activity.SetTag("http.response.content_length", httpResponse.ContentLength);
                };
            })
            .AddHttpClientInstrumentation(options =>
            {
                options.RecordException = true;
                options.EnrichWithHttpRequestMessage = (activity, httpRequestMessage) =>
                {
                    activity.SetTag("http.request.method", httpRequestMessage.Method.Method);
                };
            })
            .AddEntityFrameworkCoreInstrumentation(options =>
            {
                options.SetDbStatementForText = true;
                options.SetDbStatementForStoredProcedure = true;
                options.EnrichWithIDbCommand = (activity, command) =>
                {
                    activity.SetTag("db.query.parameters", command.Parameters.Count);
                };
            })
            .AddSource(Diagnostics.ActivitySource.Name);

        if (otelOptions.UseConsoleExporter)
        {
            tracing.AddConsoleExporter();
        }
        else
        {
            tracing.AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(otelOptions.OtlpEndpoint);
            });
        }
    })
    .WithMetrics(metrics =>
    {
        if (!otelOptions.EnableMetrics) return;

        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddMeter(Diagnostics.Meter.Name);

        if (otelOptions.UseConsoleExporter)
        {
            metrics.AddConsoleExporter();
        }
        else
        {
            metrics.AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(otelOptions.OtlpEndpoint);
            });
        }
    });

// Configure OpenTelemetry Logging
if (otelOptions.EnableLogging)
{
    builder.Logging.AddOpenTelemetry(logging =>
    {
        logging.IncludeFormattedMessage = true;
        logging.IncludeScopes = true;

        if (otelOptions.UseConsoleExporter)
        {
            logging.AddConsoleExporter();
        }
        else
        {
            logging.AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(otelOptions.OtlpEndpoint);
            });
        }
    });
}

// Add Global Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsOptions.DefaultPolicyName, policy =>
    {
        var allowedOrigins = corsOptions.GetAllowedOrigins();

        if (allowedOrigins.Length > 0)
        {
            policy.WithOrigins(allowedOrigins);
        }
        else
        {
            policy.AllowAnyOrigin();
        }

        var allowedMethods = corsOptions.GetAllowedMethods();
        if (allowedMethods != null && allowedMethods.Length > 0)
        {
            policy.WithMethods(allowedMethods);
        }
        else
        {
            policy.AllowAnyMethod();
        }

        var allowedHeaders = corsOptions.GetAllowedHeaders();
        if (allowedHeaders != null && allowedHeaders.Length > 0)
        {
            policy.WithHeaders(allowedHeaders);
        }
        else
        {
            policy.AllowAnyHeader();
        }

        if (corsOptions.AllowCredentials)
        {
            policy.AllowCredentials();
        }

        var exposedHeaders = corsOptions.GetExposedHeaders();
        if (exposedHeaders != null && exposedHeaders.Length > 0)
        {
            policy.WithExposedHeaders(exposedHeaders);
        }

        policy.SetPreflightMaxAge(TimeSpan.FromSeconds(corsOptions.MaxAge));
    });
});

// Add DbContext using Options pattern
builder.Services.AddDbContext<AccountsDbContext>((serviceProvider, options) =>
{
    var connectionStringsOptions = serviceProvider.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;
    options.UseSqlServer(connectionStringsOptions.AccountDbConnection);
});

// Register repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// Add MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(TOB.Accounts.Services.Handlers.CommandHandlers.CreateAccountCommandHandler).Assembly));

// Add Microsoft Entra External ID authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization();

// Add controllers
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure Swagger/OpenAPI with JWT Bearer authentication
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TOB Accounts API",
        Version = "v1",
        Description = "API for managing accounts and contacts"
    });

    // Add JWT Bearer authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token from Microsoft Entra External ID"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TOB Accounts API v1");
    });
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors(CorsOptions.DefaultPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
