using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public partial class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder) 
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(u => u.Email)
                .IsUnique(); 

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.Role)
                .HasConversion<string>() 
                .IsRequired();

        }
    }
}
