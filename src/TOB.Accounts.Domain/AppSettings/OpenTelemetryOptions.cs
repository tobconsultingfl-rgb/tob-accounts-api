using System.ComponentModel.DataAnnotations;

namespace TOB.Accounts.Domain.AppSettings;

/// <summary>
/// Configuration options for OpenTelemetry observability
/// </summary>
public class OpenTelemetryOptions
{
    public const string SectionName = "OpenTelemetry";

    /// <summary>
    /// Whether to use console exporter for telemetry (useful for development)
    /// </summary>
    public bool UseConsoleExporter { get; set; } = true;

    /// <summary>
    /// OTLP (OpenTelemetry Protocol) endpoint URL for exporting telemetry
    /// </summary>
    [Required(ErrorMessage = "OtlpEndpoint is required")]
    [Url(ErrorMessage = "OtlpEndpoint must be a valid URL")]
    public required string OtlpEndpoint { get; set; }

    /// <summary>
    /// Service name for telemetry identification
    /// </summary>
    [Required(ErrorMessage = "ServiceName is required")]
    public required string ServiceName { get; set; }

    /// <summary>
    /// Service version for telemetry identification
    /// </summary>
    [Required(ErrorMessage = "ServiceVersion is required")]
    public required string ServiceVersion { get; set; }

    /// <summary>
    /// Optional: Enable tracing
    /// </summary>
    public bool EnableTracing { get; set; } = true;

    /// <summary>
    /// Optional: Enable metrics
    /// </summary>
    public bool EnableMetrics { get; set; } = true;

    /// <summary>
    /// Optional: Enable logging
    /// </summary>
    public bool EnableLogging { get; set; } = true;

    /// <summary>
    /// Optional: Sampling ratio for traces (0.0 to 1.0)
    /// </summary>
    [Range(0.0, 1.0, ErrorMessage = "SamplingRatio must be between 0.0 and 1.0")]
    public double SamplingRatio { get; set; } = 1.0;

    /// <summary>
    /// Optional: Additional resource attributes
    /// </summary>
    public Dictionary<string, string>? ResourceAttributes { get; set; }
}
