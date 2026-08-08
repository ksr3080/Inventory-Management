using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management.Application.Auth.Query;
using Inventory_Management.Shared.DTOs.Auth;

namespace Inventory_Management.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDTO>> Login(
        [FromBody] LoginRequestDTO request,
        CancellationToken cancellationToken = default)
    {
        var query = new LoginUserQuery(request);
        var result = await _sender.Send(query, cancellationToken);

        if (result == null)
        {
            return Unauthorized(new { Message = "Invalid username/email or password, or account is disabled." });
        }

        return Ok(result);
    }
}
