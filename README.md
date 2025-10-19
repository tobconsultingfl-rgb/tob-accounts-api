# TOB Accounts API

A multi-tenant accounts and contacts management API built with .NET 9, following clean architecture principles with CQRS pattern, secured with Microsoft Entra External ID.

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Security](#security)
- [Deployment](#deployment)
- [Project Structure](#project-structure)
- [Development](#development)
- [Testing](#testing)
- [Contributing](#contributing)

## Overview

The TOB Accounts API is a secure, multi-tenant REST API for managing accounts and contacts. It implements enterprise-grade security with role-based access control, comprehensive observability with OpenTelemetry, and follows clean architecture patterns for maintainability and testability.

### Key Capabilities

- **Multi-Tenant Architecture**: Isolated data access per tenant with Super Admin cross-tenant access
- **CQRS Pattern**: Separation of read and write operations using MediatR
- **Soft Deletes**: Audit-friendly data retention with IsActive flags
- **Role-Based Access Control**: Tenant-isolated regular users and cross-tenant Super Admin role
- **Comprehensive Observability**: Distributed tracing, metrics, and logging with OpenTelemetry
- **Secure by Default**: Microsoft Entra External ID authentication with JWT Bearer tokens

## Architecture

This solution follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────┐
│                    TOB.Accounts.API                      │
│         (Controllers, Middleware, Configuration)         │
└─────────────────────────────────────────────────────────┘
                          │
          ┌───────────────┴───────────────┐
          │                               │
┌─────────▼──────────┐         ┌─────────▼──────────────┐
│ TOB.Accounts.      │         │ TOB.Accounts.          │
│ Services           │         │ Infrastructure         │
│ (Command/Query     │         │ (Data, Repositories,   │
│  Handlers)         │         │  EF Core)              │
└─────────┬──────────┘         └─────────┬──────────────┘
          │                               │
          └───────────────┬───────────────┘
                          │
                ┌─────────▼──────────┐
                │ TOB.Accounts.      │
                │ Domain             │
                │ (Entities, DTOs,   │
                │  Commands, Queries)│
                └────────────────────┘
```

### Layer Responsibilities

- **API Layer**: HTTP endpoints, authentication, exception handling, telemetry
- **Services Layer**: Business logic orchestration via CQRS handlers
- **Infrastructure Layer**: Data access, external services, repositories
- **Domain Layer**: Core business entities, DTOs, contracts (no dependencies)

## Features

### Multi-Tenancy
- Tenant isolation enforced at the data access layer
- Tenant ID extracted from JWT claims (`extension_TenantId`)
- Super Admin role can access all tenants
- Regular users restricted to their own tenant

### CRUD Operations
- **Accounts**: Create, Read, Update, Delete (soft delete)
- **Contacts**: Create, Read, Update, Delete (soft delete) - nested under accounts
- One-to-many relationship (Account → Contacts)
- RESTful nested routes: `/api/accounts/{accountId}/contacts`

### Security Features
- Microsoft Entra External ID authentication
- JWT Bearer token validation
- Role-based authorization (regular users, Super Admin)
- CORS support with configurable origins
- HTTPS enforcement
- Secrets stored in Azure Key Vault

### Observability
- **Distributed Tracing**: ASP.NET Core, HTTP Client, Entity Framework Core
- **Metrics**: Request rates, durations, custom business metrics
- **Logging**: Structured logging with OpenTelemetry
- **Application Insights**: Integration for Azure monitoring
- **OTLP Export**: Compatible with any OpenTelemetry collector

### Data Management
- Soft deletes with `IsActive` flag
- Audit fields: `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`
- Entity Framework Core with SQL Server
- Code-first migrations
- Global query filters for soft deletes

## Technologies

### Core Framework
- **.NET 9** - Latest LTS version
- **ASP.NET Core 9** - Web API framework
- **C# 13** - Latest language features

### Authentication & Authorization
- **Microsoft.Identity.Web** - Microsoft Entra External ID integration
- **JWT Bearer Authentication** - Token-based security

### Data Access
- **Entity Framework Core 9** - ORM
- **SQL Server** - Relational database
- **Azure SQL** - Cloud database (production)

### CQRS & Mediator
- **MediatR 13.0** - Command/Query pattern implementation

### Observability
- **OpenTelemetry 1.13+** - Distributed tracing, metrics, logging
- **Application Insights** - Azure monitoring (production)

### Azure Services
- **Azure App Service** - Hosting platform
- **Azure Key Vault** - Secrets management
- **Azure SQL Database** - Managed database
- **Azure Application Insights** - APM and monitoring

### API Documentation
- **Swashbuckle** - Swagger/OpenAPI documentation
- **Microsoft.AspNetCore.OpenApi** - OpenAPI support

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server) or [SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)
- [Azure subscription](https://azure.microsoft.com/free/) (for Entra ID and production deployment)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-org/tob-accounts-api.git
   cd tob-accounts-api
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore src/TOB.Accounts.sln
   ```

3. **Update configuration**

   Edit `src/TOB.Accounts.API/appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TOBAccountsDb;Trusted_Connection=True;"
     },
     "AzureAd": {
       "Instance": "https://login.microsoftonline.com/",
       "TenantId": "your-tenant-id",
       "ClientId": "your-client-id",
       "Audience": "your-api-client-id"
     }
   }
   ```

4. **Create the database**
   ```bash
   dotnet ef database update --project src/TOB.Accounts.Infrastructure --startup-project src/TOB.Accounts.API
   ```

5. **Build the solution**
   ```bash
   dotnet build src/TOB.Accounts.sln
   ```

## Configuration

### App Settings

Configuration is managed through `appsettings.json` and environment-specific overrides.

#### Connection Strings
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=TOBAccountsDb;..."
  }
}
```

#### Azure AD (Microsoft Entra External ID)
```json
{
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id",
    "Audience": "your-api-client-id"
  }
}
```

#### CORS
```json
{
  "Cors": {
    "AllowedOrigins": "http://localhost:3000;https://yourdomain.com",
    "AllowCredentials": true,
    "AllowedMethods": "GET;POST;PUT;DELETE;PATCH;OPTIONS",
    "AllowedHeaders": "Content-Type;Authorization;X-Requested-With",
    "MaxAge": 600
  }
}
```

#### OpenTelemetry
```json
{
  "OpenTelemetry": {
    "UseConsoleExporter": true,
    "OtlpEndpoint": "http://localhost:4317",
    "ServiceName": "TOB.Accounts.API",
    "ServiceVersion": "1.0.0",
    "EnableTracing": true,
    "EnableMetrics": true,
    "EnableLogging": true
  }
}
```

#### Key Vault (Production Only)
```json
{
  "KeyVault": {
    "VaultUri": "https://your-keyvault.vault.azure.net/"
  }
}
```

### Environment Variables

For production, sensitive values should be stored in Azure Key Vault or environment variables:

- `ConnectionStrings__DefaultConnection`
- `AzureAd__TenantId`
- `AzureAd__ClientId`
- `AzureAd__Audience`

## Running the Application

### Development Mode

```bash
cd src/TOB.Accounts.API
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

### Using Docker (Optional)

```bash
docker build -t tob-accounts-api .
docker run -p 8080:8080 tob-accounts-api
```

## API Documentation

### Swagger UI

When running in development mode, access the interactive API documentation at:
```
https://localhost:5001/swagger
```

### Authentication

All endpoints require authentication. Include a JWT Bearer token in the Authorization header:

```bash
curl -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  https://localhost:5001/api/accounts
```

### Endpoints

#### Accounts

| Method | Endpoint | Description | Access |
|--------|----------|-------------|--------|
| GET | `/api/accounts` | Get all accounts for user's tenant | Authenticated |
| GET | `/api/accounts?tenantId={guid}` | Get accounts for specific tenant | Super Admin only |
| GET | `/api/accounts/{id}` | Get account by ID | Authenticated (tenant-scoped) |
| POST | `/api/accounts` | Create new account | Authenticated |
| PUT | `/api/accounts/{id}` | Update account | Authenticated (tenant-scoped) |
| DELETE | `/api/accounts/{id}` | Soft delete account | Authenticated (tenant-scoped) |

#### Contacts

Contacts are nested under accounts in the API hierarchy. All contact operations require a valid `accountId`.

| Method | Endpoint | Description | Access |
|--------|----------|-------------|--------|
| GET | `/api/accounts/{accountId}/contacts` | Get all contacts for an account | Authenticated (account must belong to user's tenant) |
| GET | `/api/accounts/{accountId}/contacts/{id}` | Get contact by ID | Authenticated (account and contact must belong to user's tenant) |
| POST | `/api/accounts/{accountId}/contacts` | Create new contact for account | Authenticated (account must belong to user's tenant) |
| PUT | `/api/accounts/{accountId}/contacts/{id}` | Update contact | Authenticated (account and contact must belong to user's tenant) |
| DELETE | `/api/accounts/{accountId}/contacts/{id}` | Soft delete contact | Authenticated (account and contact must belong to user's tenant) |

**Note:** Super Admins can access contacts for accounts in any tenant.

### Sample Requests

**Create Account**
```bash
POST /api/accounts
Authorization: Bearer {token}
Content-Type: application/json

{
  "tenantId": "tenant-guid",
  "name": "Acme Corporation",
  "addressLine1": "123 Main St",
  "city": "New York",
  "state": "NY",
  "postalCode": "10001",
  "country": "USA",
  "primaryContactName": "John Doe",
  "primaryContactEmail": "john@acme.com",
  "primaryContactPhone": "+1-555-0100",
  "createdBy": "user@example.com"
}
```

**Create Contact for Account**
```bash
POST /api/accounts/{account-guid}/contacts
Authorization: Bearer {token}
Content-Type: application/json

{
  "tenantId": "tenant-guid",
  "accountId": "account-guid",
  "firstName": "Jane",
  "lastName": "Smith",
  "email": "jane.smith@acme.com",
  "phoneNumber": "+1-555-0101",
  "mobileNumber": "+1-555-0102",
  "addressLine1": "456 Oak Ave",
  "city": "Boston",
  "state": "MA",
  "postalCode": "02101",
  "country": "USA",
  "createdBy": "user@example.com"
}
```

**Get All Contacts for Account**
```bash
GET /api/accounts/{account-guid}/contacts
Authorization: Bearer {token}
```

## Security

### Authentication Flow

1. User authenticates with Microsoft Entra External ID
2. Receives JWT token with claims including `extension_TenantId` and `extension_Roles`
3. Includes token in `Authorization: Bearer {token}` header
4. API validates token and extracts tenant/role information
5. Enforces tenant isolation and role-based access

### User Roles

**Regular Users:**
- Can only access data within their own tenant (from `extension_TenantId` claim)
- Cannot specify `tenantId` query parameter on accounts endpoints
- Can only access contacts for accounts that belong to their tenant
- All Create/Update/Delete operations validated against their tenant

**Super Admin:**
- Can access data across all tenants
- Can specify `tenantId` query parameter on accounts GET operations
- Can access contacts for any account regardless of tenant
- No tenant validation on Create/Update/Delete operations

### Tenant Isolation

- Tenant ID is extracted from JWT claims (`extension_TenantId`)
- All GET operations filtered by tenant (unless Super Admin)
- All Create operations validate tenant matches user's claim (unless Super Admin)
- All Update/Delete operations verify resource belongs to user's tenant (unless Super Admin)
- Contact operations validate that the parent account belongs to user's tenant
- Returns 404 (not 403) for unauthorized cross-tenant access to prevent information disclosure

## Deployment

See the [deployment documentation](deploy/README.md) for detailed instructions.

### Quick Start

1. **Infrastructure Deployment**
   ```bash
   az deployment group create \
     --resource-group rg-tob-accounts-prod \
     --template-file deploy/main.bicep \
     --parameters deploy/main.parameters.prod.json
   ```

2. **Application Deployment**
   ```bash
   dotnet publish src/TOB.Accounts.API -c Release -o ./publish
   az webapp deployment source config-zip \
     --resource-group rg-tob-accounts-prod \
     --name your-app-service-name \
     --src publish.zip
   ```

### CI/CD Pipelines

- **Infrastructure Pipeline**: `deploy/infrastructure-pipelines.yml`
- **Application Pipeline**: `deploy/application-pipelines.yml`

## Project Structure

```
tob-accounts-api/
├── src/
│   ├── TOB.Accounts.API/              # Web API layer
│   │   ├── Controllers/               # API controllers
│   │   ├── Exceptions/                # Global exception handlers
│   │   ├── Telemetry/                 # OpenTelemetry configuration
│   │   └── Program.cs                 # Application entry point
│   │
│   ├── TOB.Accounts.Domain/           # Domain layer (no dependencies)
│   │   ├── Commands/                  # CQRS commands
│   │   ├── Queries/                   # CQRS queries
│   │   ├── Models/                    # DTOs
│   │   ├── Requests/                  # Request models
│   │   ├── Responses/                 # Response models
│   │   └── AppSettings/               # Configuration classes
│   │
│   ├── TOB.Accounts.Infrastructure/   # Infrastructure layer
│   │   ├── Data/                      # EF Core entities & DbContext
│   │   ├── Extensions/                # Entity/DTO mapping extensions
│   │   └── Repositories/              # Repository implementations
│   │       ├── Implementations/
│   │       ├── IAccountRepository.cs
│   │       └── IContactRepository.cs
│   │
│   ├── TOB.Accounts.Services/         # Application services layer
│   │   └── Handlers/                  # MediatR handlers
│   │       ├── CommandHandlers/
│   │       └── QueryHandlers/
│   │
│   └── TOB.Accounts.sln               # Solution file
│
├── deploy/                            # Deployment artifacts
│   ├── main.bicep                     # Infrastructure as Code
│   ├── infrastructure-pipelines.yml   # Infrastructure pipeline
│   ├── application-pipelines.yml      # Application pipeline
│   ├── main.parameters.example.json   # Parameter template
│   └── README.md                      # Deployment guide
│
└── README.md                          # This file
```

## Development

### Database Migrations

**Add a new migration:**
```bash
dotnet ef migrations add MigrationName \
  --project src/TOB.Accounts.Infrastructure \
  --startup-project src/TOB.Accounts.API
```

**Update database:**
```bash
dotnet ef database update \
  --project src/TOB.Accounts.Infrastructure \
  --startup-project src/TOB.Accounts.API
```

**Remove last migration:**
```bash
dotnet ef migrations remove \
  --project src/TOB.Accounts.Infrastructure \
  --startup-project src/TOB.Accounts.API
```

### Code Style

This project follows:
- [C# Coding Conventions](https://learn.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [.NET Framework Design Guidelines](https://learn.microsoft.com/dotnet/standard/design-guidelines/)
- Clean Architecture principles
- SOLID principles

### Adding New Entities

1. Create entity in `Infrastructure/Data/`
2. Create DTO in `Domain/Models/`
3. Create mapping extensions in `Infrastructure/Extensions/`
4. Create repository interface in `Infrastructure/Repositories/`
5. Create repository implementation in `Infrastructure/Repositories/Implementations/`
6. Create commands/queries in `Domain/Commands/` and `Domain/Queries/`
7. Create handlers in `Services/Handlers/`
8. Create controller in `API/Controllers/`
9. Add migration and update database

## Testing

### Run All Tests
```bash
dotnet test src/TOB.Accounts.sln
```

### Run Tests with Coverage
```bash
dotnet test src/TOB.Accounts.sln --collect:"XPlat Code Coverage"
```

### Test Categories

- **Unit Tests**: Test individual components in isolation
- **Integration Tests**: Test component interactions
- **API Tests**: Test HTTP endpoints end-to-end

## Contributing

### Branching Strategy

- `main` - Production-ready code
- `develop` - Integration branch
- `feature/*` - Feature branches
- `bugfix/*` - Bug fix branches
- `hotfix/*` - Production hotfixes

### Pull Request Process

1. Create a feature branch from `develop`
2. Make your changes following code style guidelines
3. Add/update tests for your changes
4. Ensure all tests pass
5. Update documentation as needed
6. Submit a pull request to `develop`
7. Address code review feedback
8. Merge after approval

## License

[Your License Here]

## Support

For issues and questions:
- **Issues**: [GitHub Issues](https://github.com/your-org/tob-accounts-api/issues)
- **Discussions**: [GitHub Discussions](https://github.com/your-org/tob-accounts-api/discussions)
- **Email**: support@yourcompany.com

## Acknowledgments

Built with:
- [.NET](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [MediatR](https://github.com/jbogard/MediatR)
- [OpenTelemetry](https://opentelemetry.io/)
- [Microsoft Identity Platform](https://learn.microsoft.com/azure/active-directory/develop/)
