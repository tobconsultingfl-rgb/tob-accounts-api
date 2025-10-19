using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace TOB.Accounts.API.Telemetry;

public static class Diagnostics
{
    public const string ServiceName = "TOB.Accounts.API";
    public const string ServiceVersion = "1.0.0";

    // ActivitySource for custom tracing
    public static readonly ActivitySource ActivitySource = new(ServiceName, ServiceVersion);

    // Meter for custom metrics
    public static readonly Meter Meter = new(ServiceName, ServiceVersion);

    // Custom metrics
    public static readonly Counter<long> AccountsCreatedCounter = Meter.CreateCounter<long>(
        "accounts.created",
        description: "Number of accounts created");

    public static readonly Counter<long> ContactsCreatedCounter = Meter.CreateCounter<long>(
        "contacts.created",
        description: "Number of contacts created");

    public static readonly Counter<long> AccountsDeletedCounter = Meter.CreateCounter<long>(
        "accounts.deleted",
        description: "Number of accounts deleted");

    public static readonly Counter<long> ContactsDeletedCounter = Meter.CreateCounter<long>(
        "contacts.deleted",
        description: "Number of contacts deleted");

    public static readonly Histogram<double> RequestDuration = Meter.CreateHistogram<double>(
        "request.duration",
        unit: "ms",
        description: "Request duration in milliseconds");
}
