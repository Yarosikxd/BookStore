using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public partial class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id); 

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(b => b.Year)
                .IsRequired();

            builder.Property(b => b.Description)
                .HasMaxLength(1000); 
        }
    }
}
