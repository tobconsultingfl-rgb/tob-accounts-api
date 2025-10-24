using MediatR;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Infrastructure.Repositories;

namespace TOB.Accounts.Services.Handlers.QueryHandlers;

public class GetAccountDocumentsQueryHandler : IRequestHandler<GetAccountDocumentsQuery, IEnumerable<AccountDocumentDto>>
{
    private readonly IAccountDocumentRepository _documentRepository;

    public GetAccountDocumentsQueryHandler(IAccountDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<IEnumerable<AccountDocumentDto>> Handle(GetAccountDocumentsQuery request, CancellationToken cancellationToken)
    {
        return await _documentRepository.GetByAccountIdAsync(request.AccountId, cancellationToken);
    }
}
