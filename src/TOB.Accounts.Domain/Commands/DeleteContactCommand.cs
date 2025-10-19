using MediatR;

namespace TOB.Accounts.Domain.Commands;

public class DeleteContactCommand : IRequest<bool>
{
    public Guid ContactId { get; set; }
    public required string DeletedBy { get; set; }
}
