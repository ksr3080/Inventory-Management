using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Configurations;

namespace HotelManagement.Infrastructure
{
    public class FormBusinessRulesConfiguration : IEntityTypeConfiguration<FormBusinessRules>
    {
        public void Configure(EntityTypeBuilder<FormBusinessRules> builder)
        {
            builder.ToTable("FormBusinessRules");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FormId)
                .IsRequired();

            builder.Property(x => x.Rules)
                .HasColumnType("nvarchar(250)")
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(x => x.RulesCode)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.RulesValue)
                .IsRequired()
                .HasDefaultValue(false);

            // Optional relationship
            builder.HasOne<Forms>()
                .WithMany()
                .HasForeignKey(x => x.FormId)
                .OnDelete(DeleteBehavior.Restrict);

            // Common properties
            builder.ConfigureCommon();
        }
    }
}
