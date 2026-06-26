using Microsoft.AspNetCore.Identity;
using UserManagementApi.Domain.Entities;

namespace UserManagementApi.Domain.Interfaces;

public interface IUserRepository
{
    Task<IdentityResult> UserRegister(UserEntity user, string Password);
    Task<UserEntity> FindByEmailUserMan(string Email);
    Task<bool> CheckPasswordUserMan(UserEntity user, string Password);
    Task AddRoleToUser(UserEntity user);
    Task<IList<string>> GetUserRole(UserEntity user);
    Task BanUser(UserEntity user);
    Task<IEnumerable<UserEntity>> GetUsers();
}
