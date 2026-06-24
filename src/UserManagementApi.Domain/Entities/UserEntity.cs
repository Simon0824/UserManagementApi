using Microsoft.AspNetCore.Identity;

namespace UserManagementApi.Domain.Entities;


public class UserEntity : IdentityUser
{
    public string? FullName {get; set;}
    public string? Password {get ; set;}
}
