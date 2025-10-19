using System.ComponentModel.DataAnnotations;

namespace TOB.Accounts.Domain.Requests;

public class CreateContactRequest
{
    [Required(ErrorMessage = "Tenant ID is required")]
    [MaxLength(100, ErrorMessage = "Tenant ID cannot exceed 100 characters")]
    public required string TenantId { get; set; }

    [Required(ErrorMessage = "Account ID is required")]
    public Guid AccountId { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public required string LastName { get; set; }

    [MaxLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string? Email { get; set; }

    [MaxLength(50, ErrorMessage = "Phone number cannot exceed 50 characters")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string? PhoneNumber { get; set; }

    [MaxLength(50, ErrorMessage = "Mobile number cannot exceed 50 characters")]
    [Phone(ErrorMessage = "Invalid mobile number format")]
    public string? MobileNumber { get; set; }

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
}
