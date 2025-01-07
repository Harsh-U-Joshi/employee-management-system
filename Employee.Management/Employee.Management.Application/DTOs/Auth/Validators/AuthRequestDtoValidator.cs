using FluentValidation;

namespace Employee.Management.Application.DTOs.Auth.Validators;

public class AuthRequestDtoValidator : AbstractValidator<AuthRequestDto>
{
    public AuthRequestDtoValidator()
    {
        RuleFor(p => p.Email).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("{PropertyName} is required").EmailAddress().WithMessage("Invalid email format"); ;

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("{PropertyName} is required");
    }
}
