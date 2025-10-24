using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Responses;

public class AccountResponse
{
    public Guid AccountId { get; set; }
    public required string TenantId { get; set; }
    public required string Name { get; set; }

    // CRM Business Information
    public string? AccountType { get; set; }
    public string? AccountStatus { get; set; }
    public string? Industry { get; set; }
    public decimal? AnnualRevenue { get; set; }
    public int? NumberOfEmployees { get; set; }
    public string? Website { get; set; }
    public string? Description { get; set; }
    public string? AccountNumber { get; set; }
    public Guid? ParentAccountId { get; set; }
    public Guid? OwnerId { get; set; }
    public string? Rating { get; set; }

    // Billing Address Information
    public string? BillingAddressLine1 { get; set; }
    public string? BillingAddressLine2 { get; set; }
    public string? BillingCity { get; set; }
    public string? BillingState { get; set; }
    public string? BillingPostalCode { get; set; }
    public string? BillingCountry { get; set; }

    // Shipping Address Information
    public string? ShippingAddressLine1 { get; set; }
    public string? ShippingAddressLine2 { get; set; }
    public string? ShippingCity { get; set; }
    public string? ShippingState { get; set; }
    public string? ShippingPostalCode { get; set; }
    public string? ShippingCountry { get; set; }

    // Contact Information
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }

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
            AccountType = dto.AccountType,
            AccountStatus = dto.AccountStatus,
            Industry = dto.Industry,
            AnnualRevenue = dto.AnnualRevenue,
            NumberOfEmployees = dto.NumberOfEmployees,
            Website = dto.Website,
            Description = dto.Description,
            AccountNumber = dto.AccountNumber,
            ParentAccountId = dto.ParentAccountId,
            OwnerId = dto.OwnerId,
            Rating = dto.Rating,
            BillingAddressLine1 = dto.BillingAddressLine1,
            BillingAddressLine2 = dto.BillingAddressLine2,
            BillingCity = dto.BillingCity,
            BillingState = dto.BillingState,
            BillingPostalCode = dto.BillingPostalCode,
            BillingCountry = dto.BillingCountry,
            ShippingAddressLine1 = dto.ShippingAddressLine1,
            ShippingAddressLine2 = dto.ShippingAddressLine2,
            ShippingCity = dto.ShippingCity,
            ShippingState = dto.ShippingState,
            ShippingPostalCode = dto.ShippingPostalCode,
            ShippingCountry = dto.ShippingCountry,
            Phone = dto.Phone,
            Fax = dto.Fax,
            Email = dto.Email,
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
