using FluentValidation;

namespace Employee.Management.Application.DTOs.Auth.Validators
{
    public class CreateUserRequestDtoValidator : AbstractValidator<CreateUserRequestDto>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public CreateUserRequestDtoValidator()
        {
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("{Property} is requried")
                ;
            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("{Property} is requried");

            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("{Property} is requried").EmailAddress().WithMessage("Email is not valid");

            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("{Property} is requried");

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("{Property} is requried");
        }
    }
}
