using System.Data;
using FluentValidation;
using UserManagementApi.Application.DTOs;

namespace UserManagementApi.Api.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Email)
               .NotNull()
               .WithMessage("You have to enter your email!")
               .EmailAddress()
               .WithMessage("Invalid Email adress!");
        
        RuleFor(user => user.Password)
               .NotNull()
               .WithMessage("You have to enter your password!")
               .MinimumLength(8)
               .WithMessage("Your password must have at least 8 characters!");
        
    }
}
