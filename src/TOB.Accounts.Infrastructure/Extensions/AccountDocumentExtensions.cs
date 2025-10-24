using TOB.Accounts.Domain.Models;
using TOB.Accounts.Infrastructure.Data;

namespace TOB.Accounts.Infrastructure.Extensions;

public static class AccountDocumentExtensions
{
    /// <summary>
    /// Maps an AccountDocument entity to AccountDocumentDto
    /// </summary>
    public static AccountDocumentDto ToDto(this AccountDocument document)
    {
        return new AccountDocumentDto
        {
            DocumentId = document.DocumentId,
            TenantId = document.TenantId,
            AccountId = document.AccountId,
            FileName = document.FileName,
            BlobUrl = document.BlobUrl,
            ContentType = document.ContentType,
            FileSizeBytes = document.FileSizeBytes,
            Category = document.Category,
            Description = document.Description,
            BlobContainer = document.BlobContainer,
            BlobName = document.BlobName,
            IsActive = document.IsActive,
            CreatedAt = document.CreatedAt,
            CreatedBy = document.CreatedBy,
            UpdatedAt = document.UpdatedAt,
            UpdatedBy = document.UpdatedBy
        };
    }

    /// <summary>
    /// Maps an AccountDocumentDto to AccountDocument entity
    /// </summary>
    public static AccountDocument ToEntity(this AccountDocumentDto dto)
    {
        return new AccountDocument
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
            BlobContainer = dto.BlobContainer,
            BlobName = dto.BlobName,
            IsActive = dto.IsActive,
            CreatedAt = dto.CreatedAt,
            CreatedBy = dto.CreatedBy,
            UpdatedAt = dto.UpdatedAt,
            UpdatedBy = dto.UpdatedBy
        };
    }

    /// <summary>
    /// Maps a collection of AccountDocument entities to AccountDocumentDto collection
    /// </summary>
    public static IEnumerable<AccountDocumentDto> ToDtoList(this IEnumerable<AccountDocument> documents)
    {
        return documents.Select(d => d.ToDto());
    }
}
