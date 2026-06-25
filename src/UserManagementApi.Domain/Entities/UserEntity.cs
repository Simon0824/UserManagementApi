using Microsoft.AspNetCore.Identity;
using UserManagementApi.Domain.Constants;
using UserManagementApi.Domain.Enums;

namespace UserManagementApi.Domain.Entities;


public class UserEntity : IdentityUser
{
    public string? FullName {get; set;}
    public UserStatus Status {get; set;}
}
