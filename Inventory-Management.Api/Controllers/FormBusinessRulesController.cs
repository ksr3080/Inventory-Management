using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management.Application.FormBusinessRules.Command;
using Inventory_Management.Application.FormBusinessRules.Query;
using Inventory_Management.Shared.DTOs.FormBusinessRules;


namespace Inventory_Management.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormBusinessRulesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public FormBusinessRulesController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    // GET api/FormBusinessRules/GetFormBusinessRules
    // GET api/FormBusinessRules/GetFormBusinessRules?formId=1

    [HttpGet("GetFormBusinessRules")]
    public async Task<ActionResult<IReadOnlyList<GetFormBusinessRulesDTO>>> GetFormBusinessRules(
        [FromQuery] int formId = 0,
        CancellationToken cancellationToken = default)
    {
        var query = new GetFormBusinessRulesQuery(formId);

        IReadOnlyList<GetFormBusinessRulesDTO> rules = await _sender.Send(query, cancellationToken);

        return Ok(rules);
    }

    // POST api/FormBusinessRules/CreateFormBusinessRule

    [HttpPost("CreateFormBusinessRule")]
    public async Task<ActionResult> CreateFormBusinessRule(
        [FromBody] CreateFormBusinessRulesDTO request,
        CancellationToken cancellationToken = default)
    {
        CreateFormBusinessRulesCommand command = _mapper.Map<CreateFormBusinessRulesCommand>(request);

        int ruleId = await _sender.Send(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, new
        {
            Id = ruleId,
            Message = "Form business rule created successfully."
        });
    }

    // PUT api/FormBusinessRules/UpdateFormBusinessRule/5

    [HttpPut("UpdateFormBusinessRule/{id:int}")]
    public async Task<ActionResult> UpdateFormBusinessRule(
        int id,
        [FromBody] UpdateFormBusinessRulesDTO request,
        CancellationToken cancellationToken = default)
    {
        if (id != request.Id)
            return BadRequest("Id in URL does not match Id in request body.");

        UpdateFormBusinessRulesCommand command = _mapper.Map<UpdateFormBusinessRulesCommand>(request);

        bool updated = await _sender.Send(command, cancellationToken);

        if (!updated)
            return NotFound($"Form business rule with Id {id} was not found.");

        return Ok(new { Message = "Form business rule updated successfully." });
    }

    // DELETE api/FormBusinessRules/DeleteFormBusinessRule/5

    [HttpDelete("DeleteFormBusinessRule/{id:int}")]
    public async Task<ActionResult> DeleteFormBusinessRule(
        int id,
        CancellationToken cancellationToken = default)
    {
        bool deleted = await _sender.Send(new DeleteFormBusinessRulesCommand { Id = id }, cancellationToken);

        if (!deleted)
            return NotFound($"Form business rule with Id {id} was not found.");

        return Ok(new { Message = "Form business rule deleted successfully." });
    }
}
