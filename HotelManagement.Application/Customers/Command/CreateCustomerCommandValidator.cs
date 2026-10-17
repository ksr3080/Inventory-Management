using FluentValidation;

namespace HotelManagement.Application.Customers.Command;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.GSTNo)
            .NotEmpty()
            .Length(15);

        RuleFor(x => x.PhoneNo)
            .NotEmpty()
            .MaximumLength(15);

        RuleFor(x => x.Address1)
            .NotEmpty();

        RuleFor(x => x.State)
            .NotEmpty();

        RuleFor(x => x.StateCode)
            .NotEmpty();

        RuleFor(x => x.City)
            .NotEmpty();

        RuleFor(x => x.Pincode)
            .NotEmpty()
            .Length(6);

        RuleFor(x => x.Country)
            .NotEmpty();

        RuleFor(x => x.Ship_Address1)
            .NotEmpty();

        RuleFor(x => x.Ship_State)
            .NotEmpty();

        RuleFor(x => x.Ship_StateCode)
            .NotEmpty();

        RuleFor(x => x.Ship_City)
            .NotEmpty();

        RuleFor(x => x.Ship_Pincode)
            .NotEmpty()
            .Length(6);

        RuleFor(x => x.Ship_Country)
            .NotEmpty();

        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).WithMessage("CreatedBy user id is required.");
    }
}