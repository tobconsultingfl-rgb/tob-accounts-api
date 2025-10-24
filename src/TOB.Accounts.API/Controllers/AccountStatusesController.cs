using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Domain.DTOs;

namespace TOB.Accounts.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AccountStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all account statuses
    /// </summary>
    /// <returns>List of account statuses</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AccountStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<AccountStatusDto>>> GetAllAccountStatusesAsync()
    {
        var query = new GetAllAccountStatusesQuery();
        var accountStatuses = await _mediator.Send(query);
        return Ok(accountStatuses);
    }
}
