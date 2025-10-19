using MediatR;
using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Queries;

public class GetAllAccountsWithContactsQuery : IRequest<IEnumerable<AccountDto>>
{
    public Guid TenantId { get; set; }
}
