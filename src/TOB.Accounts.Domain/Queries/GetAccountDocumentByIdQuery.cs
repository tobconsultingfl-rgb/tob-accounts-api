using MediatR;
using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Queries;

public class GetAccountDocumentByIdQuery : IRequest<AccountDocumentDto?>
{
    public Guid DocumentId { get; set; }
}
