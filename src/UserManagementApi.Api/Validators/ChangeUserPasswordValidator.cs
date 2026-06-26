using System.Data;
using FluentValidation;
using UserManagementApi.Application.DTOs;

namespace UserManagementApi.Api.Validators;

public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordDTO>
{
    public ChangeUserPasswordValidator()
    {
        RuleFor(user => user.Password1)
               .NotEmpty()
               .WithMessage("First password is not entered");
        
        RuleFor(user => user.Password2)
               .NotEmpty()
               .WithMessage("Second password is not entered")
               .Equal(pass => pass.Password1)
               .WithMessage("Password are not equal");
    }
}
