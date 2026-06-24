using UserManagementApi.Domain.Entities;

namespace UserManagementApi.Domain.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> RegisterUser(UserEntity userEntity);
    Task<bool> ExistByEmailAsync(string Email);
    Task<UserEntity> CheckByEmailAndPassword(string Email, string Password);
    Task<IEnumerable<UserEntity>> GetAllUsers();
}
