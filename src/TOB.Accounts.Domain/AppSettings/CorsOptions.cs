using System.ComponentModel.DataAnnotations;

namespace TOB.Accounts.Domain.AppSettings;

/// <summary>
/// Configuration options for Cross-Origin Resource Sharing (CORS)
/// </summary>
public class CorsOptions
{
    public const string SectionName = "Cors";
    public const string DefaultPolicyName = "DefaultCorsPolicy";

    /// <summary>
    /// Semicolon-delimited list of allowed origins (e.g., "http://localhost:3000;https://app.example.com")
    /// </summary>
    [Required(ErrorMessage = "AllowedOrigins is required")]
    public required string AllowedOrigins { get; set; }

    /// <summary>
    /// Whether to allow credentials (cookies, authorization headers)
    /// </summary>
    public bool AllowCredentials { get; set; } = true;

    /// <summary>
    /// Semicolon-delimited list of allowed HTTP methods (e.g., "GET;POST;PUT;DELETE")
    /// If empty, all methods are allowed
    /// </summary>
    public string? AllowedMethods { get; set; }

    /// <summary>
    /// Semicolon-delimited list of allowed headers
    /// If empty, all headers are allowed
    /// </summary>
    public string? AllowedHeaders { get; set; }

    /// <summary>
    /// Semicolon-delimited list of exposed headers
    /// </summary>
    public string? ExposedHeaders { get; set; }

    /// <summary>
    /// Maximum age in seconds for preflight cache
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "MaxAge must be a positive number")]
    public int MaxAge { get; set; } = 600;

    /// <summary>
    /// Gets the allowed origins as an array
    /// </summary>
    public string[] GetAllowedOrigins()
    {
        return AllowedOrigins
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .ToArray();
    }

    /// <summary>
    /// Gets the allowed methods as an array
    /// </summary>
    public string[]? GetAllowedMethods()
    {
        if (string.IsNullOrWhiteSpace(AllowedMethods))
            return null;

        return AllowedMethods
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .ToArray();
    }

    /// <summary>
    /// Gets the allowed headers as an array
    /// </summary>
    public string[]? GetAllowedHeaders()
    {
        if (string.IsNullOrWhiteSpace(AllowedHeaders))
            return null;

        return AllowedHeaders
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(h => !string.IsNullOrWhiteSpace(h))
            .ToArray();
    }

    /// <summary>
    /// Gets the exposed headers as an array
    /// </summary>
    public string[]? GetExposedHeaders()
    {
        if (string.IsNullOrWhiteSpace(ExposedHeaders))
            return null;

        return ExposedHeaders
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(h => !string.IsNullOrWhiteSpace(h))
            .ToArray();
    }
}
