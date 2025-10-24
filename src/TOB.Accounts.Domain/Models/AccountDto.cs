namespace TOB.Accounts.Domain.Models;

public class AccountDto
{
    public Guid AccountId { get; set; }
    public required string TenantId { get; set; }
    public required string Name { get; set; }

    // CRM Business Information
    public string? AccountType { get; set; }
    public string? AccountStatus { get; set; }
    public string? Industry { get; set; }
    public decimal? AnnualRevenue { get; set; }
    public int? NumberOfEmployees { get; set; }
    public string? Website { get; set; }
    public string? Description { get; set; }
    public string? AccountNumber { get; set; }
    public Guid? ParentAccountId { get; set; }
    public Guid? OwnerId { get; set; }
    public string? Rating { get; set; }

    // Billing Address Information
    public string? BillingAddressLine1 { get; set; }
    public string? BillingAddressLine2 { get; set; }
    public string? BillingCity { get; set; }
    public string? BillingState { get; set; }
    public string? BillingPostalCode { get; set; }
    public string? BillingCountry { get; set; }

    // Shipping Address Information
    public string? ShippingAddressLine1 { get; set; }
    public string? ShippingAddressLine2 { get; set; }
    public string? ShippingCity { get; set; }
    public string? ShippingState { get; set; }
    public string? ShippingPostalCode { get; set; }
    public string? ShippingCountry { get; set; }

    // Contact Information
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }

    // Related Contacts
    public ICollection<ContactDto>? Contacts { get; set; }

    // Soft delete flag
    public bool IsActive { get; set; }

    // Audit fields
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
