using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management.Application.Users.Command;
using Inventory_Management.Application.Users.Query;
using Inventory_Management.Shared.DTOs.Users;


namespace Inventory_Management.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    // GET api/User/GetUsers
    // GET api/User/GetUsers?companyId=1

    [HttpGet("GetUsers")]
    public async Task<ActionResult<IReadOnlyList<GetUserDTO>>> GetUsers(
        [FromQuery] int companyId = 0,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUserQuery(companyId);

        IReadOnlyList<GetUserDTO> users = await _sender.Send(query, cancellationToken);

        return Ok(users);
    }

    // POST api/User/CreateUser

    [HttpPost("CreateUser")]
    public async Task<ActionResult> CreateUser(
        [FromBody] CreateUserDTO request,
        CancellationToken cancellationToken = default)
    {
        CreateUserCommand command = _mapper.Map<CreateUserCommand>(request);

        int userId = await _sender.Send(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, new
        {
            Id = userId,
            Message = "User created successfully."
        });
    }

    // PUT api/User/UpdateUser/5

    [HttpPut("UpdateUser/{id:int}")]
    public async Task<ActionResult> UpdateUser(
        int id,
        [FromBody] UpdateUserDTO request,
        CancellationToken cancellationToken = default)
    {
        if (id != request.Id)
            return BadRequest("Id in URL does not match Id in request body.");

        UpdateUserCommand command = _mapper.Map<UpdateUserCommand>(request);

        bool updated = await _sender.Send(command, cancellationToken);

        if (!updated)
            return NotFound($"User with Id {id} was not found.");

        return Ok(new { Message = "User updated successfully." });
    }

    // DELETE api/User/DeleteUser/5

    [HttpDelete("DeleteUser/{id:int}")]
    public async Task<ActionResult> DeleteUser(
        int id,
        CancellationToken cancellationToken = default)
    {
        bool deleted = await _sender.Send(new DeleteUserCommand { Id = id }, cancellationToken);

        if (!deleted)
            return NotFound($"User with Id {id} was not found.");

        return Ok(new { Message = "User deleted successfully." });
    }

    // POST api/User/AssignRole
    // Assigns (or replaces) a role for a user under a company.

    [HttpPost("AssignRole")]
    public async Task<ActionResult> AssignRole(
        [FromBody] AssignUserRoleDTO request,
        CancellationToken cancellationToken = default)
    {
        var command = new AssignUserRoleCommand
        {
            UserId = request.UserId,
            RoleId = request.RoleId,
            CompanyId = request.CompanyId
        };

        bool result = await _sender.Send(command, cancellationToken);

        if (!result)
            return NotFound($"User with Id {request.UserId} was not found.");

        return Ok(new { Message = "Role assigned successfully." });
    }
}

