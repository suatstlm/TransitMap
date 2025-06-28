using Application.Features.Shapes.Commands.Create;
using Application.Features.Shapes.Commands.Delete;
using Application.Features.Shapes.Commands.Update;
using Application.Features.Shapes.Queries.GetById;
using Application.Features.Shapes.Queries.GetList;
using Shared.Application.Requests;
using Shared.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShapesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedShapeResponse>> Add([FromBody] CreateShapeCommand command)
    {
        CreatedShapeResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedShapeResponse>> Update([FromBody] UpdateShapeCommand command)
    {
        UpdatedShapeResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedShapeResponse>> Delete([FromRoute] Guid id)
    {
        DeleteShapeCommand command = new() { Id = id };

        DeletedShapeResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdShapeResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdShapeQuery query = new() { Id = id };

        GetByIdShapeResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListShapeListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListShapeQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListShapeListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}