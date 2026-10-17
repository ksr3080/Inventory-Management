using MediatR;
using Microsoft.Extensions.Logging;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Repositories.Interfaces;
namespace HotelManagement.Application.Users.Command;

public class CreateUserCommand : IRequest<int>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailId { get; set; }
    public string Password { get; set; }

    public string ContactNo { get; set; }

    public bool IsLoginAllowed { get; set; }

    public int CreatedBy { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        string normalizedEmail = request.EmailId.Trim().ToLowerInvariant();

        bool emailExists = await _userRepository.IsEmailExistsAsync(normalizedEmail, cancellationToken);

        if (emailExists)
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }
        var userName = $"{request.FirstName} {request.LastName}";
        var user = new Domain.Entities.Users(
            request.FirstName,
            request.LastName,
            userName,
            normalizedEmail,
            request.Password,
            request.ContactNo,
            request.IsLoginAllowed,
            0,
            null,
            request.CreatedBy);

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User {UserId} created successfully.", user.Id);

        return user.Id;
    }

}
