using MediatR;
using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Queries;

public class GetAccountDocumentsQuery : IRequest<IEnumerable<AccountDocumentDto>>
{
    public Guid AccountId { get; set; }
}
