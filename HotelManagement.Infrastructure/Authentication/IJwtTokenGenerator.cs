using HotelManagement.Domain.Entities;

namespace HotelManagement.Infrastructure.Authentication;

public interface IJwtTokenGenerator
{
    (string Token, DateTime Expiration) GenerateToken(Users user);
}
