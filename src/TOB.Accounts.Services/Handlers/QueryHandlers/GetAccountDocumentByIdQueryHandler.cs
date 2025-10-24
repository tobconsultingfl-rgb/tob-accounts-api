using MediatR;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Infrastructure.Repositories;

namespace TOB.Accounts.Services.Handlers.QueryHandlers;

public class GetAccountDocumentByIdQueryHandler : IRequestHandler<GetAccountDocumentByIdQuery, AccountDocumentDto?>
{
    private readonly IAccountDocumentRepository _documentRepository;

    public GetAccountDocumentByIdQueryHandler(IAccountDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<AccountDocumentDto?> Handle(GetAccountDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _documentRepository.GetByIdAsync(request.DocumentId, cancellationToken);
    }
}
