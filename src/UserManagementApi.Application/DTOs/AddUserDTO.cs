namespace UserManagementApi.Application.DTOs;

public record AddUserDTO(
    string FullName,
    string Email,
    string Password
);
