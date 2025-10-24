using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Domain.DTOs;

namespace TOB.Accounts.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class IndustriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public IndustriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all industries
    /// </summary>
    /// <returns>List of industries</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<IndustryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<IndustryDto>>> GetAllIndustriesAsync()
    {
        var query = new GetAllIndustriesQuery();
        var industries = await _mediator.Send(query);
        return Ok(industries);
    }
}
