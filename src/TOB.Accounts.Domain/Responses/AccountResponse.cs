using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Responses;

public class AccountResponse
{
    public Guid AccountId { get; set; }
    public required string TenantId { get; set; }
    public required string Name { get; set; }

    // Address Information
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }

    // Primary Contact Information
    public string? PrimaryContactName { get; set; }
    public string? PrimaryContactEmail { get; set; }
    public string? PrimaryContactPhone { get; set; }

    // Related Contacts
    public ICollection<ContactResponse>? Contacts { get; set; }

    // Audit fields
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    public static AccountResponse FromDto(AccountDto dto, bool includeContacts = false)
    {
        var response = new AccountResponse
        {
            AccountId = dto.AccountId,
            TenantId = dto.TenantId,
            Name = dto.Name,
            AddressLine1 = dto.AddressLine1,
            AddressLine2 = dto.AddressLine2,
            City = dto.City,
            State = dto.State,
            PostalCode = dto.PostalCode,
            Country = dto.Country,
            PrimaryContactName = dto.PrimaryContactName,
            PrimaryContactEmail = dto.PrimaryContactEmail,
            PrimaryContactPhone = dto.PrimaryContactPhone,
            CreatedAt = dto.CreatedAt,
            CreatedBy = dto.CreatedBy,
            UpdatedAt = dto.UpdatedAt,
            UpdatedBy = dto.UpdatedBy
        };

        if (includeContacts && dto.Contacts != null)
        {
            response.Contacts = dto.Contacts.Select(ContactResponse.FromDto).ToList();
        }

        return response;
    }
}
