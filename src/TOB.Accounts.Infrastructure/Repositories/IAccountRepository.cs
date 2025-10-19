using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Requests;

namespace TOB.Accounts.Infrastructure.Repositories;

public interface IAccountRepository
{
    /// <summary>
    /// Get all accounts for a tenant
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of accounts</returns>
    Task<IEnumerable<AccountDto>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get account by ID
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Account if found, null otherwise</returns>
    Task<AccountDto?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get accounts with contacts included
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of accounts with contacts</returns>
    Task<IEnumerable<AccountDto>> GetAllWithContactsAsync(Guid tenantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get account by ID with contacts included
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Account with contacts if found, null otherwise</returns>
    Task<AccountDto?> GetByIdWithContactsAsync(Guid accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new account
    /// </summary>
    /// <param name="createAccountRequest">Account creation request</param>
    /// <param name="createdBy">User creating the account</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created account</returns>
    Task<AccountDto> CreateAsync(CreateAccountRequest createAccountRequest, string createdBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing account
    /// </summary>
    /// <param name="updateAccountRequest">Account update request</param>
    /// <param name="updatedBy">User updating the account</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated account</returns>
    Task<AccountDto?> UpdateAsync(UpdateAccountRequest updateAccountRequest, string updatedBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an account (soft delete by setting IsActive to false)
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <param name="deletedBy">User deleting the account</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid accountId, string deletedBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if account exists
    /// </summary>
    /// <param name="accountName">Account Name</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if exists, false otherwise</returns>
    Task<bool> ExistsAsync(string accountName, Guid tenantId, CancellationToken cancellationToken = default);
}
