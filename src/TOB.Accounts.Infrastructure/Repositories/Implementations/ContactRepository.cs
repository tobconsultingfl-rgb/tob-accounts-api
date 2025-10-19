using Microsoft.EntityFrameworkCore;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Requests;
using TOB.Accounts.Infrastructure.Data;
using TOB.Accounts.Infrastructure.Extensions;

namespace TOB.Accounts.Infrastructure.Repositories.Implementations;

public class ContactRepository : IContactRepository
{
    private readonly AccountsDbContext _context;

    public ContactRepository(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContactDto>> GetAllAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        var contacts = await _context.Contacts
            .Where(c => c.TenantId == tenantId.ToString())
            .ToListAsync(cancellationToken);

        return contacts.ToDtoList();
    }

    public async Task<IEnumerable<ContactDto>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        var contacts = await _context.Contacts
            .Where(c => c.AccountId == accountId)
            .ToListAsync(cancellationToken);

        return contacts.ToDtoList();
    }

    public async Task<ContactDto?> GetByIdAsync(Guid contactId, CancellationToken cancellationToken = default)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.ContactId == contactId, cancellationToken);

        return contact?.ToDto();
    }

    public async Task<ContactDto> CreateAsync(CreateContactRequest createContactRequest, string createdBy, CancellationToken cancellationToken = default)
    {
        var contact = new Contact
        {
            ContactId = Guid.NewGuid(),
            TenantId = createContactRequest.TenantId,
            AccountId = createContactRequest.AccountId,
            FirstName = createContactRequest.FirstName,
            LastName = createContactRequest.LastName,
            Email = createContactRequest.Email,
            PhoneNumber = createContactRequest.PhoneNumber,
            MobileNumber = createContactRequest.MobileNumber,
            AddressLine1 = createContactRequest.AddressLine1,
            AddressLine2 = createContactRequest.AddressLine2,
            City = createContactRequest.City,
            State = createContactRequest.State,
            PostalCode = createContactRequest.PostalCode,
            Country = createContactRequest.Country,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy
        };

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync(cancellationToken);

        return contact.ToDto();
    }

    public async Task<ContactDto?> UpdateAsync(UpdateContactRequest updateContactRequest, string updatedBy, CancellationToken cancellationToken = default)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.ContactId == updateContactRequest.ContactId, cancellationToken);

        if (contact == null)
        {
            return null;
        }

        contact.FirstName = updateContactRequest.FirstName;
        contact.LastName = updateContactRequest.LastName;
        contact.Email = updateContactRequest.Email;
        contact.PhoneNumber = updateContactRequest.PhoneNumber;
        contact.MobileNumber = updateContactRequest.MobileNumber;
        contact.AddressLine1 = updateContactRequest.AddressLine1;
        contact.AddressLine2 = updateContactRequest.AddressLine2;
        contact.City = updateContactRequest.City;
        contact.State = updateContactRequest.State;
        contact.PostalCode = updateContactRequest.PostalCode;
        contact.Country = updateContactRequest.Country;
        contact.IsActive = updateContactRequest.IsActive;
        contact.UpdatedAt = DateTime.UtcNow;
        contact.UpdatedBy = updatedBy;

        await _context.SaveChangesAsync(cancellationToken);

        return contact.ToDto();
    }

    public async Task<bool> DeleteAsync(Guid contactId, string deletedBy, CancellationToken cancellationToken = default)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.ContactId == contactId, cancellationToken);

        if (contact == null)
        {
            return false;
        }

        // Soft delete
        contact.IsActive = false;
        contact.UpdatedAt = DateTime.UtcNow;
        contact.UpdatedBy = deletedBy;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ExistsAsync(Guid contactId, CancellationToken cancellationToken = default)
    {
        return await _context.Contacts
            .AnyAsync(c => c.ContactId == contactId, cancellationToken);
    }
}
