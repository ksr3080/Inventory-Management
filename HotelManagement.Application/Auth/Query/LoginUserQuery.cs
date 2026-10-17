using MediatR;
using HotelManagement.Infrastructure.Authentication;
using HotelManagement.Infrastructure.Repositories.Interfaces;
using HotelManagement.Shared.DTOs.Auth;

namespace HotelManagement.Application.Auth.Query;

public class LoginUserQuery : IRequest<LoginResponseDTO?>
{
    public LoginRequestDTO Request { get; }

    public LoginUserQuery(LoginRequestDTO request)
    {
        Request = request;
    }
}

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginResponseDTO?>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUsersInRoleRepository _usersInRoleRepository;

    public LoginUserQueryHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IUsersInRoleRepository usersInRoleRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _usersInRoleRepository = usersInRoleRepository;
    }

    public async Task<LoginResponseDTO?> Handle(LoginUserQuery query, CancellationToken cancellationToken)
    {
        var request = query.Request;

        if (string.IsNullOrWhiteSpace(request.UserNameOrEmail) || string.IsNullOrWhiteSpace(request.Password))
        {
            return null;
        }

        var user = await _userRepository.GetByUserNameOrEmailAsync(request.UserNameOrEmail, cancellationToken);

        if (user == null)
        {
            return null;
        }

        if (!user.IsActive || !user.IsLoginAllowed || user.IsDeleted)
        {
            return null;
        }

        bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.Password);

        if (!isPasswordValid)
        {
            return null;
        }

        var (token, expiration) = _jwtTokenGenerator.GenerateToken(user);
        var userInRole = await _usersInRoleRepository.GetByUserIdAsync(user.Id, cancellationToken);

        return new LoginResponseDTO
        {
            Token = token,
            Expiration = expiration,
            UserId = user.Id,
            UserName = user.UserName,
            EmailId = user.EmailId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserType = user.UserType,
            UserTypeId = user.UserTypeId,
            RoleId = userInRole?.RoleId ?? 0,
            CompanyId = userInRole?.CompanyId ?? 0
        };
    }
}
