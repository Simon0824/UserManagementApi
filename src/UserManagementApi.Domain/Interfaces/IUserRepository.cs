using UserManagementApi.Domain.Entities;

namespace UserManagementApi.Domain.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> AddUser(UserEntity userEntity);
}
