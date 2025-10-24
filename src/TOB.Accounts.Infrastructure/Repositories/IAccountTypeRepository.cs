using TOB.Accounts.Domain.DTOs;

namespace TOB.Accounts.Infrastructure.Repositories;

public interface IAccountTypeRepository
{
    Task<IEnumerable<AccountTypeDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AccountTypeDto?> GetByIdAsync(Guid accountTypeId, CancellationToken cancellationToken = default);
    Task<AccountTypeDto?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
