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
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber,
            MobileNumber = contact.MobileNumber,
            AddressLine1 = contact.AddressLine1,
            AddressLine2 = contact.AddressLine2,
            City = contact.City,
            State = contact.State,
            PostalCode = contact.PostalCode,
            Country = contact.Country,
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
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            MobileNumber = dto.MobileNumber,
            AddressLine1 = dto.AddressLine1,
            AddressLine2 = dto.AddressLine2,
            City = dto.City,
            State = dto.State,
            PostalCode = dto.PostalCode,
            Country = dto.Country,
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
        contact.Email = dto.Email;
        contact.PhoneNumber = dto.PhoneNumber;
        contact.MobileNumber = dto.MobileNumber;
        contact.AddressLine1 = dto.AddressLine1;
        contact.AddressLine2 = dto.AddressLine2;
        contact.City = dto.City;
        contact.State = dto.State;
        contact.PostalCode = dto.PostalCode;
        contact.Country = dto.Country;
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
