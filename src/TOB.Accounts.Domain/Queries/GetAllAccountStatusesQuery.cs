using MediatR;
using TOB.Accounts.Domain.DTOs;

namespace TOB.Accounts.Domain.Queries;

public class GetAllAccountStatusesQuery : IRequest<IEnumerable<AccountStatusDto>>
{
}
