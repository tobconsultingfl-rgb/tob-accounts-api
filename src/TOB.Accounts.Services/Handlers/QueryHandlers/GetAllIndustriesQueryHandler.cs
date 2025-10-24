using MediatR;
using TOB.Accounts.Domain.DTOs;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Infrastructure.Repositories;

namespace TOB.Accounts.Services.Handlers.QueryHandlers;

public class GetAllIndustriesQueryHandler : IRequestHandler<GetAllIndustriesQuery, IEnumerable<IndustryDto>>
{
    private readonly IIndustryRepository _industryRepository;

    public GetAllIndustriesQueryHandler(IIndustryRepository industryRepository)
    {
        _industryRepository = industryRepository;
    }

    public async Task<IEnumerable<IndustryDto>> Handle(GetAllIndustriesQuery request, CancellationToken cancellationToken)
    {
        return await _industryRepository.GetAllAsync(cancellationToken);
    }
}
