using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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

        var role = await userRepository.GetUserRole(user);

        List<Claim> claims = [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                ..role.Select(r => new Claim(ClaimTypes.Role, r))
        ];

        var Token = tokenProvider.Create(user, claims);

        return new LoginUserResultDTO(
            FullName: user.FullName!,
            Email: user.Email!,
            Role: role,
            Token: Token
        );
    }
}
