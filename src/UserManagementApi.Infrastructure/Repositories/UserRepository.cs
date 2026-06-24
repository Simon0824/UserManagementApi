using System.Collections;
using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using UserManagementApi.Application.DTOs;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;
using UserManagementApi.Infrastructure.Data;

namespace UserManagementApi.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<UserEntity> RegisterUser(UserEntity userEntity)
    {
        UserEntity user = new ()
        {
          Id = Guid.NewGuid(),
          FullName = userEntity.FullName,
          Email = userEntity.Email,
          Password = userEntity.Password
        };

        var findUser = await ExistByEmailAsync(userEntity.Email);
        if(findUser)
        {
            throw new Exception("User already exist in database");
        }
        dbContext.users.Add(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> ExistByEmailAsync(string Email)
    {
        return await dbContext.users.AnyAsync(x => x.Email == Email);
    }

    public async Task<UserEntity> CheckByEmailAndPassword(string Email, string Password)
    {
        var user = await dbContext.users.FirstOrDefaultAsync(x => x.Email == Email && x.Password == Password);
        if(user is null)
        {
            throw new Exception("User does not exist in database");
        }
        return user;
    }

    public async Task<IEnumerable<UserEntity>> GetAllUsers()
    {
      return await dbContext.users
        .AsNoTracking()
        .Select(u => new UserEntity
        {
           Id = u.Id,
           FullName = u.FullName,
           Email = u.Email
        })
        .ToListAsync();
    }
}
