using MediatR;

namespace TOB.Accounts.Domain.Queries;

public class DownloadAccountDocumentQuery : IRequest<DownloadAccountDocumentResult?>
{
    public Guid DocumentId { get; set; }
}

public class DownloadAccountDocumentResult
{
    public required Stream FileStream { get; set; }
    public required string FileName { get; set; }
    public required string ContentType { get; set; }
    public long FileSizeBytes { get; set; }
}
