using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Infrastructure.Repositories;

public interface IAccountDocumentRepository
{
    /// <summary>
    /// Get document by ID
    /// </summary>
    /// <param name="documentId">Document ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Document if found, null otherwise</returns>
    Task<AccountDocumentDto?> GetByIdAsync(Guid documentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all documents for an account
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of documents</returns>
    Task<IEnumerable<AccountDocumentDto>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new document record
    /// </summary>
    /// <param name="document">Document to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created document</returns>
    Task<AccountDocumentDto> CreateAsync(AccountDocumentDto document, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a document (soft delete by setting IsActive to false)
    /// </summary>
    /// <param name="documentId">Document ID</param>
    /// <param name="deletedBy">User deleting the document</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid documentId, string deletedBy, CancellationToken cancellationToken = default);
}
