using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Application.Commands;
using UserManagementApi.Application.DTOs;
using UserManagementApi.Domain.Entities;
using UserManagementApi.Domain.Interfaces;

namespace UserManagementApi.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class UsersController(ISender sender) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("Auth/Register")]
    public async Task<IActionResult> RegisterUserToDb([FromBody] RegisterUserDTO dto)
    {
        var registerResult = await sender.Send(new RegisterUserCommand(
            dto.FullName,
            dto.Email,
            dto.Password
        ));
        return Ok(registerResult);
    }

    [AllowAnonymous]
    [HttpPost("Auth/Login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO dto)
    {
        var loginResult =  await sender.Send(new LoginUserCommand(
            dto.Email,
            dto.Password
        ));
        return Ok(loginResult);
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsersFromDb()
    {
        return Ok();
    }
}
