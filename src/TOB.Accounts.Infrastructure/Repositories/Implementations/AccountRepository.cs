using Microsoft.EntityFrameworkCore;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Requests;
using TOB.Accounts.Infrastructure.Data;
using TOB.Accounts.Infrastructure.Extensions;

namespace TOB.Accounts.Infrastructure.Repositories.Implementations;

public class AccountRepository : IAccountRepository
{
    private readonly AccountsDbContext _context;

    public AccountRepository(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountDto>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        var accounts = await _context.Accounts
            .Include(a => a.AccountType)
            .Include(a => a.AccountStatus)
            .Include(a => a.Industry)
            .Where(a => a.TenantId == tenantId.ToString())
            .ToListAsync(cancellationToken);

        return accounts.ToDtoList(includeContacts: false);
    }

    public async Task<AccountDto?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .Include(a => a.AccountType)
            .Include(a => a.AccountStatus)
            .Include(a => a.Industry)
            .FirstOrDefaultAsync(a => a.AccountId == accountId, cancellationToken);

        return account?.ToDto(includeContacts: false);
    }

    public async Task<IEnumerable<AccountDto>> GetAllWithContactsAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        var accounts = await _context.Accounts
            .Include(a => a.AccountType)
            .Include(a => a.AccountStatus)
            .Include(a => a.Industry)
            .Include(a => a.Contacts)
            .Where(a => a.TenantId == tenantId.ToString())
            .ToListAsync(cancellationToken);

        return accounts.ToDtoList(includeContacts: true);
    }

    public async Task<AccountDto?> GetByIdWithContactsAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .Include(a => a.AccountType)
            .Include(a => a.AccountStatus)
            .Include(a => a.Industry)
            .Include(a => a.Contacts)
            .FirstOrDefaultAsync(a => a.AccountId == accountId, cancellationToken);

        return account?.ToDto(includeContacts: true);
    }

    public async Task<AccountDto> CreateAsync(CreateAccountRequest createAccountRequest, string createdBy, CancellationToken cancellationToken = default)
    {
        // Look up IDs for lookup fields
        var accountTypeId = await GetAccountTypeIdByNameAsync(createAccountRequest.AccountType, cancellationToken);
        var accountStatusId = await GetAccountStatusIdByNameAsync(createAccountRequest.AccountStatus, cancellationToken);
        var industryId = await GetIndustryIdByNameAsync(createAccountRequest.Industry, cancellationToken);

        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            TenantId = createAccountRequest.TenantId,
            Name = createAccountRequest.Name,

            // CRM Business Information
            AccountTypeId = accountTypeId,
            AccountStatusId = accountStatusId,
            IndustryId = industryId,
            AnnualRevenue = createAccountRequest.AnnualRevenue,
            NumberOfEmployees = createAccountRequest.NumberOfEmployees,
            Website = createAccountRequest.Website,
            Description = createAccountRequest.Description,
            AccountNumber = createAccountRequest.AccountNumber,
            ParentAccountId = createAccountRequest.ParentAccountId,
            OwnerId = createAccountRequest.OwnerId,
            Rating = createAccountRequest.Rating,

            // Billing Address Information
            BillingAddressLine1 = createAccountRequest.BillingAddressLine1,
            BillingAddressLine2 = createAccountRequest.BillingAddressLine2,
            BillingCity = createAccountRequest.BillingCity,
            BillingState = createAccountRequest.BillingState,
            BillingPostalCode = createAccountRequest.BillingPostalCode,
            BillingCountry = createAccountRequest.BillingCountry,

            // Shipping Address Information
            ShippingAddressLine1 = createAccountRequest.ShippingAddressLine1,
            ShippingAddressLine2 = createAccountRequest.ShippingAddressLine2,
            ShippingCity = createAccountRequest.ShippingCity,
            ShippingState = createAccountRequest.ShippingState,
            ShippingPostalCode = createAccountRequest.ShippingPostalCode,
            ShippingCountry = createAccountRequest.ShippingCountry,

            // Contact Information
            Phone = createAccountRequest.Phone,
            Fax = createAccountRequest.Fax,
            Email = createAccountRequest.Email,

            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync(cancellationToken);

        // Reload account with navigation properties
        var createdAccount = await _context.Accounts
            .Include(a => a.AccountType)
            .Include(a => a.AccountStatus)
            .Include(a => a.Industry)
            .FirstOrDefaultAsync(a => a.AccountId == account.AccountId, cancellationToken);

        return createdAccount!.ToDto(includeContacts: false);
    }

    public async Task<AccountDto?> UpdateAsync(UpdateAccountRequest updateAccountRequest, string updatedBy, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == updateAccountRequest.AccountId, cancellationToken);

        if (account == null)
        {
            return null;
        }

        // Look up IDs for lookup fields
        var accountTypeId = await GetAccountTypeIdByNameAsync(updateAccountRequest.AccountType, cancellationToken);
        var accountStatusId = await GetAccountStatusIdByNameAsync(updateAccountRequest.AccountStatus, cancellationToken);
        var industryId = await GetIndustryIdByNameAsync(updateAccountRequest.Industry, cancellationToken);

        account.Name = updateAccountRequest.Name;

        // CRM Business Information
        account.AccountTypeId = accountTypeId;
        account.AccountStatusId = accountStatusId;
        account.IndustryId = industryId;
        account.AnnualRevenue = updateAccountRequest.AnnualRevenue;
        account.NumberOfEmployees = updateAccountRequest.NumberOfEmployees;
        account.Website = updateAccountRequest.Website;
        account.Description = updateAccountRequest.Description;
        account.AccountNumber = updateAccountRequest.AccountNumber;
        account.ParentAccountId = updateAccountRequest.ParentAccountId;
        account.OwnerId = updateAccountRequest.OwnerId;
        account.Rating = updateAccountRequest.Rating;

        // Billing Address Information
        account.BillingAddressLine1 = updateAccountRequest.BillingAddressLine1;
        account.BillingAddressLine2 = updateAccountRequest.BillingAddressLine2;
        account.BillingCity = updateAccountRequest.BillingCity;
        account.BillingState = updateAccountRequest.BillingState;
        account.BillingPostalCode = updateAccountRequest.BillingPostalCode;
        account.BillingCountry = updateAccountRequest.BillingCountry;

        // Shipping Address Information
        account.ShippingAddressLine1 = updateAccountRequest.ShippingAddressLine1;
        account.ShippingAddressLine2 = updateAccountRequest.ShippingAddressLine2;
        account.ShippingCity = updateAccountRequest.ShippingCity;
        account.ShippingState = updateAccountRequest.ShippingState;
        account.ShippingPostalCode = updateAccountRequest.ShippingPostalCode;
        account.ShippingCountry = updateAccountRequest.ShippingCountry;

        // Contact Information
        account.Phone = updateAccountRequest.Phone;
        account.Fax = updateAccountRequest.Fax;
        account.Email = updateAccountRequest.Email;

        account.IsActive = updateAccountRequest.IsActive;
        account.UpdatedAt = DateTime.UtcNow;
        account.UpdatedBy = updatedBy;

        await _context.SaveChangesAsync(cancellationToken);

        // Reload account with navigation properties
        var updatedAccount = await _context.Accounts
            .Include(a => a.AccountType)
            .Include(a => a.AccountStatus)
            .Include(a => a.Industry)
            .FirstOrDefaultAsync(a => a.AccountId == account.AccountId, cancellationToken);

        return updatedAccount!.ToDto(includeContacts: false);
    }

    public async Task<bool> DeleteAsync(Guid accountId, string deletedBy, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == accountId, cancellationToken);

        if (account == null)
        {
            return false;
        }

        // Soft delete
        account.IsActive = false;
        account.UpdatedAt = DateTime.UtcNow;
        account.UpdatedBy = deletedBy;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ExistsAsync(string accountName, Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts
            .AnyAsync(a => a.Name == accountName && a.TenantId == tenantId.ToString(), cancellationToken);
    }

    private async Task<Guid?> GetAccountTypeIdByNameAsync(string? accountTypeName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(accountTypeName))
        {
            return null;
        }

        var accountType = await _context.AccountTypes
            .FirstOrDefaultAsync(at => at.Name == accountTypeName, cancellationToken);

        return accountType?.AccountTypeId;
    }

    private async Task<Guid?> GetAccountStatusIdByNameAsync(string? accountStatusName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(accountStatusName))
        {
            return null;
        }

        var accountStatus = await _context.AccountStatuses
            .FirstOrDefaultAsync(ast => ast.Name == accountStatusName, cancellationToken);

        return accountStatus?.AccountStatusId;
    }

    private async Task<Guid?> GetIndustryIdByNameAsync(string? industryName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(industryName))
        {
            return null;
        }

        var industry = await _context.Industries
            .FirstOrDefaultAsync(i => i.Name == industryName, cancellationToken);

        return industry?.IndustryId;
    }
}
