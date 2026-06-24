namespace UserManagementApi.Application.DTOs;

public record LoginUserDTO(
    string Email,
    string Password
);