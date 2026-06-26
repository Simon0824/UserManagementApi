using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;

namespace UserManagementApi.Infrastructure.Repositories;

public class CachedUserRepository(IUserRepository decorated, IDistributedCache cache) : IUserRepository
{
    private const string cachedKey = "users";

    private DistributedCacheEntryOptions cachedOptions = new ()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
    };
    public async Task AddRoleToUser(UserEntity user)
    {
        await decorated.AddRoleToUser(user);
        await cache.RemoveAsync(cachedKey);
    }

    public async Task BanUser(UserEntity user)
    {
        await decorated.BanUser(user);
        await cache.RemoveAsync(cachedKey);
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

        string? cachedUser = await cache.GetStringAsync(cachedKey);

        IEnumerable<UserEntity>? userEntity;

        if(string.IsNullOrEmpty(cachedUser))
        {
            userEntity = await decorated.GetUsers();

            if(userEntity is null)
            {
                return userEntity!;
            }
            await cache.SetStringAsync(
                cachedKey, JsonSerializer.Serialize(userEntity), cachedOptions
            );

            return userEntity;
        }

        userEntity = JsonSerializer.Deserialize<IEnumerable<UserEntity>>(cachedUser);

        return userEntity!;
    }

    public async Task<IdentityResult> UserRegister(UserEntity user, string Password)
    {
        var result = await decorated.UserRegister(user, Password);
        if(result.Succeeded)
        {
            await cache.RemoveAsync(cachedKey);
        }
        return result;
    }

}
