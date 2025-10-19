using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TOB.Accounts.Domain.Commands;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Domain.Responses;

namespace TOB.Accounts.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class AccountsController : BaseController
{
    private readonly ILogger<AccountsController> _logger;
    private readonly IMediator _mediator;

    public AccountsController(ILogger<AccountsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Get all accounts
    /// </summary>
    /// <param name="tenantId">Optional Tenant ID (Super Admin only)</param>
    /// <returns>List of accounts</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAccountsAsync([FromQuery] Guid? tenantId = null)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;
        Guid effectiveTenantId;

        if (tenantId.HasValue)
        {
            // Only Super Admins can specify a different tenant ID
            if (!isSuperAdmin)
            {
                return Forbid();
            }
            effectiveTenantId = tenantId.Value;
        }
        else
        {
            // Use the current user's tenant ID
            if (string.IsNullOrWhiteSpace(CurrentUserTenantId))
            {
                return Unauthorized("Tenant ID not found in user claims");
            }

            if (!Guid.TryParse(CurrentUserTenantId, out effectiveTenantId))
            {
                return BadRequest("Invalid Tenant ID format");
            }
        }

        _logger.LogInformation("Getting all accounts for tenant {TenantId}", effectiveTenantId);

        var query = new GetAllAccountsQuery { TenantId = effectiveTenantId };
        var accounts = await _mediator.Send(query);

        var response = accounts.Select(a => AccountResponse.FromDto(a));
        return Ok(response);
    }

    /// <summary>
    /// Get account by ID
    /// </summary>
    /// <param name="id">Account ID</param>
    /// <returns>Account details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AccountResponse>> GetAccountAsync(Guid id)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        if (!isSuperAdmin && string.IsNullOrWhiteSpace(CurrentUserTenantId))
        {
            return Unauthorized("Tenant ID not found in user claims");
        }

        _logger.LogInformation("Getting account with ID: {AccountId}", id);

        var query = new GetAccountByIdQuery { AccountId = id };
        var account = await _mediator.Send(query);

        if (account == null)
        {
            return NotFound();
        }

        // Verify the account belongs to the user's tenant (unless Super Admin)
        if (!isSuperAdmin && account.TenantId != CurrentUserTenantId)
        {
            return NotFound();
        }

        return Ok(AccountResponse.FromDto(account));
    }

    /// <summary>
    /// Create a new account
    /// </summary>
    /// <param name="command">Account data</param>
    /// <returns>Created account</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<AccountResponse>> CreateAccountAsync([FromBody] CreateAccountCommand command)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        // Regular users can only create accounts for their own tenant
        if (!isSuperAdmin)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserTenantId))
            {
                return Unauthorized("Tenant ID not found in user claims");
            }

            if (command.TenantId != CurrentUserTenantId)
            {
                return Forbid();
            }
        }

        _logger.LogInformation("Creating new account for tenant {TenantId}", command.TenantId);

        var account = await _mediator.Send(command);
        var response = AccountResponse.FromDto(account);

        return CreatedAtAction(nameof(GetAccountAsync), new { id = account.AccountId }, response);
    }

    /// <summary>
    /// Update an existing account
    /// </summary>
    /// <param name="id">Account ID</param>
    /// <param name="command">Updated account data</param>
    /// <returns>No content</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateAccountAsync(Guid id, [FromBody] UpdateAccountCommand command)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        if (id != command.AccountId)
        {
            return BadRequest("Account ID mismatch");
        }

        // Verify the account exists and belongs to the user's tenant (unless Super Admin)
        if (!isSuperAdmin)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserTenantId))
            {
                return Unauthorized("Tenant ID not found in user claims");
            }

            var existingAccountQuery = new GetAccountByIdQuery { AccountId = id };
            var existingAccount = await _mediator.Send(existingAccountQuery);

            if (existingAccount == null)
            {
                return NotFound();
            }

            if (existingAccount.TenantId != CurrentUserTenantId)
            {
                return NotFound();
            }
        }

        _logger.LogInformation("Updating account with ID: {AccountId}", id);

        var account = await _mediator.Send(command);

        if (account == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete an account
    /// </summary>
    /// <param name="id">Account ID</param>
    /// <param name="deletedBy">User deleting the account</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteAccountAsync(Guid id, [FromQuery] string deletedBy)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        // Verify the account exists and belongs to the user's tenant (unless Super Admin)
        if (!isSuperAdmin)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserTenantId))
            {
                return Unauthorized("Tenant ID not found in user claims");
            }

            var existingAccountQuery = new GetAccountByIdQuery { AccountId = id };
            var existingAccount = await _mediator.Send(existingAccountQuery);

            if (existingAccount == null)
            {
                return NotFound();
            }

            if (existingAccount.TenantId != CurrentUserTenantId)
            {
                return NotFound();
            }
        }

        _logger.LogInformation("Deleting account with ID: {AccountId}", id);

        var command = new DeleteAccountCommand { AccountId = id, DeletedBy = deletedBy };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
