using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Infrastructure.Configurations;

public static class CommonConfigurationExtensions
{
    public static void ConfigureCommon<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : Common
    {
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedBy)
            .IsRequired(false);

        builder.Property(x => x.CreatedOn)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .IsRequired(false);

        builder.Property(x => x.UpdatedOn)
            .IsRequired(false);
    }
}