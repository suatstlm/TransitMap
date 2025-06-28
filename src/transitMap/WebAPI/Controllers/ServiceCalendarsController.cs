using Application.Features.ServiceCalendars.Commands.Create;
using Application.Features.ServiceCalendars.Commands.Delete;
using Application.Features.ServiceCalendars.Commands.Update;
using Application.Features.ServiceCalendars.Queries.GetById;
using Application.Features.ServiceCalendars.Queries.GetList;
using Shared.Application.Requests;
using Shared.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceCalendarsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedServiceCalendarResponse>> Add([FromBody] CreateServiceCalendarCommand command)
    {
        CreatedServiceCalendarResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedServiceCalendarResponse>> Update([FromBody] UpdateServiceCalendarCommand command)
    {
        UpdatedServiceCalendarResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedServiceCalendarResponse>> Delete([FromRoute] Guid id)
    {
        DeleteServiceCalendarCommand command = new() { Id = id };

        DeletedServiceCalendarResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdServiceCalendarResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdServiceCalendarQuery query = new() { Id = id };

        GetByIdServiceCalendarResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListServiceCalendarListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListServiceCalendarQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListServiceCalendarListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}