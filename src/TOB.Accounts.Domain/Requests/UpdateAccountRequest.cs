using System.ComponentModel.DataAnnotations;

namespace TOB.Accounts.Domain.Requests;

public class UpdateAccountRequest
{
    [Required(ErrorMessage = "Account ID is required")]
    public Guid AccountId { get; set; }

    [Required(ErrorMessage = "Account name is required")]
    [MaxLength(200, ErrorMessage = "Account name cannot exceed 200 characters")]
    public required string Name { get; set; }

    // CRM Business Information
    [MaxLength(50, ErrorMessage = "Account type cannot exceed 50 characters")]
    public string? AccountType { get; set; }

    [MaxLength(50, ErrorMessage = "Account status cannot exceed 50 characters")]
    public string? AccountStatus { get; set; }

    [MaxLength(100, ErrorMessage = "Industry cannot exceed 100 characters")]
    public string? Industry { get; set; }

    public decimal? AnnualRevenue { get; set; }

    public int? NumberOfEmployees { get; set; }

    [MaxLength(200, ErrorMessage = "Website cannot exceed 200 characters")]
    public string? Website { get; set; }

    [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
    public string? Description { get; set; }

    [MaxLength(100, ErrorMessage = "Account number cannot exceed 100 characters")]
    public string? AccountNumber { get; set; }

    public Guid? ParentAccountId { get; set; }

    public Guid? OwnerId { get; set; }

    [MaxLength(100, ErrorMessage = "Rating cannot exceed 100 characters")]
    public string? Rating { get; set; }

    // Billing Address Information
    [MaxLength(500, ErrorMessage = "Billing address line 1 cannot exceed 500 characters")]
    public string? BillingAddressLine1 { get; set; }

    [MaxLength(500, ErrorMessage = "Billing address line 2 cannot exceed 500 characters")]
    public string? BillingAddressLine2 { get; set; }

    [MaxLength(100, ErrorMessage = "Billing city cannot exceed 100 characters")]
    public string? BillingCity { get; set; }

    [MaxLength(100, ErrorMessage = "Billing state cannot exceed 100 characters")]
    public string? BillingState { get; set; }

    [MaxLength(20, ErrorMessage = "Billing postal code cannot exceed 20 characters")]
    public string? BillingPostalCode { get; set; }

    [MaxLength(100, ErrorMessage = "Billing country cannot exceed 100 characters")]
    public string? BillingCountry { get; set; }

    // Shipping Address Information
    [MaxLength(500, ErrorMessage = "Shipping address line 1 cannot exceed 500 characters")]
    public string? ShippingAddressLine1 { get; set; }

    [MaxLength(500, ErrorMessage = "Shipping address line 2 cannot exceed 500 characters")]
    public string? ShippingAddressLine2 { get; set; }

    [MaxLength(100, ErrorMessage = "Shipping city cannot exceed 100 characters")]
    public string? ShippingCity { get; set; }

    [MaxLength(100, ErrorMessage = "Shipping state cannot exceed 100 characters")]
    public string? ShippingState { get; set; }

    [MaxLength(20, ErrorMessage = "Shipping postal code cannot exceed 20 characters")]
    public string? ShippingPostalCode { get; set; }

    [MaxLength(100, ErrorMessage = "Shipping country cannot exceed 100 characters")]
    public string? ShippingCountry { get; set; }

    // Contact Information
    [MaxLength(50, ErrorMessage = "Phone cannot exceed 50 characters")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string? Phone { get; set; }

    [MaxLength(50, ErrorMessage = "Fax cannot exceed 50 characters")]
    [Phone(ErrorMessage = "Invalid fax number format")]
    public string? Fax { get; set; }

    [MaxLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;
}
