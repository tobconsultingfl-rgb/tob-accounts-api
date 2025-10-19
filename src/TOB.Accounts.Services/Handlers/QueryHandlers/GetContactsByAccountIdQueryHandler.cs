using MediatR;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Infrastructure.Repositories;

namespace TOB.Accounts.Services.Handlers.QueryHandlers;

public class GetContactsByAccountIdQueryHandler : IRequestHandler<GetContactsByAccountIdQuery, IEnumerable<ContactDto>>
{
    private readonly IContactRepository _contactRepository;

    public GetContactsByAccountIdQueryHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<IEnumerable<ContactDto>> Handle(GetContactsByAccountIdQuery request, CancellationToken cancellationToken)
    {
        return await _contactRepository.GetByAccountIdAsync(request.AccountId, cancellationToken);
    }
}
