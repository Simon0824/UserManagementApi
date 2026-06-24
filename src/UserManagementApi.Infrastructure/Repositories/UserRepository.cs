using System.Collections;
using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using UserManagementApi.Application.DTOs;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;
using UserManagementApi.Infrastructure.Data;

namespace UserManagementApi.Infrastructure.Repositories;

public class UserRepository(UserManager<UserEntity> userManager) : IUserRepository
{
    public async Task<IdentityResult> UserRegister(UserEntity user, string Password)
    {
        return await userManager.CreateAsync(user, Password);
    }

    public async Task<UserEntity> CheckUserEmail(string Email)
    {
        var user = await userManager.FindByEmailAsync(Email);
        if(user is null)
        {
            throw new Exception("User does not exist in database!");
        }
        return user;
    }
}
