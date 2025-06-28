using Application.Features.Routes.Commands.Create;
using Application.Features.Routes.Commands.Delete;
using Application.Features.Routes.Commands.Update;
using Application.Features.Routes.Queries.GetById;
using Application.Features.Routes.Queries.GetList;
using Shared.Application.Requests;
using Shared.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoutesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedRouteResponse>> Add([FromBody] CreateRouteCommand command)
    {
        CreatedRouteResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedRouteResponse>> Update([FromBody] UpdateRouteCommand command)
    {
        UpdatedRouteResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedRouteResponse>> Delete([FromRoute] Guid id)
    {
        DeleteRouteCommand command = new() { Id = id };

        DeletedRouteResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdRouteResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdRouteQuery query = new() { Id = id };

        GetByIdRouteResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListRouteListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListRouteQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListRouteListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}