using Microsoft.EntityFrameworkCore;
using TOB.Accounts.Domain.DTOs;
using TOB.Accounts.Infrastructure.Data;
using TOB.Accounts.Infrastructure.Extensions;

namespace TOB.Accounts.Infrastructure.Repositories.Implementations;

public class AccountTypeRepository : IAccountTypeRepository
{
    private readonly AccountsDbContext _context;

    public AccountTypeRepository(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountTypeDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var accountTypes = await _context.AccountTypes
            .OrderBy(at => at.DisplayOrder)
            .ThenBy(at => at.Name)
            .ToListAsync(cancellationToken);

        return accountTypes.Select(at => at.ToDto());
    }

    public async Task<AccountTypeDto?> GetByIdAsync(Guid accountTypeId, CancellationToken cancellationToken = default)
    {
        var accountType = await _context.AccountTypes
            .FirstOrDefaultAsync(at => at.AccountTypeId == accountTypeId, cancellationToken);

        return accountType?.ToDto();
    }

    public async Task<AccountTypeDto?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var accountType = await _context.AccountTypes
            .FirstOrDefaultAsync(at => at.Name == name, cancellationToken);

        return accountType?.ToDto();
    }
}
