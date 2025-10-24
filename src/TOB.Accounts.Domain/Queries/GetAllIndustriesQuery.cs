using MediatR;
using TOB.Accounts.Domain.DTOs;

namespace TOB.Accounts.Domain.Queries;

public class GetAllIndustriesQuery : IRequest<IEnumerable<IndustryDto>>
{
}
