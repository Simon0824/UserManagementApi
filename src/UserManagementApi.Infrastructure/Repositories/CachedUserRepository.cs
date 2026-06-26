using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;

namespace UserManagementApi.Infrastructure.Repositories;

public class CachedUserRepository(IUserRepository decorated, IMemoryCache memoryCache) : IUserRepository
{

    public Task AddRoleToUser(UserEntity user)
    {
        return decorated.AddRoleToUser(user);
    }

    public Task BanUser(UserEntity user)
    {
        return decorated.BanUser(user);
    }

    public Task<IdentityResult> ChangeUserPassword(UserEntity user, string currentPassword, string newPassword)
    {
        return decorated.ChangeUserPassword(user, currentPassword, newPassword);
    }

    public Task<bool> CheckPasswordUserMan(UserEntity user, string Password)
    {
        return decorated.CheckPasswordUserMan(user, Password);
    }

    public Task<UserEntity> FindByEmailUserMan(string Email)
    {
        return decorated.FindByEmailUserMan(Email);
    }

    public Task<IList<string>> GetUserRole(UserEntity user)
    {
        return decorated.GetUserRole(user);
    }

    public async Task<IEnumerable<UserEntity>> GetUsers()
    {
        var key = "users";

        return (await  memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return decorated.GetUsers();
            }
        ))!;
    }

    public Task<IdentityResult> UserRegister(UserEntity user, string Password)
    {
        return decorated.UserRegister(user, Password);
    }

}
