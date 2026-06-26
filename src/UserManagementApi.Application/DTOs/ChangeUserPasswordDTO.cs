namespace UserManagementApi.Application.DTOs;

public record ChangeUserPasswordDTO(
    string currentPassword,
    string Password1,
    string Password2
);
