using MediatR;
using TOB.Accounts.Domain.Models;

namespace TOB.Accounts.Domain.Commands;

public class UploadAccountDocumentCommand : IRequest<AccountDocumentDto>
{
    public required string TenantId { get; set; }
    public Guid AccountId { get; set; }
    public required string FileName { get; set; }
    public required string ContentType { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public required Stream FileStream { get; set; }
    public long FileSizeBytes { get; set; }
    public required string CreatedBy { get; set; }
}
