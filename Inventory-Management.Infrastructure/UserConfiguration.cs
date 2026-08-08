using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Inventory_Management.Domain.Entities;
using Inventory_Management.Infrastructure.Configurations;

namespace Inventory_Management.Infrastructure;

public class UserConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(x => x.EmailId)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)");

        builder.Property(x => x.ContactNo)
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(x => x.IsLoginAllowed)
            .IsRequired();

        builder.Property(x => x.UserTypeId)
            .IsRequired();

        builder.Property(x => x.UserType)
            .HasMaxLength(20)
            .HasColumnType("varchar(20)");

        // Common Properties
        builder.ConfigureCommon();
    }
}