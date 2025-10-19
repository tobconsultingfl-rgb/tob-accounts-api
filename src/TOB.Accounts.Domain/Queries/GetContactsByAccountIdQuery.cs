using MediatR;
using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Queries;

public class GetContactsByAccountIdQuery : IRequest<IEnumerable<ContactDto>>
{
    public Guid AccountId { get; set; }
}
