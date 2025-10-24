using TOB.Accounts.Domain.DTOs;

namespace TOB.Accounts.Infrastructure.Repositories;

public interface IAccountStatusRepository
{
    Task<IEnumerable<AccountStatusDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AccountStatusDto?> GetByIdAsync(Guid accountStatusId, CancellationToken cancellationToken = default);
    Task<AccountStatusDto?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
