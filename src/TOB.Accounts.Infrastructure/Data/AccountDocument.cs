using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TOB.Accounts.Infrastructure.Data;

public class AccountDocument
{
    [Key]
    public Guid DocumentId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string TenantId { get; set; }

    [Required]
    [ForeignKey(nameof(Account))]
    public Guid AccountId { get; set; }

    [Required]
    [MaxLength(500)]
    public required string FileName { get; set; }

    [Required]
    [MaxLength(2000)]
    public required string BlobUrl { get; set; }

    [Required]
    [MaxLength(100)]
    public required string ContentType { get; set; }

    public long FileSizeBytes { get; set; }

    [MaxLength(200)]
    public string? Category { get; set; } // Invoice, Contract, Proposal, etc.

    [MaxLength(2000)]
    public string? Description { get; set; }

    [MaxLength(500)]
    public string? BlobContainer { get; set; }

    [MaxLength(500)]
    public string? BlobName { get; set; }

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
