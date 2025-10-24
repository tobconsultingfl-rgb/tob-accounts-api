using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Responses;

public class AccountDocumentResponse
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
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    public static AccountDocumentResponse FromDto(AccountDocumentDto dto)
    {
        return new AccountDocumentResponse
        {
            DocumentId = dto.DocumentId,
            TenantId = dto.TenantId,
            AccountId = dto.AccountId,
            FileName = dto.FileName,
            BlobUrl = dto.BlobUrl,
            ContentType = dto.ContentType,
            FileSizeBytes = dto.FileSizeBytes,
            Category = dto.Category,
            Description = dto.Description,
            CreatedAt = dto.CreatedAt,
            CreatedBy = dto.CreatedBy,
            UpdatedAt = dto.UpdatedAt,
            UpdatedBy = dto.UpdatedBy
        };
    }
}
