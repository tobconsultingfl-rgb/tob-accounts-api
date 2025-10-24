using MediatR;
using TOB.Accounts.Domain.DTOs;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Infrastructure.Repositories;

namespace TOB.Accounts.Services.Handlers.QueryHandlers;

public class GetAllAccountTypesQueryHandler : IRequestHandler<GetAllAccountTypesQuery, IEnumerable<AccountTypeDto>>
{
    private readonly IAccountTypeRepository _accountTypeRepository;

    public GetAllAccountTypesQueryHandler(IAccountTypeRepository accountTypeRepository)
    {
        _accountTypeRepository = accountTypeRepository;
    }

    public async Task<IEnumerable<AccountTypeDto>> Handle(GetAllAccountTypesQuery request, CancellationToken cancellationToken)
    {
        return await _accountTypeRepository.GetAllAsync(cancellationToken);
    }
}
