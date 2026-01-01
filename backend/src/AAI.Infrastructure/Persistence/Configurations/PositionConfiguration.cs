using AAI.Domain.Entities;
using AAI.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Positions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PortfolioId)
            .IsRequired();

        builder.Property(p => p.AssetId)
            .IsRequired();

        builder.Property(p => p.Quantity)
            .IsRequired()
            .HasPrecision(18, 8);

        // AverageCost as owned entity (Money value object)
        builder.OwnsOne(p => p.AverageCost, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("AverageCost")
                .HasPrecision(18, 8);

            money.Property(m => m.Currency)
                .HasColumnName("AverageCostCurrency")
                .HasMaxLength(3)
                .HasDefaultValue("BRL");
        });

        // TotalInvested as owned entity
        builder.OwnsOne(p => p.TotalInvested, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("TotalInvested")
                .HasPrecision(18, 8);

            money.Property(m => m.Currency)
                .HasColumnName("TotalInvestedCurrency")
                .HasMaxLength(3)
                .HasDefaultValue("BRL");
        });

        // CurrentValue as owned entity
        builder.OwnsOne(p => p.CurrentValue, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("CurrentValue")
                .HasPrecision(18, 8);

            money.Property(m => m.Currency)
                .HasColumnName("CurrentValueCurrency")
                .HasMaxLength(3)
                .HasDefaultValue("BRL");
        });

        builder.Property(p => p.AllocationPercent)
            .IsRequired()
            .HasPrecision(5, 2);

        // UnrealizedGainLoss as owned entity
        builder.OwnsOne(p => p.UnrealizedGainLoss, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("UnrealizedGainLoss")
                .HasPrecision(18, 8);

            money.Property(m => m.Currency)
                .HasColumnName("UnrealizedGainLossCurrency")
                .HasMaxLength(3)
                .HasDefaultValue("BRL");
        });

        // UnrealizedGainLossPercent as owned entity
        builder.OwnsOne(p => p.UnrealizedGainLossPercent, percent =>
        {
            percent.Property(pct => pct.Value)
                .HasColumnName("UnrealizedGainLossPercent")
                .HasPrecision(8, 4);
        });

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationship: 1:N with Transaction
        builder.HasMany(p => p.Transactions)
            .WithOne(t => t.Position)
            .HasForeignKey(t => t.PositionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(p => p.PortfolioId);
        builder.HasIndex(p => p.AssetId);
        builder.HasIndex(p => new { p.PortfolioId, p.AssetId }).IsUnique();
        builder.HasIndex(p => p.IsDeleted);
    }
}
