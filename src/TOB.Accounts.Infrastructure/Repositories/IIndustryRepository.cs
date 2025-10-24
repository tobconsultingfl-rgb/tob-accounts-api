using TOB.Accounts.Domain.DTOs;

namespace TOB.Accounts.Infrastructure.Repositories;

public interface IIndustryRepository
{
    Task<IEnumerable<IndustryDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IndustryDto?> GetByIdAsync(Guid industryId, CancellationToken cancellationToken = default);
    Task<IndustryDto?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
