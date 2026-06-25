using System.ComponentModel;
using System.Security.Claims;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;
using UserManagementApi.Application.DTOs;
using UserManagementApi.Domain.Interfaces;

namespace UserManagementApi.Application.Commands;

public record LoginUserCommand(string Email, string Password) : IRequest<LoginUserResultDTO>;

public class LoginUserCommandHandler(IUserRepository userRepository, ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, LoginUserResultDTO>
{
    public async Task<LoginUserResultDTO> Handle(LoginUserCommand request, CancellationToken token)
    {
        var user = await userRepository.FindByEmailUserMan(request.Email);
        if(user is null)
        {
            throw new Exception("User does not exist in our database");
        }
        var passwordCorrect = await userRepository.CheckPasswordUserMan(user, request.Password);
        if(!passwordCorrect)
        {
            throw new Exception("Uncorrect password");
        }

        var Token = tokenProvider.Create(user);
        return new LoginUserResultDTO(
            FullName: user.FullName!,
            Email: user.Email!,
            Token: Token
        );
    }
}
