using Application.Features.StopTimes.Commands.Create;
using Application.Features.StopTimes.Commands.Delete;
using Application.Features.StopTimes.Commands.Update;
using Application.Features.StopTimes.Queries.GetById;
using Application.Features.StopTimes.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StopTimesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedStopTimeResponse>> Add([FromBody] CreateStopTimeCommand command)
    {
        CreatedStopTimeResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedStopTimeResponse>> Update([FromBody] UpdateStopTimeCommand command)
    {
        UpdatedStopTimeResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedStopTimeResponse>> Delete([FromRoute] Guid id)
    {
        DeleteStopTimeCommand command = new() { Id = id };

        DeletedStopTimeResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdStopTimeResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdStopTimeQuery query = new() { Id = id };

        GetByIdStopTimeResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListStopTimeListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStopTimeQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListStopTimeListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}