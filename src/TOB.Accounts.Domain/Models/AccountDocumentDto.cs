namespace TOB.Accounts.Domain.Models;

public class AccountDocumentDto
{
    public Guid DocumentId { get; set; }
    public required string TenantId { get; set; }
    public Guid AccountId { get; set; }
    public required string FileName { get; set; }
    public required string BlobUrl { get; set; }
    public required string ContentType { get; set; }
    public long FileSizeBytes { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public string? BlobContainer { get; set; }
    public string? BlobName { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
