using Microsoft.AspNetCore.Identity;
using UserManagementApi.Domain.Constants;

namespace UserManagementApi.Domain.Entities;


public class UserEntity : IdentityUser
{
    public string? FullName {get; set;}
    public string? Password {get ; set;}
    public string? Status {get; set;}
}
