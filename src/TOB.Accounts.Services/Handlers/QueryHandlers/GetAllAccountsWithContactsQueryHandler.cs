using MediatR;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Infrastructure.Repositories;

namespace TOB.Accounts.Services.Handlers.QueryHandlers;

public class GetAllAccountsWithContactsQueryHandler : IRequestHandler<GetAllAccountsWithContactsQuery, IEnumerable<AccountDto>>
{
    private readonly IAccountRepository _accountRepository;

    public GetAllAccountsWithContactsQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<AccountDto>> Handle(GetAllAccountsWithContactsQuery request, CancellationToken cancellationToken)
    {
        return await _accountRepository.GetAllWithContactsAsync(request.TenantId, cancellationToken);
    }
}
