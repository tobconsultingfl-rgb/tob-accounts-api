using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TOB.Accounts.Domain.Commands;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Domain.Responses;

namespace TOB.Accounts.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
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
    /// <returns>List of accounts</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAccountsAsync([FromQuery] Guid tenantId)
    {
        _logger.LogInformation("Getting all accounts for tenant {TenantId}", tenantId);

        var query = new GetAllAccountsQuery { TenantId = tenantId };
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
        _logger.LogInformation("Getting account with ID: {AccountId}", id);

        var query = new GetAccountByIdQuery { AccountId = id };
        var account = await _mediator.Send(query);

        if (account == null)
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
    public async Task<ActionResult<AccountResponse>> CreateAccountAsync([FromBody] CreateAccountCommand command)
    {
        _logger.LogInformation("Creating new account");

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
    public async Task<IActionResult> UpdateAccountAsync(Guid id, [FromBody] UpdateAccountCommand command)
    {
        _logger.LogInformation("Updating account with ID: {AccountId}", id);

        if (id != command.AccountId)
        {
            return BadRequest("Account ID mismatch");
        }

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
    public async Task<IActionResult> DeleteAccountAsync(Guid id, [FromQuery] string deletedBy)
    {
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
