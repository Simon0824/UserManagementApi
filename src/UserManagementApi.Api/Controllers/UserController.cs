using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Application.DTOs;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;

namespace UserManagementApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUserToDb([FromBody] AddUserDTO dto)
    {
        UserEntity user = new()
        {
          Id = Guid.NewGuid(),
          FullName = dto.FullName,
          Email = dto.Email,
          Password = dto.Password
        };
        var result = await userRepository.AddUser(user);
        return Ok(result);

    }
}
