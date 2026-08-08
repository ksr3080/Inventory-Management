using FluentValidation;

namespace Inventory_Management.Application.Roles.Query;

public class GetRoleQueryValidator : AbstractValidator<GetRoleQuery>
{
    public GetRoleQueryValidator()
    {
    }
}
