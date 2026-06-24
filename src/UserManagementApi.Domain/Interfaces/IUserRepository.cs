using Microsoft.AspNetCore.Identity;
using UserManagementApi.Domain.Entities;

namespace UserManagementApi.Domain.Interfaces;

public interface IUserRepository
{
    Task<IdentityResult> UserRegister(UserEntity user, string Password);
}
