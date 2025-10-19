using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TOB.Accounts.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;

    public AccountsController(ILogger<AccountsController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get all accounts
    /// </summary>
    /// <returns>List of accounts</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IEnumerable<object>> GetAccounts()
    {
        _logger.LogInformation("Getting all accounts");

        // TODO: Implement actual business logic via Services layer
        return Ok(new[]
        {
            new { Id = 1, Name = "Account 1", Status = "Active" },
            new { Id = 2, Name = "Account 2", Status = "Active" }
        });
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
    public ActionResult<object> GetAccount(int id)
    {
        _logger.LogInformation("Getting account with ID: {AccountId}", id);

        // TODO: Implement actual business logic via Services layer
        if (id <= 0)
        {
            return NotFound();
        }

        return Ok(new { Id = id, Name = $"Account {id}", Status = "Active" });
    }

    /// <summary>
    /// Create a new account
    /// </summary>
    /// <param name="account">Account data</param>
    /// <returns>Created account</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<object> CreateAccount([FromBody] object account)
    {
        _logger.LogInformation("Creating new account");

        // TODO: Implement actual business logic via Services layer
        var newAccount = new { Id = 3, Name = "New Account", Status = "Active" };

        return CreatedAtAction(nameof(GetAccount), new { id = 3 }, newAccount);
    }

    /// <summary>
    /// Update an existing account
    /// </summary>
    /// <param name="id">Account ID</param>
    /// <param name="account">Updated account data</param>
    /// <returns>No content</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult UpdateAccount(int id, [FromBody] object account)
    {
        _logger.LogInformation("Updating account with ID: {AccountId}", id);

        // TODO: Implement actual business logic via Services layer
        if (id <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete an account
    /// </summary>
    /// <param name="id">Account ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult DeleteAccount(int id)
    {
        _logger.LogInformation("Deleting account with ID: {AccountId}", id);

        // TODO: Implement actual business logic via Services layer
        if (id <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }
}
