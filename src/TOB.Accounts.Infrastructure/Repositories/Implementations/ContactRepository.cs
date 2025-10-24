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

    public async Task<IEnumerable<ContactDto>> GetAllAsync(Guid accountId, CancellationToken cancellationToken = default)
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
            MiddleName = createContactRequest.MiddleName,
            Salutation = createContactRequest.Salutation,
            JobTitle = createContactRequest.JobTitle,
            Department = createContactRequest.Department,
            IsPrimaryContact = createContactRequest.IsPrimaryContact,
            ReportsToId = createContactRequest.ReportsToId,
            OwnerId = createContactRequest.OwnerId,

            // Contact Information
            Email = createContactRequest.Email,
            SecondaryEmail = createContactRequest.SecondaryEmail,
            PhoneNumber = createContactRequest.PhoneNumber,
            MobileNumber = createContactRequest.MobileNumber,
            HomePhone = createContactRequest.HomePhone,
            OtherPhone = createContactRequest.OtherPhone,
            Fax = createContactRequest.Fax,
            LinkedIn = createContactRequest.LinkedIn,
            Twitter = createContactRequest.Twitter,
            Notes = createContactRequest.Notes,

            // Mailing Address Information
            MailingAddressLine1 = createContactRequest.MailingAddressLine1,
            MailingAddressLine2 = createContactRequest.MailingAddressLine2,
            MailingCity = createContactRequest.MailingCity,
            MailingState = createContactRequest.MailingState,
            MailingPostalCode = createContactRequest.MailingPostalCode,
            MailingCountry = createContactRequest.MailingCountry,

            // Other Address Information
            OtherAddressLine1 = createContactRequest.OtherAddressLine1,
            OtherAddressLine2 = createContactRequest.OtherAddressLine2,
            OtherCity = createContactRequest.OtherCity,
            OtherState = createContactRequest.OtherState,
            OtherPostalCode = createContactRequest.OtherPostalCode,
            OtherCountry = createContactRequest.OtherCountry,

            Birthdate = createContactRequest.Birthdate,

            // Preferences
            DoNotCall = createContactRequest.DoNotCall,
            DoNotEmail = createContactRequest.DoNotEmail,
            HasOptedOutOfEmail = createContactRequest.HasOptedOutOfEmail,

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
        contact.MiddleName = updateContactRequest.MiddleName;
        contact.Salutation = updateContactRequest.Salutation;
        contact.JobTitle = updateContactRequest.JobTitle;
        contact.Department = updateContactRequest.Department;
        contact.IsPrimaryContact = updateContactRequest.IsPrimaryContact;
        contact.ReportsToId = updateContactRequest.ReportsToId;
        contact.OwnerId = updateContactRequest.OwnerId;

        // Contact Information
        contact.Email = updateContactRequest.Email;
        contact.SecondaryEmail = updateContactRequest.SecondaryEmail;
        contact.PhoneNumber = updateContactRequest.PhoneNumber;
        contact.MobileNumber = updateContactRequest.MobileNumber;
        contact.HomePhone = updateContactRequest.HomePhone;
        contact.OtherPhone = updateContactRequest.OtherPhone;
        contact.Fax = updateContactRequest.Fax;
        contact.LinkedIn = updateContactRequest.LinkedIn;
        contact.Twitter = updateContactRequest.Twitter;
        contact.Notes = updateContactRequest.Notes;

        // Mailing Address Information
        contact.MailingAddressLine1 = updateContactRequest.MailingAddressLine1;
        contact.MailingAddressLine2 = updateContactRequest.MailingAddressLine2;
        contact.MailingCity = updateContactRequest.MailingCity;
        contact.MailingState = updateContactRequest.MailingState;
        contact.MailingPostalCode = updateContactRequest.MailingPostalCode;
        contact.MailingCountry = updateContactRequest.MailingCountry;

        // Other Address Information
        contact.OtherAddressLine1 = updateContactRequest.OtherAddressLine1;
        contact.OtherAddressLine2 = updateContactRequest.OtherAddressLine2;
        contact.OtherCity = updateContactRequest.OtherCity;
        contact.OtherState = updateContactRequest.OtherState;
        contact.OtherPostalCode = updateContactRequest.OtherPostalCode;
        contact.OtherCountry = updateContactRequest.OtherCountry;

        contact.Birthdate = updateContactRequest.Birthdate;

        // Preferences
        contact.DoNotCall = updateContactRequest.DoNotCall;
        contact.DoNotEmail = updateContactRequest.DoNotEmail;
        contact.HasOptedOutOfEmail = updateContactRequest.HasOptedOutOfEmail;

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
