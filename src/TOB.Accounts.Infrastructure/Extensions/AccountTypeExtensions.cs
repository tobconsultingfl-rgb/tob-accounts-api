using TOB.Accounts.Domain.DTOs;
using TOB.Accounts.Infrastructure.Data;

namespace TOB.Accounts.Infrastructure.Extensions;

public static class AccountTypeExtensions
{
    public static AccountTypeDto ToDto(this AccountType accountType)
    {
        return new AccountTypeDto
        {
            AccountTypeId = accountType.AccountTypeId,
            Name = accountType.Name,
            Description = accountType.Description,
            IsActive = accountType.IsActive,
            DisplayOrder = accountType.DisplayOrder
        };
    }
}
