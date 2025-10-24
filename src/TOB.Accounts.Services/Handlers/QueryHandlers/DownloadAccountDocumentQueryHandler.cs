using MediatR;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Infrastructure.Repositories;
using TOB.Accounts.Infrastructure.Services;

namespace TOB.Accounts.Services.Handlers.QueryHandlers;

public class DownloadAccountDocumentQueryHandler : IRequestHandler<DownloadAccountDocumentQuery, DownloadAccountDocumentResult?>
{
    private readonly IAccountDocumentRepository _documentRepository;
    private readonly IBlobStorageService _blobStorageService;

    public DownloadAccountDocumentQueryHandler(
        IAccountDocumentRepository documentRepository,
        IBlobStorageService blobStorageService)
    {
        _documentRepository = documentRepository;
        _blobStorageService = blobStorageService;
    }

    public async Task<DownloadAccountDocumentResult?> Handle(DownloadAccountDocumentQuery request, CancellationToken cancellationToken)
    {
        // Get document metadata
        var document = await _documentRepository.GetByIdAsync(request.DocumentId, cancellationToken);

        if (document == null || string.IsNullOrWhiteSpace(document.BlobContainer) || string.IsNullOrWhiteSpace(document.BlobName))
        {
            return null;
        }

        // Download file from blob storage
        var fileStream = await _blobStorageService.DownloadFileAsync(
            document.BlobContainer,
            document.BlobName,
            cancellationToken);

        return new DownloadAccountDocumentResult
        {
            FileStream = fileStream,
            FileName = document.FileName,
            ContentType = document.ContentType,
            FileSizeBytes = document.FileSizeBytes
        };
    }
}
