using System.ComponentModel.DataAnnotations;

namespace TOB.Accounts.Domain.AppSettings;

/// <summary>
/// Configuration options for Azure Storage
/// </summary>
public class AzureStorageOptions
{
    public const string SectionName = "AzureStorage";

    /// <summary>
    /// Azure Storage connection string
    /// </summary>
    [Required(ErrorMessage = "ConnectionString is required")]
    public required string ConnectionString { get; set; }

    /// <summary>
    /// Azure Storage account name
    /// </summary>
    [Required(ErrorMessage = "AccountName is required")]
    public required string AccountName { get; set; }
}
