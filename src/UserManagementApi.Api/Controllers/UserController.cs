using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Application.DTOs;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;

namespace UserManagementApi.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserRepository userRepository, ITokenProvider tokenProvider) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUserToDb([FromBody] AddUserDTO dto)
    {
        UserEntity user = new()
        {
          Id = Guid.NewGuid(),
          FullName = dto.FullName,
          Email = dto.Email,
          Password = dto.Password
        };
        var result = await userRepository.RegisterUser(user);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO dto)
    {
        var user = await userRepository.CheckByEmailAndPassword(dto.Email, dto.Password);
        var token = tokenProvider.Create(user);
        return Ok(new {Message = "You have logged succesfully", user.Email, token});
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsersFromDb()
    {
      var users = await userRepository.GetAllUsers();
      var dto = users.Select(u => new UserDTO(
                u.Id,
                u.FullName,
                u.Email
                ));
        return Ok(dto);
    }
}
