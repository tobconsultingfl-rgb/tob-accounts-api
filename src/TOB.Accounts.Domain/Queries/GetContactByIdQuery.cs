using MediatR;
using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Queries;

public class GetContactByIdQuery : IRequest<ContactDto?>
{
    public Guid ContactId { get; set; }
}
