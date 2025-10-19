using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Requests;

namespace TOB.Accounts.Infrastructure.Repositories;

public interface IContactRepository
{
    /// <summary>
    /// Get all contacts for a specific account
    /// </summary>
    /// <param name="accountId">Account ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of contacts for the account</returns>
    Task<IEnumerable<ContactDto>> GetAllAsync(Guid accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get contact by ID
    /// </summary>
    /// <param name="contactId">Contact ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Contact if found, null otherwise</returns>
    Task<ContactDto?> GetByIdAsync(Guid contactId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new contact
    /// </summary>
    /// <param name="createAccountRequest">Contact creation request</param>
    /// <param name="createdBy">User creating the contact</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created contact</returns>
    Task<ContactDto> CreateAsync(CreateContactRequest createAccountRequest, string createdBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing contact
    /// </summary>
    /// <param name="updateContactRequest">Contact update request</param>
    /// <param name="updatedBy">User updating the contact</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated contact</returns>
    Task<ContactDto?> UpdateAsync(UpdateContactRequest updateContactRequest, string updatedBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a contact (soft delete by setting IsActive to false)
    /// </summary>
    /// <param name="contactId">Contact ID</param>
    /// <param name="deletedBy">User deleting the contact</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid contactId, string deletedBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if contact exists
    /// </summary>
    /// <param name="contactId">Contact ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if exists, false otherwise</returns>
    Task<bool> ExistsAsync(Guid contactId, CancellationToken cancellationToken = default);
}
