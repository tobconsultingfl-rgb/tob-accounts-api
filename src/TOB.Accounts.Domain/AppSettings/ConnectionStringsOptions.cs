using System.ComponentModel.DataAnnotations;

namespace TOB.Accounts.Domain.AppSettings;

/// <summary>
/// Configuration options for database connection strings
/// </summary>
public class ConnectionStringsOptions
{
    public const string SectionName = "ConnectionStrings";

    /// <summary>
    /// Default database connection string
    /// </summary>
    [Required(ErrorMessage = "AccountDbConnection is required")]
    public required string AccountDbConnection { get; set; }
}
