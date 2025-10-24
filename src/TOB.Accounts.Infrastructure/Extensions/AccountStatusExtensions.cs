using TOB.Accounts.Domain.DTOs;
using TOB.Accounts.Infrastructure.Data;

namespace TOB.Accounts.Infrastructure.Extensions;

public static class AccountStatusExtensions
{
    public static AccountStatusDto ToDto(this AccountStatus accountStatus)
    {
        return new AccountStatusDto
        {
            AccountStatusId = accountStatus.AccountStatusId,
            Name = accountStatus.Name,
            Description = accountStatus.Description,
            IsActive = accountStatus.IsActive,
            DisplayOrder = accountStatus.DisplayOrder
        };
    }
}
