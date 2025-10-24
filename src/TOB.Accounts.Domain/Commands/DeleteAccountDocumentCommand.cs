using MediatR;

namespace TOB.Accounts.Domain.Commands;

public class DeleteAccountDocumentCommand : IRequest<bool>
{
    public Guid DocumentId { get; set; }
    public required string DeletedBy { get; set; }
}
