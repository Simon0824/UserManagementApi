namespace UserManagementApi.Application.DTOs;

public record RegisterUserDTO(
    string FullName,
    string Email,
    string Password
);
