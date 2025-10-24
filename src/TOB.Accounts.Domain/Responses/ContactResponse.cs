using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Responses;

public class ContactResponse
{
    public Guid ContactId { get; set; }
    public required string TenantId { get; set; }
    public Guid AccountId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Salutation { get; set; }
    public string? JobTitle { get; set; }
    public string? Department { get; set; }
    public bool IsPrimaryContact { get; set; }
    public Guid? ReportsToId { get; set; }
    public Guid? OwnerId { get; set; }

    // Contact Information
    public string? Email { get; set; }
    public string? SecondaryEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? HomePhone { get; set; }
    public string? OtherPhone { get; set; }
    public string? Fax { get; set; }
    public string? LinkedIn { get; set; }
    public string? Twitter { get; set; }
    public string? Notes { get; set; }

    // Mailing Address Information
    public string? MailingAddressLine1 { get; set; }
    public string? MailingAddressLine2 { get; set; }
    public string? MailingCity { get; set; }
    public string? MailingState { get; set; }
    public string? MailingPostalCode { get; set; }
    public string? MailingCountry { get; set; }

    // Other Address Information
    public string? OtherAddressLine1 { get; set; }
    public string? OtherAddressLine2 { get; set; }
    public string? OtherCity { get; set; }
    public string? OtherState { get; set; }
    public string? OtherPostalCode { get; set; }
    public string? OtherCountry { get; set; }

    public DateTime? Birthdate { get; set; }

    // Preferences
    public bool DoNotCall { get; set; }
    public bool DoNotEmail { get; set; }
    public bool HasOptedOutOfEmail { get; set; }

    // Audit fields
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    public static ContactResponse FromDto(ContactDto dto)
    {
        return new ContactResponse
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
            MailingAddressLine1 = dto.MailingAddressLine1,
            MailingAddressLine2 = dto.MailingAddressLine2,
            MailingCity = dto.MailingCity,
            MailingState = dto.MailingState,
            MailingPostalCode = dto.MailingPostalCode,
            MailingCountry = dto.MailingCountry,
            OtherAddressLine1 = dto.OtherAddressLine1,
            OtherAddressLine2 = dto.OtherAddressLine2,
            OtherCity = dto.OtherCity,
            OtherState = dto.OtherState,
            OtherPostalCode = dto.OtherPostalCode,
            OtherCountry = dto.OtherCountry,
            Birthdate = dto.Birthdate,
            DoNotCall = dto.DoNotCall,
            DoNotEmail = dto.DoNotEmail,
            HasOptedOutOfEmail = dto.HasOptedOutOfEmail,
            CreatedAt = dto.CreatedAt,
            CreatedBy = dto.CreatedBy,
            UpdatedAt = dto.UpdatedAt,
            UpdatedBy = dto.UpdatedBy
        };
    }
}
