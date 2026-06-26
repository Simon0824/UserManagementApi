using MediatR;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;

namespace UserManagementApi.Application.Queries;

public record GetAllUsersQuery() : IRequest<IEnumerable<UserEntity>>;
public class GetAllUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserEntity>>
{
    public async Task<IEnumerable<UserEntity>> Handle(GetAllUsersQuery request, CancellationToken token)
    {
        var users = await userRepository.GetUsers();
        return users;
    }
}
