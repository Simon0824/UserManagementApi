using MediatR;
using UserManagementApi.Domain.Interfaces;

namespace UserManagementApi.Application.Commands;

public record ChangeUserPasswordCommand(string Email, string currentPassword, string Password1, string Password2) : IRequest<bool>;

public class ChangeUserPasswordCommandHandler(IUserRepository userRepository) : IRequestHandler<ChangeUserPasswordCommand, bool>
{
    public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken token)
    {
        var user = await userRepository.FindByEmailUserMan(request.Email);
        if(user is null)
        {
            throw new Exception("User not found");
        }
        await userRepository.CheckPasswordUserMan(user, request.currentPassword);
        var result = await userRepository.ChangeUserPassword(user, request.currentPassword, request.Password1);
        if(!result.Succeeded)
        {
            throw new Exception("Failed changing users password");
        }
        return true;
    }
}
