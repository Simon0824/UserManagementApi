using UserManagementApi.Domain.Enums;

namespace UserManagementApi.Application.DTOs;

public record BanUserResultDTO(
    string Email,
    string Status
);
