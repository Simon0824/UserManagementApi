using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Application.Commands;
using UserManagementApi.Application.DTOs;
using UserManagementApi.Application.Queries;
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

    [Authorize(Roles = "Admin")]
    [HttpPost("BanUser")]
    public async Task<IActionResult> BanUser([FromBody] BanUserDTO dto)
    {
        var banResult = await sender.Send(new BanUserCommand(dto.Email));
        return Ok(banResult);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await sender.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpPost("ChangeUserPassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordDTO dto)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        var isChanged = await sender.Send(new ChangeUserPasswordCommand(
            userEmail!,
            dto.currentPassword,
            dto.Password1,
            dto.Password2));
        return Ok(new {isChanged, Message = "Password changed"});
    }
}
