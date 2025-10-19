using System.ComponentModel.DataAnnotations;

namespace TOB.Accounts.Domain.AppSettings;

/// <summary>
/// Configuration options for Azure Key Vault
/// </summary>
public class KeyVaultOptions
{
    public const string SectionName = "KeyVault";

    /// <summary>
    /// The URI of the Azure Key Vault (e.g., "https://your-keyvault-name.vault.azure.net/")
    /// </summary>
    [Required(ErrorMessage = "VaultUri is required")]
    [Url(ErrorMessage = "VaultUri must be a valid URL")]
    public required string VaultUri { get; set; }
}
