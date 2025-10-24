using MediatR;
using TOB.Accounts.Domain.Commands;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Infrastructure.Repositories;
using TOB.Accounts.Infrastructure.Services;

namespace TOB.Accounts.Services.Handlers.CommandHandlers;

public class UploadAccountDocumentCommandHandler : IRequestHandler<UploadAccountDocumentCommand, AccountDocumentDto>
{
    private readonly IAccountDocumentRepository _documentRepository;
    private readonly IBlobStorageService _blobStorageService;
    private const string ContainerName = "account-documents";

    public UploadAccountDocumentCommandHandler(
        IAccountDocumentRepository documentRepository,
        IBlobStorageService blobStorageService)
    {
        _documentRepository = documentRepository;
        _blobStorageService = blobStorageService;
    }

    public async Task<AccountDocumentDto> Handle(UploadAccountDocumentCommand request, CancellationToken cancellationToken)
    {
        // Generate unique blob name
        var blobName = $"{request.AccountId}/{Guid.NewGuid()}_{request.FileName}";

        // Upload file to blob storage
        var blobUrl = await _blobStorageService.UploadFileAsync(
            ContainerName,
            blobName,
            request.FileStream,
            request.ContentType,
            cancellationToken);

        // Create document record
        var documentDto = new AccountDocumentDto
        {
            DocumentId = Guid.NewGuid(),
            TenantId = request.TenantId,
            AccountId = request.AccountId,
            FileName = request.FileName,
            BlobUrl = blobUrl,
            ContentType = request.ContentType,
            FileSizeBytes = request.FileSizeBytes,
            Category = request.Category,
            Description = request.Description,
            BlobContainer = ContainerName,
            BlobName = blobName,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        return await _documentRepository.CreateAsync(documentDto, cancellationToken);
    }
}
