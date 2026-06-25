using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementApi.Domain.Entities;

namespace UserManagementApi.Infrastructure.Configurations;

public class UserStatusConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(x => x.Status)
               .HasConversion<string>();
    }
}
