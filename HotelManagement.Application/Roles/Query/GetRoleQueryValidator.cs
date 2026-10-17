using FluentValidation;

namespace HotelManagement.Application.Roles.Query;

public class GetRoleQueryValidator : AbstractValidator<GetRoleQuery>
{
    public GetRoleQueryValidator()
    {
    }
}
