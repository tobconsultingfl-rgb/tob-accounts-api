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
public class ContactsController : ControllerBase
{
    private readonly ILogger<ContactsController> _logger;
    private readonly IMediator _mediator;

    public ContactsController(ILogger<ContactsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Get all contacts
    /// </summary>
    /// <returns>List of contacts</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ContactResponse>>> GetContactsAsync([FromQuery] Guid tenantId)
    {
        _logger.LogInformation("Getting all contacts for tenant {TenantId}", tenantId);

        var query = new GetAllContactsQuery { TenantId = tenantId };
        var contacts = await _mediator.Send(query);

        var response = contacts.Select(c => ContactResponse.FromDto(c));
        return Ok(response);
    }

    /// <summary>
    /// Get contact by ID
    /// </summary>
    /// <param name="id">Contact ID</param>
    /// <returns>Contact details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ContactResponse>> GetContactAsync(Guid id)
    {
        _logger.LogInformation("Getting contact with ID: {ContactId}", id);

        var query = new GetContactByIdQuery { ContactId = id };
        var contact = await _mediator.Send(query);

        if (contact == null)
        {
            return NotFound();
        }

        return Ok(ContactResponse.FromDto(contact));
    }

    /// <summary>
    /// Create a new contact
    /// </summary>
    /// <param name="command">Contact data</param>
    /// <returns>Created contact</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ContactResponse>> CreateContactAsync([FromBody] CreateContactCommand command)
    {
        _logger.LogInformation("Creating new contact");

        var contact = await _mediator.Send(command);
        var response = ContactResponse.FromDto(contact);

        return CreatedAtAction(nameof(GetContactAsync), new { id = contact.ContactId }, response);
    }

    /// <summary>
    /// Update an existing contact
    /// </summary>
    /// <param name="id">Contact ID</param>
    /// <param name="command">Updated contact data</param>
    /// <returns>No content</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateContactAsync(Guid id, [FromBody] UpdateContactCommand command)
    {
        _logger.LogInformation("Updating contact with ID: {ContactId}", id);

        if (id != command.ContactId)
        {
            return BadRequest("Contact ID mismatch");
        }

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
    /// <param name="id">Contact ID</param>
    /// <param name="deletedBy">User deleting the contact</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteContactAsync(Guid id, [FromQuery] string deletedBy)
    {
        _logger.LogInformation("Deleting contact with ID: {ContactId}", id);

        var command = new DeleteContactCommand { ContactId = id, DeletedBy = deletedBy };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
