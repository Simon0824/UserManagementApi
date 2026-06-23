using System.Formats.Asn1;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using UserManagementApi.Application.DTOs;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;
using UserManagementApi.Infrastructure.Data;

namespace UserManagementApi.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<UserEntity> AddUser(UserEntity userEntity)
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
}
