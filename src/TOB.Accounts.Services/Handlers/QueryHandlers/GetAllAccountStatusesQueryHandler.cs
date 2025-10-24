using MediatR;
using TOB.Accounts.Domain.DTOs;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Infrastructure.Repositories;

namespace TOB.Accounts.Services.Handlers.QueryHandlers;

public class GetAllAccountStatusesQueryHandler : IRequestHandler<GetAllAccountStatusesQuery, IEnumerable<AccountStatusDto>>
{
    private readonly IAccountStatusRepository _accountStatusRepository;

    public GetAllAccountStatusesQueryHandler(IAccountStatusRepository accountStatusRepository)
    {
        _accountStatusRepository = accountStatusRepository;
    }

    public async Task<IEnumerable<AccountStatusDto>> Handle(GetAllAccountStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _accountStatusRepository.GetAllAsync(cancellationToken);
    }
}
