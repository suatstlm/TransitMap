using Application.Features.Agencies.Commands.Create;
using Application.Features.Agencies.Commands.Delete;
using Application.Features.Agencies.Commands.Update;
using Application.Features.Agencies.Queries.GetById;
using Application.Features.Agencies.Queries.GetList;
using Shared.Application.Requests;
using Shared.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgenciesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedAgencyResponse>> Add([FromBody] CreateAgencyCommand command)
    {
        CreatedAgencyResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedAgencyResponse>> Update([FromBody] UpdateAgencyCommand command)
    {
        UpdatedAgencyResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedAgencyResponse>> Delete([FromRoute] Guid id)
    {
        DeleteAgencyCommand command = new() { Id = id };

        DeletedAgencyResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdAgencyResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdAgencyQuery query = new() { Id = id };

        GetByIdAgencyResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListAgencyListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAgencyQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListAgencyListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}