using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasData(
                new Role { Id = Guid.Parse("D1A2B3C4-E5F6-4A7B-8C9D-0E1F2A3B4C5D"), Name = "Admin", Code = SystemRole.Admin },
                new Role { Id = Guid.Parse("E2A3B4C5-D6F7-4B8C-9D0E-1F2A3B4C5D6E"), Name = "Technician", Code = SystemRole.Technician },
                new Role { Id = Guid.Parse("F3A4B5C6-E7D8-4C9D-0E1F-2A3B4C5D6E7F"), Name = "User", Code = SystemRole.User }
            );
        }
    }
}
