namespace TOB.Accounts.Domain.Models;

public class AccountDto
{
    public Guid AccountId { get; set; }
    public required string TenantId { get; set; }
    public required string Name { get; set; }

    // Address Information
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }

    // Primary Contact Information
    public string? PrimaryContactName { get; set; }
    public string? PrimaryContactEmail { get; set; }
    public string? PrimaryContactPhone { get; set; }

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
