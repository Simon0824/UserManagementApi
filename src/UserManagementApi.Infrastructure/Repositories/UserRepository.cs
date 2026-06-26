using System.Collections;
using System.Diagnostics.Contracts;
using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using UserManagementApi.Application.DTOs;
using UserManagementApi.Domain.Constants;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Enums;
using UserManagementApi.Domain.Interfaces;
using UserManagementApi.Infrastructure.Data;

namespace UserManagementApi.Infrastructure.Repositories;

public class UserRepository(UserManager<UserEntity> userManager) : IUserRepository
{
    public async Task<IdentityResult> UserRegister(UserEntity user, string Password)
    {
        return await userManager.CreateAsync(user, Password);
    }

    public async Task<UserEntity> FindByEmailUserMan(string Email)
    {
        return await userManager.FindByEmailAsync(Email);
    }

    public async Task<bool> CheckPasswordUserMan(UserEntity user, string Password)
    {
        return await userManager.CheckPasswordAsync(user, Password);
    }
    public async Task AddRoleToUser(UserEntity user)
    {
        await userManager.AddToRoleAsync(user, Roles.Member);
    }

    public async Task<IList<string>> GetUserRole(UserEntity user)
    {
        return await userManager.GetRolesAsync(user);
    }

    public async Task BanUser(UserEntity user)
    {
        user.Status = UserStatus.Banned;
        await userManager.UpdateAsync(user);
    }

    public async Task<IEnumerable<UserEntity>> GetUsers()
    {
        return userManager.Users.AsNoTracking().ToList();
    }
}
