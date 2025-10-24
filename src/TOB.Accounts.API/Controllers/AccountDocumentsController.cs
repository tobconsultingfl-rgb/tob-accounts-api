using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TOB.Accounts.Domain.Commands;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Domain.Responses;

namespace TOB.Accounts.API.Controllers;

[Authorize]
[Route("api/accounts/{accountId}/documents")]
public class AccountDocumentsController : BaseController
{
    private readonly ILogger<AccountDocumentsController> _logger;
    private readonly IMediator _mediator;

    public AccountDocumentsController(ILogger<AccountDocumentsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Get all documents for a specific account
    /// </summary>
    /// <param name="accountId">Account ID from route</param>
    /// <returns>List of documents for the account</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<AccountDocumentResponse>>> GetDocumentsAsync([FromRoute] Guid accountId)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        // Verify the account exists and belongs to the user's tenant (unless Super Admin)
        if (!isSuperAdmin)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserTenantId))
            {
                return Unauthorized("Tenant ID not found in user claims");
            }

            // Verify account belongs to user's tenant
            var accountQuery = new GetAccountByIdQuery { AccountId = accountId };
            var account = await _mediator.Send(accountQuery);

            if (account == null)
            {
                return NotFound("Account not found");
            }

            if (account.TenantId != CurrentUserTenantId)
            {
                return NotFound("Account not found");
            }
        }

        _logger.LogInformation("Getting all documents for account {AccountId}", accountId);

        var query = new GetAccountDocumentsQuery { AccountId = accountId };
        var documents = await _mediator.Send(query);

        var response = documents.Select(d => AccountDocumentResponse.FromDto(d));
        return Ok(response);
    }

    /// <summary>
    /// Get document metadata by ID
    /// </summary>
    /// <param name="accountId">Account ID from route</param>
    /// <param name="id">Document ID</param>
    /// <returns>Document metadata</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AccountDocumentResponse>> GetDocumentAsync([FromRoute] Guid accountId, Guid id)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        if (!isSuperAdmin && string.IsNullOrWhiteSpace(CurrentUserTenantId))
        {
            return Unauthorized("Tenant ID not found in user claims");
        }

        _logger.LogInformation("Getting document with ID: {DocumentId} for account {AccountId}", id, accountId);

        var query = new GetAccountDocumentByIdQuery { DocumentId = id };
        var document = await _mediator.Send(query);

        if (document == null)
        {
            return NotFound();
        }

        // Verify the document belongs to the specified account
        if (document.AccountId != accountId)
        {
            return NotFound();
        }

        // Verify the document belongs to the user's tenant (unless Super Admin)
        if (!isSuperAdmin && document.TenantId != CurrentUserTenantId)
        {
            return NotFound();
        }

        return Ok(AccountDocumentResponse.FromDto(document));
    }

    /// <summary>
    /// Download a document file
    /// </summary>
    /// <param name="accountId">Account ID from route</param>
    /// <param name="id">Document ID</param>
    /// <returns>File stream</returns>
    [HttpGet("{id}/download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DownloadDocumentAsync([FromRoute] Guid accountId, Guid id)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        // First verify access to the document
        var metadataQuery = new GetAccountDocumentByIdQuery { DocumentId = id };
        var document = await _mediator.Send(metadataQuery);

        if (document == null)
        {
            return NotFound();
        }

        // Verify the document belongs to the specified account
        if (document.AccountId != accountId)
        {
            return NotFound();
        }

        // Verify the document belongs to the user's tenant (unless Super Admin)
        if (!isSuperAdmin)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserTenantId))
            {
                return Unauthorized("Tenant ID not found in user claims");
            }

            if (document.TenantId != CurrentUserTenantId)
            {
                return NotFound();
            }
        }

        _logger.LogInformation("Downloading document with ID: {DocumentId} for account {AccountId}", id, accountId);

        var query = new DownloadAccountDocumentQuery { DocumentId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return File(result.FileStream, result.ContentType, result.FileName);
    }

    /// <summary>
    /// Upload a new document
    /// </summary>
    /// <param name="accountId">Account ID from route</param>
    /// <param name="file">File to upload</param>
    /// <param name="category">Document category (optional)</param>
    /// <param name="description">Document description (optional)</param>
    /// <returns>Created document metadata</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [RequestSizeLimit(52428800)] // 50 MB limit
    [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
    public async Task<ActionResult<AccountDocumentResponse>> UploadDocumentAsync(
        [FromRoute] Guid accountId,
        IFormFile file,
        [FromForm] string? category = null,
        [FromForm] string? description = null)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        // Validate file
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file provided");
        }

        // Verify the account exists and belongs to the user's tenant (unless Super Admin)
        if (!isSuperAdmin)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserTenantId))
            {
                return Unauthorized("Tenant ID not found in user claims");
            }

            // Verify account belongs to user's tenant
            var accountQuery = new GetAccountByIdQuery { AccountId = accountId };
            var account = await _mediator.Send(accountQuery);

            if (account == null)
            {
                return NotFound("Account not found");
            }

            if (account.TenantId != CurrentUserTenantId)
            {
                return Forbid();
            }
        }

        _logger.LogInformation("Uploading document for account {AccountId}", accountId);

        // Get tenant ID
        string tenantId;
        if (isSuperAdmin)
        {
            var accountQuery = new GetAccountByIdQuery { AccountId = accountId };
            var account = await _mediator.Send(accountQuery);
            if (account == null)
            {
                return NotFound("Account not found");
            }
            tenantId = account.TenantId;
        }
        else
        {
            tenantId = CurrentUserTenantId!;
        }

        // Create upload command
        using var stream = file.OpenReadStream();
        var command = new UploadAccountDocumentCommand
        {
            TenantId = tenantId,
            AccountId = accountId,
            FileName = file.FileName,
            ContentType = file.ContentType,
            Category = category,
            Description = description,
            FileStream = stream,
            FileSizeBytes = file.Length,
            CreatedBy = CurrentUserEmail ?? CurrentUserId ?? "Unknown"
        };

        var documentDto = await _mediator.Send(command);
        var response = AccountDocumentResponse.FromDto(documentDto);

        return CreatedAtAction(nameof(GetDocumentAsync), new { accountId = accountId, id = documentDto.DocumentId }, response);
    }

    /// <summary>
    /// Delete a document
    /// </summary>
    /// <param name="accountId">Account ID from route</param>
    /// <param name="id">Document ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteDocumentAsync([FromRoute] Guid accountId, Guid id)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        // Verify the document exists and belongs to the user's tenant (unless Super Admin)
        if (!isSuperAdmin)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserTenantId))
            {
                return Unauthorized("Tenant ID not found in user claims");
            }

            var existingDocumentQuery = new GetAccountDocumentByIdQuery { DocumentId = id };
            var existingDocument = await _mediator.Send(existingDocumentQuery);

            if (existingDocument == null)
            {
                return NotFound();
            }

            // Verify document belongs to the specified account
            if (existingDocument.AccountId != accountId)
            {
                return NotFound();
            }

            if (existingDocument.TenantId != CurrentUserTenantId)
            {
                return NotFound();
            }
        }

        _logger.LogInformation("Deleting document with ID: {DocumentId} for account {AccountId}", id, accountId);

        var command = new DeleteAccountDocumentCommand
        {
            DocumentId = id,
            DeletedBy = CurrentUserEmail ?? CurrentUserId ?? "Unknown"
        };

        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
