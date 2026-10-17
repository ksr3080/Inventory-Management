using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Application.Customers.Command;
using HotelManagement.Application.Customers.Query;
using HotelManagement.Shared.DTOs.Customer;


namespace HotelManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CustomerController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    // CREATE

    [HttpPost("create")]
    public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomerDTO request, CancellationToken cancellationToken)
    {
        CreateCustomerCommand command = _mapper.Map<CreateCustomerCommand>(request);

        int customerId = await _sender.Send(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, new
        {
            Id = customerId,
            Message = "Customer created successfully."
        });
    }

    // GET ALL

    [HttpGet("get")]
    public async Task<ActionResult<IReadOnlyList<GetCustomerDTO>>> GetCustomers(CancellationToken cancellationToken)
    {
        var query = new GetCustomerQuery();
        IReadOnlyList<GetCustomerDTO> customers = await _sender.Send(query, cancellationToken);

        return Ok(customers);
    }

    // UPDATE

    [HttpPut("update/{id:int}")]
    public async Task<ActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDTO request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
            return BadRequest("Invalid customer id.");

        UpdateCustomerCommand command = _mapper.Map<UpdateCustomerCommand>(request);

        var response = await _sender.Send(command, cancellationToken);

        return Ok(response);
    }

    // DELETE

    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {

        await _sender.Send(new DeleteCustomerCommand(id), CancellationToken.None);

        return Ok(new
        {
            Message = "Customer deleted successfully."
        });
    }
}