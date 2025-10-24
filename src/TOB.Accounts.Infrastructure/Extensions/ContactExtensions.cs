using TOB.Accounts.Domain.Models;
using TOB.Accounts.Infrastructure.Data;

namespace TOB.Accounts.Infrastructure.Extensions;

public static class ContactExtensions
{
    /// <summary>
    /// Maps a Contact entity to ContactDto
    /// </summary>
    public static ContactDto ToDto(this Contact contact)
    {
        return new ContactDto
        {
            ContactId = contact.ContactId,
            TenantId = contact.TenantId,
            AccountId = contact.AccountId,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            MiddleName = contact.MiddleName,
            Salutation = contact.Salutation,
            JobTitle = contact.JobTitle,
            Department = contact.Department,
            IsPrimaryContact = contact.IsPrimaryContact,
            ReportsToId = contact.ReportsToId,
            OwnerId = contact.OwnerId,

            // Contact Information
            Email = contact.Email,
            SecondaryEmail = contact.SecondaryEmail,
            PhoneNumber = contact.PhoneNumber,
            MobileNumber = contact.MobileNumber,
            HomePhone = contact.HomePhone,
            OtherPhone = contact.OtherPhone,
            Fax = contact.Fax,
            LinkedIn = contact.LinkedIn,
            Twitter = contact.Twitter,
            Notes = contact.Notes,

            // Mailing Address Information
            MailingAddressLine1 = contact.MailingAddressLine1,
            MailingAddressLine2 = contact.MailingAddressLine2,
            MailingCity = contact.MailingCity,
            MailingState = contact.MailingState,
            MailingPostalCode = contact.MailingPostalCode,
            MailingCountry = contact.MailingCountry,

            // Other Address Information
            OtherAddressLine1 = contact.OtherAddressLine1,
            OtherAddressLine2 = contact.OtherAddressLine2,
            OtherCity = contact.OtherCity,
            OtherState = contact.OtherState,
            OtherPostalCode = contact.OtherPostalCode,
            OtherCountry = contact.OtherCountry,

            Birthdate = contact.Birthdate,

            // Preferences
            DoNotCall = contact.DoNotCall,
            DoNotEmail = contact.DoNotEmail,
            HasOptedOutOfEmail = contact.HasOptedOutOfEmail,

            IsActive = contact.IsActive,
            CreatedAt = contact.CreatedAt,
            CreatedBy = contact.CreatedBy,
            UpdatedAt = contact.UpdatedAt,
            UpdatedBy = contact.UpdatedBy
        };
    }

    /// <summary>
    /// Maps a ContactDto to Contact entity
    /// </summary>
    public static Contact ToEntity(this ContactDto dto)
    {
        return new Contact
        {
            ContactId = dto.ContactId,
            TenantId = dto.TenantId,
            AccountId = dto.AccountId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            Salutation = dto.Salutation,
            JobTitle = dto.JobTitle,
            Department = dto.Department,
            IsPrimaryContact = dto.IsPrimaryContact,
            ReportsToId = dto.ReportsToId,
            OwnerId = dto.OwnerId,

            // Contact Information
            Email = dto.Email,
            SecondaryEmail = dto.SecondaryEmail,
            PhoneNumber = dto.PhoneNumber,
            MobileNumber = dto.MobileNumber,
            HomePhone = dto.HomePhone,
            OtherPhone = dto.OtherPhone,
            Fax = dto.Fax,
            LinkedIn = dto.LinkedIn,
            Twitter = dto.Twitter,
            Notes = dto.Notes,

            // Mailing Address Information
            MailingAddressLine1 = dto.MailingAddressLine1,
            MailingAddressLine2 = dto.MailingAddressLine2,
            MailingCity = dto.MailingCity,
            MailingState = dto.MailingState,
            MailingPostalCode = dto.MailingPostalCode,
            MailingCountry = dto.MailingCountry,

            // Other Address Information
            OtherAddressLine1 = dto.OtherAddressLine1,
            OtherAddressLine2 = dto.OtherAddressLine2,
            OtherCity = dto.OtherCity,
            OtherState = dto.OtherState,
            OtherPostalCode = dto.OtherPostalCode,
            OtherCountry = dto.OtherCountry,

            Birthdate = dto.Birthdate,

            // Preferences
            DoNotCall = dto.DoNotCall,
            DoNotEmail = dto.DoNotEmail,
            HasOptedOutOfEmail = dto.HasOptedOutOfEmail,

            IsActive = dto.IsActive,
            CreatedAt = dto.CreatedAt,
            CreatedBy = dto.CreatedBy,
            UpdatedAt = dto.UpdatedAt,
            UpdatedBy = dto.UpdatedBy
        };
    }

    /// <summary>
    /// Updates an existing Contact entity from ContactDto
    /// </summary>
    public static void UpdateFromDto(this Contact contact, ContactDto dto)
    {
        contact.FirstName = dto.FirstName;
        contact.LastName = dto.LastName;
        contact.MiddleName = dto.MiddleName;
        contact.Salutation = dto.Salutation;
        contact.JobTitle = dto.JobTitle;
        contact.Department = dto.Department;
        contact.IsPrimaryContact = dto.IsPrimaryContact;
        contact.ReportsToId = dto.ReportsToId;
        contact.OwnerId = dto.OwnerId;

        // Contact Information
        contact.Email = dto.Email;
        contact.SecondaryEmail = dto.SecondaryEmail;
        contact.PhoneNumber = dto.PhoneNumber;
        contact.MobileNumber = dto.MobileNumber;
        contact.HomePhone = dto.HomePhone;
        contact.OtherPhone = dto.OtherPhone;
        contact.Fax = dto.Fax;
        contact.LinkedIn = dto.LinkedIn;
        contact.Twitter = dto.Twitter;
        contact.Notes = dto.Notes;

        // Mailing Address Information
        contact.MailingAddressLine1 = dto.MailingAddressLine1;
        contact.MailingAddressLine2 = dto.MailingAddressLine2;
        contact.MailingCity = dto.MailingCity;
        contact.MailingState = dto.MailingState;
        contact.MailingPostalCode = dto.MailingPostalCode;
        contact.MailingCountry = dto.MailingCountry;

        // Other Address Information
        contact.OtherAddressLine1 = dto.OtherAddressLine1;
        contact.OtherAddressLine2 = dto.OtherAddressLine2;
        contact.OtherCity = dto.OtherCity;
        contact.OtherState = dto.OtherState;
        contact.OtherPostalCode = dto.OtherPostalCode;
        contact.OtherCountry = dto.OtherCountry;

        contact.Birthdate = dto.Birthdate;

        // Preferences
        contact.DoNotCall = dto.DoNotCall;
        contact.DoNotEmail = dto.DoNotEmail;
        contact.HasOptedOutOfEmail = dto.HasOptedOutOfEmail;

        contact.IsActive = dto.IsActive;
        // Note: TenantId, AccountId, and ContactId should not be updated
        // Audit fields will be updated by DbContext
    }

    /// <summary>
    /// Maps a collection of Contact entities to ContactDto collection
    /// </summary>
    public static IEnumerable<ContactDto> ToDtoList(this IEnumerable<Contact> contacts)
    {
        return contacts.Select(c => c.ToDto());
    }
}
