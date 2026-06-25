namespace UserManagementApi.Application.DTOs;

public record LoginUserResultDTO(
    string FullName,
    string Email,
    IList<string> Role,
    string Token
);
