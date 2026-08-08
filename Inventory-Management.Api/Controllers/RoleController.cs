using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management.Application.Roles.Command;
using Inventory_Management.Application.Roles.Query;
using Inventory_Management.Shared.DTOs.Roles;


namespace Inventory_Management.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public RoleController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    // GET api/Role/GetRoles

    [HttpGet("GetRoles")]
    public async Task<ActionResult<IReadOnlyList<GetRoleDTO>>> GetRoles(
        CancellationToken cancellationToken = default)
    {
        var query = new GetRoleQuery();

        IReadOnlyList<GetRoleDTO> roles = await _sender.Send(query, cancellationToken);

        return Ok(roles);
    }

    // POST api/Role/CreateRole

    [HttpPost("CreateRole")]
    public async Task<ActionResult> CreateRole(
        [FromBody] CreateRoleDTO request,
        CancellationToken cancellationToken = default)
    {
        CreateRoleCommand command = _mapper.Map<CreateRoleCommand>(request);

        int roleId = await _sender.Send(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, new
        {
            Id = roleId,
            Message = "Role created successfully."
        });
    }

    // PUT api/Role/UpdateRole/5

    [HttpPut("UpdateRole/{id:int}")]
    public async Task<ActionResult> UpdateRole(
        int id,
        [FromBody] UpdateRoleDTO request,
        CancellationToken cancellationToken = default)
    {
        if (id != request.Id)
            return BadRequest("Id in URL does not match Id in request body.");

        UpdateRoleCommand command = _mapper.Map<UpdateRoleCommand>(request);

        bool updated = await _sender.Send(command, cancellationToken);

        if (!updated)
            return NotFound($"Role with Id {id} was not found.");

        return Ok(new { Message = "Role updated successfully." });
    }

    // DELETE api/Role/DeleteRole/5

    [HttpDelete("DeleteRole/{id:int}")]
    public async Task<ActionResult> DeleteRole(
        int id,
        CancellationToken cancellationToken = default)
    {
        bool deleted = await _sender.Send(new DeleteRoleCommand { Id = id }, cancellationToken);

        if (!deleted)
            return NotFound($"Role with Id {id} was not found.");

        return Ok(new { Message = "Role deleted successfully." });
    }
}
