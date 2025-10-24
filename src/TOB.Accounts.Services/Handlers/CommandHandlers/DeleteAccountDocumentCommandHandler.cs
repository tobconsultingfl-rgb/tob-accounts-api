using MediatR;
using TOB.Accounts.Domain.Commands;
using TOB.Accounts.Infrastructure.Repositories;
using TOB.Accounts.Infrastructure.Services;

namespace TOB.Accounts.Services.Handlers.CommandHandlers;

public class DeleteAccountDocumentCommandHandler : IRequestHandler<DeleteAccountDocumentCommand, bool>
{
    private readonly IAccountDocumentRepository _documentRepository;
    private readonly IBlobStorageService _blobStorageService;

    public DeleteAccountDocumentCommandHandler(
        IAccountDocumentRepository documentRepository,
        IBlobStorageService blobStorageService)
    {
        _documentRepository = documentRepository;
        _blobStorageService = blobStorageService;
    }

    public async Task<bool> Handle(DeleteAccountDocumentCommand request, CancellationToken cancellationToken)
    {
        // Get document metadata
        var document = await _documentRepository.GetByIdAsync(request.DocumentId, cancellationToken);

        if (document == null)
        {
            return false;
        }

        // Delete from blob storage
        if (!string.IsNullOrWhiteSpace(document.BlobContainer) && !string.IsNullOrWhiteSpace(document.BlobName))
        {
            await _blobStorageService.DeleteFileAsync(document.BlobContainer, document.BlobName, cancellationToken);
        }

        // Soft delete document record
        return await _documentRepository.DeleteAsync(request.DocumentId, request.DeletedBy, cancellationToken);
    }
}
