using MediatR;
using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Queries;

public class GetAccountByIdWithContactsQuery : IRequest<AccountDto?>
{
    public Guid AccountId { get; set; }
}
