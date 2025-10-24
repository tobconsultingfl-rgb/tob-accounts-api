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

            // CRM Business Information - Flatten lookup navigation properties to strings
            AccountType = account.AccountType?.Name,
            AccountStatus = account.AccountStatus?.Name,
            Industry = account.Industry?.Name,
            AnnualRevenue = account.AnnualRevenue,
            NumberOfEmployees = account.NumberOfEmployees,
            Website = account.Website,
            Description = account.Description,
            AccountNumber = account.AccountNumber,
            ParentAccountId = account.ParentAccountId,
            OwnerId = account.OwnerId,
            Rating = account.Rating,

            // Billing Address Information
            BillingAddressLine1 = account.BillingAddressLine1,
            BillingAddressLine2 = account.BillingAddressLine2,
            BillingCity = account.BillingCity,
            BillingState = account.BillingState,
            BillingPostalCode = account.BillingPostalCode,
            BillingCountry = account.BillingCountry,

            // Shipping Address Information
            ShippingAddressLine1 = account.ShippingAddressLine1,
            ShippingAddressLine2 = account.ShippingAddressLine2,
            ShippingCity = account.ShippingCity,
            ShippingState = account.ShippingState,
            ShippingPostalCode = account.ShippingPostalCode,
            ShippingCountry = account.ShippingCountry,

            // Contact Information
            Phone = account.Phone,
            Fax = account.Fax,
            Email = account.Email,

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

            // CRM Business Information
            // Note: AccountTypeId, AccountStatusId, IndustryId will be set by repository/service layer
            AnnualRevenue = dto.AnnualRevenue,
            NumberOfEmployees = dto.NumberOfEmployees,
            Website = dto.Website,
            Description = dto.Description,
            AccountNumber = dto.AccountNumber,
            ParentAccountId = dto.ParentAccountId,
            OwnerId = dto.OwnerId,
            Rating = dto.Rating,

            // Billing Address Information
            BillingAddressLine1 = dto.BillingAddressLine1,
            BillingAddressLine2 = dto.BillingAddressLine2,
            BillingCity = dto.BillingCity,
            BillingState = dto.BillingState,
            BillingPostalCode = dto.BillingPostalCode,
            BillingCountry = dto.BillingCountry,

            // Shipping Address Information
            ShippingAddressLine1 = dto.ShippingAddressLine1,
            ShippingAddressLine2 = dto.ShippingAddressLine2,
            ShippingCity = dto.ShippingCity,
            ShippingState = dto.ShippingState,
            ShippingPostalCode = dto.ShippingPostalCode,
            ShippingCountry = dto.ShippingCountry,

            // Contact Information
            Phone = dto.Phone,
            Fax = dto.Fax,
            Email = dto.Email,

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

        // CRM Business Information
        // Note: AccountTypeId, AccountStatusId, IndustryId will be updated by repository/service layer
        account.AnnualRevenue = dto.AnnualRevenue;
        account.NumberOfEmployees = dto.NumberOfEmployees;
        account.Website = dto.Website;
        account.Description = dto.Description;
        account.AccountNumber = dto.AccountNumber;
        account.ParentAccountId = dto.ParentAccountId;
        account.OwnerId = dto.OwnerId;
        account.Rating = dto.Rating;

        // Billing Address Information
        account.BillingAddressLine1 = dto.BillingAddressLine1;
        account.BillingAddressLine2 = dto.BillingAddressLine2;
        account.BillingCity = dto.BillingCity;
        account.BillingState = dto.BillingState;
        account.BillingPostalCode = dto.BillingPostalCode;
        account.BillingCountry = dto.BillingCountry;

        // Shipping Address Information
        account.ShippingAddressLine1 = dto.ShippingAddressLine1;
        account.ShippingAddressLine2 = dto.ShippingAddressLine2;
        account.ShippingCity = dto.ShippingCity;
        account.ShippingState = dto.ShippingState;
        account.ShippingPostalCode = dto.ShippingPostalCode;
        account.ShippingCountry = dto.ShippingCountry;

        // Contact Information
        account.Phone = dto.Phone;
        account.Fax = dto.Fax;
        account.Email = dto.Email;

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
