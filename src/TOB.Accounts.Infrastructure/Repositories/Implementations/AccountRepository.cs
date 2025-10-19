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
            .Where(a => a.TenantId == tenantId.ToString())
            .ToListAsync(cancellationToken);

        return accounts.ToDtoList(includeContacts: false);
    }

    public async Task<AccountDto?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == accountId, cancellationToken);

        return account?.ToDto(includeContacts: false);
    }

    public async Task<IEnumerable<AccountDto>> GetAllWithContactsAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        var accounts = await _context.Accounts
            .Include(a => a.Contacts)
            .Where(a => a.TenantId == tenantId.ToString())
            .ToListAsync(cancellationToken);

        return accounts.ToDtoList(includeContacts: true);
    }

    public async Task<AccountDto?> GetByIdWithContactsAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .Include(a => a.Contacts)
            .FirstOrDefaultAsync(a => a.AccountId == accountId, cancellationToken);

        return account?.ToDto(includeContacts: true);
    }

    public async Task<AccountDto> CreateAsync(CreateAccountRequest createAccountRequest, string createdBy, CancellationToken cancellationToken = default)
    {
        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            TenantId = createAccountRequest.TenantId,
            Name = createAccountRequest.Name,
            AddressLine1 = createAccountRequest.AddressLine1,
            AddressLine2 = createAccountRequest.AddressLine2,
            City = createAccountRequest.City,
            State = createAccountRequest.State,
            PostalCode = createAccountRequest.PostalCode,
            Country = createAccountRequest.Country,
            PrimaryContactName = createAccountRequest.PrimaryContactName,
            PrimaryContactEmail = createAccountRequest.PrimaryContactEmail,
            PrimaryContactPhone = createAccountRequest.PrimaryContactPhone,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync(cancellationToken);

        return account.ToDto(includeContacts: false);
    }

    public async Task<AccountDto?> UpdateAsync(UpdateAccountRequest updateAccountRequest, string updatedBy, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == updateAccountRequest.AccountId, cancellationToken);

        if (account == null)
        {
            return null;
        }

        account.Name = updateAccountRequest.Name;
        account.AddressLine1 = updateAccountRequest.AddressLine1;
        account.AddressLine2 = updateAccountRequest.AddressLine2;
        account.City = updateAccountRequest.City;
        account.State = updateAccountRequest.State;
        account.PostalCode = updateAccountRequest.PostalCode;
        account.Country = updateAccountRequest.Country;
        account.PrimaryContactName = updateAccountRequest.PrimaryContactName;
        account.PrimaryContactEmail = updateAccountRequest.PrimaryContactEmail;
        account.PrimaryContactPhone = updateAccountRequest.PrimaryContactPhone;
        account.IsActive = updateAccountRequest.IsActive;
        account.UpdatedAt = DateTime.UtcNow;
        account.UpdatedBy = updatedBy;

        await _context.SaveChangesAsync(cancellationToken);

        return account.ToDto(includeContacts: false);
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
}
