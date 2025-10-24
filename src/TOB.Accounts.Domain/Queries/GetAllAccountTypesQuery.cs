using MediatR;
using TOB.Accounts.Domain.DTOs;

namespace TOB.Accounts.Domain.Queries;

public class GetAllAccountTypesQuery : IRequest<IEnumerable<AccountTypeDto>>
{
}
