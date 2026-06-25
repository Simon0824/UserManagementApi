using System.Security.Claims;
using UserManagementApi.Domain.Entities;

namespace UserManagementApi.Domain.Interfaces;

public interface ITokenProvider
{
    string Create(UserEntity userEntity, List<Claim> claims);
}
