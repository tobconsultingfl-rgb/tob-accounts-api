using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TOB.Accounts.Infrastructure.Data;

public class Contact
{
    [Key]
    public Guid ContactId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string TenantId { get; set; }

    [Required]
    [ForeignKey(nameof(Account))]
    public Guid AccountId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public required string LastName { get; set; }

    [MaxLength(100)]
    public string? MiddleName { get; set; }

    [MaxLength(20)]
    public string? Salutation { get; set; } // Mr., Mrs., Ms., Dr., etc.

    [MaxLength(100)]
    public string? JobTitle { get; set; }

    [MaxLength(100)]
    public string? Department { get; set; }

    public bool IsPrimaryContact { get; set; } = false;

    public Guid? ReportsToId { get; set; } // Contact hierarchy - who this contact reports to

    public Guid? OwnerId { get; set; } // User assigned to this contact

    // Contact Information
    [MaxLength(200)]
    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(200)]
    [EmailAddress]
    public string? SecondaryEmail { get; set; }

    [MaxLength(50)]
    [Phone]
    public string? PhoneNumber { get; set; }

    [MaxLength(50)]
    [Phone]
    public string? MobileNumber { get; set; }

    [MaxLength(50)]
    [Phone]
    public string? HomePhone { get; set; }

    [MaxLength(50)]
    [Phone]
    public string? OtherPhone { get; set; }

    [MaxLength(50)]
    [Phone]
    public string? Fax { get; set; }

    [MaxLength(200)]
    public string? LinkedIn { get; set; }

    [MaxLength(200)]
    public string? Twitter { get; set; }

    [MaxLength(2000)]
    public string? Notes { get; set; }

    // Address Information
    [MaxLength(500)]
    public string? MailingAddressLine1 { get; set; }

    [MaxLength(500)]
    public string? MailingAddressLine2 { get; set; }

    [MaxLength(100)]
    public string? MailingCity { get; set; }

    [MaxLength(100)]
    public string? MailingState { get; set; }

    [MaxLength(20)]
    public string? MailingPostalCode { get; set; }

    [MaxLength(100)]
    public string? MailingCountry { get; set; }

    [MaxLength(500)]
    public string? OtherAddressLine1 { get; set; }

    [MaxLength(500)]
    public string? OtherAddressLine2 { get; set; }

    [MaxLength(100)]
    public string? OtherCity { get; set; }

    [MaxLength(100)]
    public string? OtherState { get; set; }

    [MaxLength(20)]
    public string? OtherPostalCode { get; set; }

    [MaxLength(100)]
    public string? OtherCountry { get; set; }

    public DateTime? Birthdate { get; set; }

    // Preferences
    public bool DoNotCall { get; set; } = false;

    public bool DoNotEmail { get; set; } = false;

    public bool HasOptedOutOfEmail { get; set; } = false;

    // Navigation property back to Account
    public Account? Account { get; set; }

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
