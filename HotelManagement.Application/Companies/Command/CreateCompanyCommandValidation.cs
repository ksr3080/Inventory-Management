using FluentValidation;

namespace HotelManagement.Application.Companies.Command;

public class CreateCompanyCommandValidation : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(50).WithMessage("Full name cannot exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Please enter a valid email address.")
            .MaximumLength(50).WithMessage("Email cannot exceed 50 characters.");

        RuleFor(x => x.PhoneNo)
            .NotEmpty().WithMessage("PhoneNo is required.")
            .Matches(@"^\d{10}$").WithMessage("Please enter a valid 10-digit phone number.");

        RuleFor(x => x.Address1)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(256).WithMessage("Address cannot exceed 256 characters.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.")
            .MaximumLength(50).WithMessage("State cannot exceed 50 characters.");

        RuleFor(x => x.StateCode)
            .NotEmpty().WithMessage("State Code is required.")
            .MaximumLength(10).WithMessage("State Code cannot exceed 10 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City cannot exceed 50 characters.");

        RuleFor(x => x.Pincode)
            .NotEmpty().WithMessage("Pincode is required.")
            .Matches(@"^\d{6}$").WithMessage("Please enter a valid 6-digit pincode.");

        RuleFor(x => x.GSTNo)
            .NotEmpty().WithMessage("GST NO is required.")
            .Matches(@"^\d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}Z[A-Z\d]{1}$")
            .WithMessage("Please enter a valid GST NO.")
            .MaximumLength(20).WithMessage("GST NO cannot exceed 20 characters.");

        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).WithMessage("CreatedBy user id is required.");
    }
}