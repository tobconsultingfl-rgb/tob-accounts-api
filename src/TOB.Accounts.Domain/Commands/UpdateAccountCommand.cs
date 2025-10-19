using MediatR;
using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Commands;

public class UpdateAccountCommand : IRequest<AccountDto?>
{
    public Guid AccountId { get; set; }
    public required string Name { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? PrimaryContactName { get; set; }
    public string? PrimaryContactEmail { get; set; }
    public string? PrimaryContactPhone { get; set; }
    public bool IsActive { get; set; }
    public required string UpdatedBy { get; set; }
}
