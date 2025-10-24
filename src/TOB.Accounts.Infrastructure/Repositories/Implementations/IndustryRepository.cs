using Microsoft.EntityFrameworkCore;
using TOB.Accounts.Domain.DTOs;
using TOB.Accounts.Infrastructure.Data;
using TOB.Accounts.Infrastructure.Extensions;

namespace TOB.Accounts.Infrastructure.Repositories.Implementations;

public class IndustryRepository : IIndustryRepository
{
    private readonly AccountsDbContext _context;

    public IndustryRepository(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IndustryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var industries = await _context.Industries
            .OrderBy(i => i.DisplayOrder)
            .ThenBy(i => i.Name)
            .ToListAsync(cancellationToken);

        return industries.Select(i => i.ToDto());
    }

    public async Task<IndustryDto?> GetByIdAsync(Guid industryId, CancellationToken cancellationToken = default)
    {
        var industry = await _context.Industries
            .FirstOrDefaultAsync(i => i.IndustryId == industryId, cancellationToken);

        return industry?.ToDto();
    }

    public async Task<IndustryDto?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var industry = await _context.Industries
            .FirstOrDefaultAsync(i => i.Name == name, cancellationToken);

        return industry?.ToDto();
    }
}
