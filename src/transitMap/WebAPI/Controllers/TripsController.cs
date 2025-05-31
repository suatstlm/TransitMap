using Application.Features.Trips.Commands.Create;
using Application.Features.Trips.Commands.Delete;
using Application.Features.Trips.Commands.Update;
using Application.Features.Trips.Queries.GetById;
using Application.Features.Trips.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedTripResponse>> Add([FromBody] CreateTripCommand command)
    {
        CreatedTripResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedTripResponse>> Update([FromBody] UpdateTripCommand command)
    {
        UpdatedTripResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedTripResponse>> Delete([FromRoute] Guid id)
    {
        DeleteTripCommand command = new() { Id = id };

        DeletedTripResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdTripResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdTripQuery query = new() { Id = id };

        GetByIdTripResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListTripListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTripQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListTripListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}