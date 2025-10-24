using MediatR;
using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Commands;

public class CreateContactCommand : IRequest<ContactDto>
{
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

    public required string CreatedBy { get; set; }
}
