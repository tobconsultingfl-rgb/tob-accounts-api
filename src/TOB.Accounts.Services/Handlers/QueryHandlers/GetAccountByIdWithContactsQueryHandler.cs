using MediatR;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Infrastructure.Repositories;

namespace TOB.Accounts.Services.Handlers.QueryHandlers;

public class GetAccountByIdWithContactsQueryHandler : IRequestHandler<GetAccountByIdWithContactsQuery, AccountDto?>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountByIdWithContactsQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountDto?> Handle(GetAccountByIdWithContactsQuery request, CancellationToken cancellationToken)
    {
        return await _accountRepository.GetByIdWithContactsAsync(request.AccountId, cancellationToken);
    }
}
