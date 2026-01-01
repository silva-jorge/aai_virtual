using AAI.Domain.Entities;
using AAI.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("Assets");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Ticker)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.AssetClass)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(a => a.Exchange)
            .HasMaxLength(50);

        builder.Property(a => a.Sector)
            .HasMaxLength(100);

        builder.Property(a => a.Currency)
            .IsRequired()
            .HasMaxLength(3)
            .HasDefaultValue("BRL");

        // CurrentPrice as owned entity (Money value object)
        builder.OwnsOne(a => a.CurrentPrice, price =>
        {
            price.Property(m => m.Amount)
                .HasColumnName("CurrentPrice")
                .HasPrecision(18, 8);

            price.Property(m => m.Currency)
                .HasColumnName("CurrentPriceCurrency")
                .HasMaxLength(3)
                .HasDefaultValue("BRL");
        });

        builder.Property(a => a.LastPriceUpdate);

        builder.Property(a => a.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(a => a.IsManualEntry)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.UpdatedAt);

        builder.Property(a => a.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationship: 1:N with Position
        builder.HasMany(a => a.Positions)
            .WithOne(p => p.Asset)
            .HasForeignKey(p => p.AssetId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(a => a.Ticker).IsUnique();
        builder.HasIndex(a => a.AssetClass);
        builder.HasIndex(a => a.IsActive);
        builder.HasIndex(a => a.IsDeleted);
    }
}
