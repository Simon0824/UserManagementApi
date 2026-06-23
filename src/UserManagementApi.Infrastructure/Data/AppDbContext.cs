using Microsoft.EntityFrameworkCore;
using UserManagementApi.Domain.Entities;

namespace UserManagementApi.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> users {get; set;}
}
