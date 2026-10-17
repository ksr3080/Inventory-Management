using MediatR;
using Microsoft.Extensions.Logging;
using HotelManagement.Infrastructure.Repositories.Interfaces;

namespace HotelManagement.Application.Users.Command;

public class UpdateUserCommand : IRequest<bool>
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailId { get; set; }

    /// <summary>Leave null to keep the existing password unchanged.</summary>
    public string? Password { get; set; }

    public string ContactNo { get; set; }

    public bool IsLoginAllowed { get; set; }

    public int UpdatedBy { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    private readonly ILogger<UpdateUserCommandHandler> _logger;


    public UpdateUserCommandHandler(IUserRepository userRepository, ILogger<UpdateUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }


    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user =
            await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return false;
        }

        string normalizedEmail =
            request.EmailId.Trim().ToLowerInvariant();

        var userName = $"{request.FirstName} {request.LastName}";

        user.UpdateDetails(
            request.FirstName,
            request.LastName,
            userName,
            normalizedEmail,
            request.ContactNo,
            request.IsLoginAllowed,
            0,
            null,
            request.UpdatedBy);

        // Only reset the password when an explicit new password is provided
        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            user.ChangePassword(request.Password, request.UpdatedBy);
        }

        await _userRepository.SaveChangesAsync(cancellationToken);


        _logger.LogInformation("User {UserId} updated successfully.", user.Id);


        return true;
    }
}