using TOB.Accounts.Domain.Models;
using TOB.Accounts.Infrastructure.Data;

namespace TOB.Accounts.Infrastructure.Extensions;

public static class AccountExtensions
{
    /// <summary>
    /// Maps an Account entity to AccountDto
    /// </summary>
    public static AccountDto ToDto(this Account account, bool includeContacts = false)
    {
        var dto = new AccountDto
        {
            AccountId = account.AccountId,
            TenantId = account.TenantId,
            Name = account.Name,
            AddressLine1 = account.AddressLine1,
            AddressLine2 = account.AddressLine2,
            City = account.City,
            State = account.State,
            PostalCode = account.PostalCode,
            Country = account.Country,
            PrimaryContactName = account.PrimaryContactName,
            PrimaryContactEmail = account.PrimaryContactEmail,
            PrimaryContactPhone = account.PrimaryContactPhone,
            IsActive = account.IsActive,
            CreatedAt = account.CreatedAt,
            CreatedBy = account.CreatedBy,
            UpdatedAt = account.UpdatedAt,
            UpdatedBy = account.UpdatedBy
        };

        if (includeContacts && account.Contacts != null && account.Contacts.Any())
        {
            dto.Contacts = account.Contacts.Select(c => c.ToDto()).ToList();
        }

        return dto;
    }

    /// <summary>
    /// Maps an AccountDto to Account entity
    /// </summary>
    public static Account ToEntity(this AccountDto dto)
    {
        return new Account
        {
            AccountId = dto.AccountId,
            TenantId = dto.TenantId,
            Name = dto.Name,
            AddressLine1 = dto.AddressLine1,
            AddressLine2 = dto.AddressLine2,
            City = dto.City,
            State = dto.State,
            PostalCode = dto.PostalCode,
            Country = dto.Country,
            PrimaryContactName = dto.PrimaryContactName,
            PrimaryContactEmail = dto.PrimaryContactEmail,
            PrimaryContactPhone = dto.PrimaryContactPhone,
            IsActive = dto.IsActive,
            CreatedAt = dto.CreatedAt,
            CreatedBy = dto.CreatedBy,
            UpdatedAt = dto.UpdatedAt,
            UpdatedBy = dto.UpdatedBy
        };
    }

    /// <summary>
    /// Updates an existing Account entity from AccountDto
    /// </summary>
    public static void UpdateFromDto(this Account account, AccountDto dto)
    {
        account.Name = dto.Name;
        account.AddressLine1 = dto.AddressLine1;
        account.AddressLine2 = dto.AddressLine2;
        account.City = dto.City;
        account.State = dto.State;
        account.PostalCode = dto.PostalCode;
        account.Country = dto.Country;
        account.PrimaryContactName = dto.PrimaryContactName;
        account.PrimaryContactEmail = dto.PrimaryContactEmail;
        account.PrimaryContactPhone = dto.PrimaryContactPhone;
        account.IsActive = dto.IsActive;
        // Note: TenantId and AccountId should not be updated
        // Audit fields will be updated by DbContext
    }

    /// <summary>
    /// Maps a collection of Account entities to AccountDto collection
    /// </summary>
    public static IEnumerable<AccountDto> ToDtoList(this IEnumerable<Account> accounts, bool includeContacts = false)
    {
        return accounts.Select(a => a.ToDto(includeContacts));
    }
}
