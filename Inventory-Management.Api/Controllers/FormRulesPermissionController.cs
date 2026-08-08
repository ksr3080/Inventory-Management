using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management.Application.FormRulesPermission.Command;
using Inventory_Management.Application.FormRulesPermission.Query;
using Inventory_Management.Shared.DTOs.FormRulesPermission;

namespace Inventory_Management.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormRulesPermissionController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public FormRulesPermissionController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    // GET api/FormRulesPermission/GetByRoleId?roleId=1

    [HttpGet("GetByRoleId")]
    public async Task<ActionResult<IReadOnlyList<GetFormRulesPermissionDTO>>> GetByRoleId(
        [FromQuery] int roleId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetFormRulesPermissionQuery(roleId);

        IReadOnlyList<GetFormRulesPermissionDTO> permissions = await _sender.Send(query, cancellationToken);

        return Ok(permissions);
    }

    // POST api/FormRulesPermission/SavePermissions

    [HttpPost("SavePermissions")]
    public async Task<ActionResult> SavePermissions(
        [FromBody] SaveFormRulesPermissionDTO request,
        CancellationToken cancellationToken = default)
    {
        SaveFormRulesPermissionCommand command = _mapper.Map<SaveFormRulesPermissionCommand>(request);

        bool result = await _sender.Send(command, cancellationToken);

        if (!result)
            return BadRequest("Failed to save permissions.");

        return Ok(new { Message = "Permissions saved successfully." });
    }
}
