using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Domain.DTOs;

namespace TOB.Accounts.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AccountTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all account types
    /// </summary>
    /// <returns>List of account types</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AccountTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<AccountTypeDto>>> GetAllAccountTypesAsync()
    {
        var query = new GetAllAccountTypesQuery();
        var accountTypes = await _mediator.Send(query);
        return Ok(accountTypes);
    }
}
