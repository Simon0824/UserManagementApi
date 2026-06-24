namespace UserManagementApi.Application.DTOs;

public record UserDTO(
    Guid Id,
    string FullName,
    string Email
);