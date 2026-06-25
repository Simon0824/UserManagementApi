using MediatR;
using UserManagementApi.Application.DTOs;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Enums;
using UserManagementApi.Domain.Interfaces;

namespace UserManagementApi.Application.Commands;

public record BanUserCommand(string Email) : IRequest<BanUserResultDTO>;

public class BanUserCommandHandler(IUserRepository userRepository) : IRequestHandler<BanUserCommand, BanUserResultDTO>
{
    public async Task<BanUserResultDTO> Handle(BanUserCommand request, CancellationToken token)
    {
        var user = await userRepository.FindByEmailUserMan(request.Email);
        if(user is null)
        {
            throw new Exception("User does not exist");
        }

        await userRepository.BanUser(user);
        return new BanUserResultDTO(
            Email: user.Email!,
            Status: user.Status.ToString()
        );
    }
}