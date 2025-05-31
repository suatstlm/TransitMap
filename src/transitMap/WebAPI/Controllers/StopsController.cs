using Application.Features.Stops.Commands.Create;
using Application.Features.Stops.Commands.Delete;
using Application.Features.Stops.Commands.Update;
using Application.Features.Stops.Queries.GetById;
using Application.Features.Stops.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StopsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedStopResponse>> Add([FromBody] CreateStopCommand command)
    {
        CreatedStopResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedStopResponse>> Update([FromBody] UpdateStopCommand command)
    {
        UpdatedStopResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedStopResponse>> Delete([FromRoute] Guid id)
    {
        DeleteStopCommand command = new() { Id = id };

        DeletedStopResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdStopResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdStopQuery query = new() { Id = id };

        GetByIdStopResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListStopListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStopQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListStopListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}