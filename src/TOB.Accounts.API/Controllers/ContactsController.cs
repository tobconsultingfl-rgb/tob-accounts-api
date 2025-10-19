using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TOB.Accounts.Domain.Commands;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Domain.Responses;

namespace TOB.Accounts.API.Controllers;

[Authorize]
[Route("api/accounts/{accountId}/contacts")]
public class ContactsController : BaseController
{
    private readonly ILogger<ContactsController> _logger;
    private readonly IMediator _mediator;

    public ContactsController(ILogger<ContactsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Get all contacts for a specific account
    /// </summary>
    /// <param name="accountId">Account ID from route</param>
    /// <returns>List of contacts for the account</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ContactResponse>>> GetContactsAsync([FromRoute] Guid accountId)
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

        _logger.LogInformation("Getting all contacts for account {AccountId}", accountId);

        var query = new GetAllContactsQuery { AccountId = accountId };
        var contacts = await _mediator.Send(query);

        var response = contacts.Select(c => ContactResponse.FromDto(c));
        return Ok(response);
    }

    /// <summary>
    /// Get contact by ID
    /// </summary>
    /// <param name="accountId">Account ID from route</param>
    /// <param name="id">Contact ID</param>
    /// <returns>Contact details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ContactResponse>> GetContactAsync([FromRoute] Guid accountId, Guid id)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        if (!isSuperAdmin && string.IsNullOrWhiteSpace(CurrentUserTenantId))
        {
            return Unauthorized("Tenant ID not found in user claims");
        }

        _logger.LogInformation("Getting contact with ID: {ContactId} for account {AccountId}", id, accountId);

        var query = new GetContactByIdQuery { ContactId = id };
        var contact = await _mediator.Send(query);

        if (contact == null)
        {
            return NotFound();
        }

        // Verify the contact belongs to the specified account
        if (contact.AccountId != accountId)
        {
            return NotFound();
        }

        // Verify the contact belongs to the user's tenant (unless Super Admin)
        if (!isSuperAdmin && contact.TenantId != CurrentUserTenantId)
        {
            return NotFound();
        }

        return Ok(ContactResponse.FromDto(contact));
    }

    /// <summary>
    /// Create a new contact
    /// </summary>
    /// <param name="accountId">Account ID from route</param>
    /// <param name="command">Contact data</param>
    /// <returns>Created contact</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ContactResponse>> CreateContactAsync([FromRoute] Guid accountId, [FromBody] CreateContactCommand command)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        // Validate accountId matches the command
        if (accountId != command.AccountId)
        {
            return BadRequest("Account ID in route does not match Account ID in request body");
        }

        // Regular users can only create contacts for their own tenant
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

        _logger.LogInformation("Creating new contact for account {AccountId}", accountId);

        var contact = await _mediator.Send(command);
        var response = ContactResponse.FromDto(contact);

        return CreatedAtAction(nameof(GetContactAsync), new { accountId = accountId, id = contact.ContactId }, response);
    }

    /// <summary>
    /// Update an existing contact
    /// </summary>
    /// <param name="accountId">Account ID from route</param>
    /// <param name="id">Contact ID</param>
    /// <param name="command">Updated contact data</param>
    /// <returns>No content</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateContactAsync([FromRoute] Guid accountId, Guid id, [FromBody] UpdateContactCommand command)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        if (id != command.ContactId)
        {
            return BadRequest("Contact ID mismatch");
        }

        // Verify the contact exists and belongs to the user's tenant (unless Super Admin)
        if (!isSuperAdmin)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserTenantId))
            {
                return Unauthorized("Tenant ID not found in user claims");
            }

            var existingContactQuery = new GetContactByIdQuery { ContactId = id };
            var existingContact = await _mediator.Send(existingContactQuery);

            if (existingContact == null)
            {
                return NotFound();
            }

            // Verify contact belongs to the specified account
            if (existingContact.AccountId != accountId)
            {
                return NotFound();
            }

            if (existingContact.TenantId != CurrentUserTenantId)
            {
                return NotFound();
            }
        }

        _logger.LogInformation("Updating contact with ID: {ContactId} for account {AccountId}", id, accountId);

        var contact = await _mediator.Send(command);

        if (contact == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a contact
    /// </summary>
    /// <param name="accountId">Account ID from route</param>
    /// <param name="id">Contact ID</param>
    /// <param name="deletedBy">User deleting the contact</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteContactAsync([FromRoute] Guid accountId, Guid id, [FromQuery] string deletedBy)
    {
        var isSuperAdmin = CurrentUserRoles?.Contains("Super Admin") ?? false;

        // Verify the contact exists and belongs to the user's tenant (unless Super Admin)
        if (!isSuperAdmin)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserTenantId))
            {
                return Unauthorized("Tenant ID not found in user claims");
            }

            var existingContactQuery = new GetContactByIdQuery { ContactId = id };
            var existingContact = await _mediator.Send(existingContactQuery);

            if (existingContact == null)
            {
                return NotFound();
            }

            // Verify contact belongs to the specified account
            if (existingContact.AccountId != accountId)
            {
                return NotFound();
            }

            if (existingContact.TenantId != CurrentUserTenantId)
            {
                return NotFound();
            }
        }

        _logger.LogInformation("Deleting contact with ID: {ContactId} for account {AccountId}", id, accountId);

        var command = new DeleteContactCommand { ContactId = id, DeletedBy = deletedBy };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
