using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Configurations;

namespace HotelManagement.Infrastructure;

public class FormsConfiguration : IEntityTypeConfiguration<Forms>
{
    public void Configure(EntityTypeBuilder<Forms> builder)
    {
        builder.ToTable("Forms");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FormCode)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(x => x.FormName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(x => x.ParentId)
            .IsRequired();

        builder.Property(x => x.FormUrl)
            .IsRequired(false)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(x => x.MenuIconClass)
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(x => x.ShowInMenu)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.IsAddEdit)
            .IsRequired();

        builder.Property(x => x.IsView)
            .IsRequired();

        builder.Property(x => x.DisplayIndex)
            .IsRequired();

        // Common Properties
        builder.ConfigureCommon();
    }
}