using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TOB.Accounts.Infrastructure.Data;

public class Account
{
    [Key]
    public Guid AccountId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string TenantId { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    // CRM Business Information - Lookup Foreign Keys
    [ForeignKey(nameof(AccountType))]
    public Guid? AccountTypeId { get; set; }

    [ForeignKey(nameof(AccountStatus))]
    public Guid? AccountStatusId { get; set; }

    [ForeignKey(nameof(Industry))]
    public Guid? IndustryId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? AnnualRevenue { get; set; }

    public int? NumberOfEmployees { get; set; }

    [MaxLength(200)]
    public string? Website { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    [MaxLength(100)]
    public string? AccountNumber { get; set; }

    public Guid? ParentAccountId { get; set; } // For account hierarchies

    public Guid? OwnerId { get; set; } // User assigned to this account

    [MaxLength(100)]
    public string? Rating { get; set; } // Hot, Warm, Cold

    // Address Information
    [MaxLength(500)]
    public string? BillingAddressLine1 { get; set; }

    [MaxLength(500)]
    public string? BillingAddressLine2 { get; set; }

    [MaxLength(100)]
    public string? BillingCity { get; set; }

    [MaxLength(100)]
    public string? BillingState { get; set; }

    [MaxLength(20)]
    public string? BillingPostalCode { get; set; }

    [MaxLength(100)]
    public string? BillingCountry { get; set; }

    [MaxLength(500)]
    public string? ShippingAddressLine1 { get; set; }

    [MaxLength(500)]
    public string? ShippingAddressLine2 { get; set; }

    [MaxLength(100)]
    public string? ShippingCity { get; set; }

    [MaxLength(100)]
    public string? ShippingState { get; set; }

    [MaxLength(20)]
    public string? ShippingPostalCode { get; set; }

    [MaxLength(100)]
    public string? ShippingCountry { get; set; }

    // Contact Information
    [MaxLength(50)]
    [Phone]
    public string? Phone { get; set; }

    [MaxLength(50)]
    [Phone]
    public string? Fax { get; set; }

    [MaxLength(200)]
    [EmailAddress]
    public string? Email { get; set; }

    // Navigation property for related contacts
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    // Navigation property for related documents
    public ICollection<AccountDocument> Documents { get; set; } = new List<AccountDocument>();

    // Navigation properties for lookups
    public AccountType? AccountType { get; set; }
    public AccountStatus? AccountStatus { get; set; }
    public Industry? Industry { get; set; }

    // Soft delete flag
    public bool IsActive { get; set; } = true;

    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(200)]
    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [MaxLength(200)]
    public string? UpdatedBy { get; set; }
}
