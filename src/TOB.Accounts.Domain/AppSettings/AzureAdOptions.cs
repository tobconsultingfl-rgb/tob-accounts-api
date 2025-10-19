using System.ComponentModel.DataAnnotations;

namespace TOB.Accounts.Domain.AppSettings;

/// <summary>
/// Configuration options for Microsoft Entra External ID (Azure AD) authentication
/// </summary>
public class AzureAdOptions
{
    public const string SectionName = "AzureAd";

    /// <summary>
    /// Azure AD instance URL (e.g., https://login.microsoftonline.com/)
    /// </summary>
    [Required(ErrorMessage = "Instance is required")]
    [Url(ErrorMessage = "Instance must be a valid URL")]
    public required string Instance { get; set; }

    /// <summary>
    /// Azure AD Tenant ID
    /// </summary>
    [Required(ErrorMessage = "TenantId is required")]
    public required string TenantId { get; set; }

    /// <summary>
    /// Client ID (Application ID) of the API registration
    /// </summary>
    [Required(ErrorMessage = "ClientId is required")]
    public required string ClientId { get; set; }

    /// <summary>
    /// Audience for token validation (typically the Client ID)
    /// </summary>
    [Required(ErrorMessage = "Audience is required")]
    public required string Audience { get; set; }

    /// <summary>
    /// Optional: Client Secret for confidential client flows
    /// </summary>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// Optional: Scope required for API access
    /// </summary>
    public string? Scope { get; set; }

    /// <summary>
    /// Optional: Domain hint for login
    /// </summary>
    public string? Domain { get; set; }
}
