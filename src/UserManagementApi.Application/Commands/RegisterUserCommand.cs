using MediatR;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;

namespace UserManagementApi.Application.Commands;

public record RegisterUserCommand(string FullName, string Email, string Password) : IRequest<UserEntity>;

public class RegisterUserCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, UserEntity>
{
    public async Task<UserEntity> Handle(RegisterUserCommand request, CancellationToken token)
    {
        UserEntity user = new ()
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email
        };
        var result = await userRepository.UserRegister(user, request.Password);
        if(!result.Succeeded)
        {
            throw new Exception("User cannot be registered");
        }
        await userRepository.AddRoleToUser(user);
        
        return user;
    }
}
