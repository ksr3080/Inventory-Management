using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management.Application.Companies.Command;
using Inventory_Management.Application.Companies.Query;
using Inventory_Management.Shared.DTOs.Company;


namespace Inventory_Management.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CompanyController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateCompany([FromBody] CreateCompanyDTO request, CancellationToken cancellationToken)
    {
        CreateCompanyCommand command = _mapper.Map<CreateCompanyCommand>(request);

        int userId = await _sender.Send(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created,
            new
            {
                Id = userId,
                Message = "User created successfully."
            });
    }
    [HttpGet("get")]
    public async Task<ActionResult<IReadOnlyList<GetCompanyDTO>>> GetCompanies(CancellationToken cancellationToken)
    {
        var query = new GetCompanyQuery();

        IReadOnlyList<GetCompanyDTO> users = await _sender.Send(query, cancellationToken);

        return Ok(users);
    }
    [HttpPut("update/{id:int}")]
    public async Task<ActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyDTO request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
            return BadRequest("Invalid company id.");

        UpdateCompanyCommand command = _mapper.Map<UpdateCompanyCommand>(request);

        await _sender.Send(command, cancellationToken);

        return Ok(new
        {
            Message = "Company updated successfully."
        });
    }

    // DELETE

    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult> DeleteCompany(int id, CancellationToken cancellationToken)
    {
        await _sender.Send(new DeleteCompanyCommand(id), cancellationToken);

        return Ok(new
        {
            Message = "Company deleted successfully."
        });
    }
}
