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

    // Address Information
    [MaxLength(500)]
    public string? AddressLine1 { get; set; }

    [MaxLength(500)]
    public string? AddressLine2 { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(100)]
    public string? State { get; set; }

    [MaxLength(20)]
    public string? PostalCode { get; set; }

    [MaxLength(100)]
    public string? Country { get; set; }

    // Primary Contact Information
    [MaxLength(200)]
    public string? PrimaryContactName { get; set; }

    [MaxLength(200)]
    [EmailAddress]
    public string? PrimaryContactEmail { get; set; }

    [MaxLength(50)]
    [Phone]
    public string? PrimaryContactPhone { get; set; }

    // Navigation property for related contacts
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

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
