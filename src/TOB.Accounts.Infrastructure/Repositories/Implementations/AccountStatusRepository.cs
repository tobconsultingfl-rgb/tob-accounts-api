using Microsoft.EntityFrameworkCore;
using TOB.Accounts.Domain.DTOs;
using TOB.Accounts.Infrastructure.Data;
using TOB.Accounts.Infrastructure.Extensions;

namespace TOB.Accounts.Infrastructure.Repositories.Implementations;

public class AccountStatusRepository : IAccountStatusRepository
{
    private readonly AccountsDbContext _context;

    public AccountStatusRepository(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountStatusDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var accountStatuses = await _context.AccountStatuses
            .OrderBy(ast => ast.DisplayOrder)
            .ThenBy(ast => ast.Name)
            .ToListAsync(cancellationToken);

        return accountStatuses.Select(ast => ast.ToDto());
    }

    public async Task<AccountStatusDto?> GetByIdAsync(Guid accountStatusId, CancellationToken cancellationToken = default)
    {
        var accountStatus = await _context.AccountStatuses
            .FirstOrDefaultAsync(ast => ast.AccountStatusId == accountStatusId, cancellationToken);

        return accountStatus?.ToDto();
    }

    public async Task<AccountStatusDto?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var accountStatus = await _context.AccountStatuses
            .FirstOrDefaultAsync(ast => ast.Name == name, cancellationToken);

        return accountStatus?.ToDto();
    }
}
