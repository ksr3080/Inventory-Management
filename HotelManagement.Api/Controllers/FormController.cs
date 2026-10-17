using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Application.Forms.Command;
using HotelManagement.Application.Forms.Query;
using HotelManagement.Shared.DTOs.Forms;


namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public FormController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    // GET api/Form/GetForms

    [HttpGet("GetForms")]
    public async Task<ActionResult<IReadOnlyList<GetFormDTO>>> GetForms(
        CancellationToken cancellationToken = default)
    {
        var query = new GetFormQuery();

        IReadOnlyList<GetFormDTO> forms = await _sender.Send(query, cancellationToken);

        return Ok(forms);
    }

    // GET api/Form/GetFormBreadCums

    [HttpGet("GetFormBreadCums")]
    public async Task<ActionResult<IReadOnlyList<FormBreadCumDTO>>> GetFormBreadCums(
        CancellationToken cancellationToken = default)
    {
        var query = new GetFormBreadCumQuery();

        IReadOnlyList<FormBreadCumDTO> result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }

    // POST api/Form/CreateForm

    [HttpPost("CreateForm")]
    public async Task<ActionResult> CreateForm(
        [FromBody] CreateFormDTO request,
        CancellationToken cancellationToken = default)
    {
        CreateFormCommand command = _mapper.Map<CreateFormCommand>(request);

        int formId = await _sender.Send(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, new
        {
            Id = formId,
            Message = "Form created successfully."
        });
    }

    // PUT api/Form/UpdateForm/5

    [HttpPut("UpdateForm/{id:int}")]
    public async Task<ActionResult> UpdateForm(
        int id,
        [FromBody] UpdateFormDTO request,
        CancellationToken cancellationToken = default)
    {
        if (id != request.Id)
            return BadRequest("Id in URL does not match Id in request body.");

        UpdateFormCommand command = _mapper.Map<UpdateFormCommand>(request);

        bool updated = await _sender.Send(command, cancellationToken);

        if (!updated)
            return NotFound($"Form with Id {id} was not found.");

        return Ok(new { Message = "Form updated successfully." });
    }

    // DELETE api/Form/DeleteForm/5

    [HttpDelete("DeleteForm/{id:int}")]
    public async Task<ActionResult> DeleteForm(
        int id,
        CancellationToken cancellationToken = default)
    {
        bool deleted = await _sender.Send(new DeleteFormCommand { Id = id }, cancellationToken);

        if (!deleted)
            return NotFound($"Form with Id {id} was not found.");

        return Ok(new { Message = "Form deleted successfully." });
    }
}
