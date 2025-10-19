using MediatR;
using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Commands;

public class CreateContactCommand : IRequest<ContactDto>
{
    public required string TenantId { get; set; }
    public Guid AccountId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public required string CreatedBy { get; set; }
}
