using Microsoft.EntityFrameworkCore;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Infrastructure.Data;
using TOB.Accounts.Infrastructure.Extensions;

namespace TOB.Accounts.Infrastructure.Repositories.Implementations;

public class AccountDocumentRepository : IAccountDocumentRepository
{
    private readonly AccountsDbContext _context;

    public AccountDocumentRepository(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task<AccountDocumentDto?> GetByIdAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        var document = await _context.AccountDocuments
            .FirstOrDefaultAsync(d => d.DocumentId == documentId && d.IsActive, cancellationToken);

        return document?.ToDto();
    }

    public async Task<IEnumerable<AccountDocumentDto>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        var documents = await _context.AccountDocuments
            .Where(d => d.AccountId == accountId && d.IsActive)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(cancellationToken);

        return documents.ToDtoList();
    }

    public async Task<AccountDocumentDto> CreateAsync(AccountDocumentDto document, CancellationToken cancellationToken = default)
    {
        var entity = document.ToEntity();

        _context.AccountDocuments.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.ToDto();
    }

    public async Task<bool> DeleteAsync(Guid documentId, string deletedBy, CancellationToken cancellationToken = default)
    {
        var document = await _context.AccountDocuments
            .FirstOrDefaultAsync(d => d.DocumentId == documentId, cancellationToken);

        if (document == null)
        {
            return false;
        }

        // Soft delete
        document.IsActive = false;
        document.UpdatedAt = DateTime.UtcNow;
        document.UpdatedBy = deletedBy;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
