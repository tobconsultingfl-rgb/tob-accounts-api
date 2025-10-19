using System.ComponentModel.DataAnnotations;

namespace TOB.Accounts.Domain.Requests;

public class CreateAccountRequest
{
    [Required(ErrorMessage = "Tenant ID is required")]
    [MaxLength(100, ErrorMessage = "Tenant ID cannot exceed 100 characters")]
    public required string TenantId { get; set; }

    [Required(ErrorMessage = "Account name is required")]
    [MaxLength(200, ErrorMessage = "Account name cannot exceed 200 characters")]
    public required string Name { get; set; }

    // Address Information
    [MaxLength(500, ErrorMessage = "Address line 1 cannot exceed 500 characters")]
    public string? AddressLine1 { get; set; }

    [MaxLength(500, ErrorMessage = "Address line 2 cannot exceed 500 characters")]
    public string? AddressLine2 { get; set; }

    [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters")]
    public string? City { get; set; }

    [MaxLength(100, ErrorMessage = "State cannot exceed 100 characters")]
    public string? State { get; set; }

    [MaxLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
    public string? PostalCode { get; set; }

    [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
    public string? Country { get; set; }

    // Primary Contact Information
    [MaxLength(200, ErrorMessage = "Primary contact name cannot exceed 200 characters")]
    public string? PrimaryContactName { get; set; }

    [MaxLength(200, ErrorMessage = "Primary contact email cannot exceed 200 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string? PrimaryContactEmail { get; set; }

    [MaxLength(50, ErrorMessage = "Primary contact phone cannot exceed 50 characters")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string? PrimaryContactPhone { get; set; }
}
