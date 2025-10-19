using MediatR;

namespace TOB.Accounts.Domain.Commands;

public class DeleteAccountCommand : IRequest<bool>
{
    public Guid AccountId { get; set; }
    public required string DeletedBy { get; set; }
}
