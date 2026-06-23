using System.Formats.Asn1;
using System.Security;
using Microsoft.EntityFrameworkCore;
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
          Password = userEntity.Password,
          Email = userEntity.Email
        };

        var findUser = await dbContext.users.AnyAsync(x => x.Email == userEntity.Email);
        if(findUser)
        {
            throw new Exception("User already exist in database");
        }
        dbContext.users.Add(user);
        await dbContext.SaveChangesAsync();
        return user;
    }
}
