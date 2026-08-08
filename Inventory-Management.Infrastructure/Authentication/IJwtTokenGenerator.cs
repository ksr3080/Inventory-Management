using Inventory_Management.Domain.Entities;

namespace Inventory_Management.Infrastructure.Authentication;

public interface IJwtTokenGenerator
{
    (string Token, DateTime Expiration) GenerateToken(Users user);
}
