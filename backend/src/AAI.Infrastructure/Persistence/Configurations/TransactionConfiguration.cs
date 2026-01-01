using AAI.Domain.Entities;
using AAI.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.PositionId)
            .IsRequired();

        builder.Property(t => t.TransactionType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(t => t.Quantity)
            .IsRequired()
            .HasPrecision(18, 8);

        // UnitPrice as owned entity (Money value object)
        builder.OwnsOne(t => t.UnitPrice, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("UnitPrice")
                .HasPrecision(18, 8);

            money.Property(m => m.Currency)
                .HasColumnName("UnitPriceCurrency")
                .HasMaxLength(3)
                .HasDefaultValue("BRL");
        });

        // TotalValue as owned entity
        builder.OwnsOne(t => t.TotalValue, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("TotalValue")
                .HasPrecision(18, 8);

            money.Property(m => m.Currency)
                .HasColumnName("TotalValueCurrency")
                .HasMaxLength(3)
                .HasDefaultValue("BRL");
        });

        // Fees as owned entity
        builder.OwnsOne(t => t.Fees, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("Fees")
                .HasPrecision(18, 8);

            money.Property(m => m.Currency)
                .HasColumnName("FeesCurrency")
                .HasMaxLength(3)
                .HasDefaultValue("BRL");
        });

        builder.Property(t => t.TransactionDate)
            .IsRequired();

        builder.Property(t => t.Broker)
            .HasMaxLength(100);

        builder.Property(t => t.Notes)
            .HasMaxLength(1000);

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.UpdatedAt);

        builder.Property(t => t.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Indexes
        builder.HasIndex(t => t.PositionId);
        builder.HasIndex(t => t.TransactionDate);
        builder.HasIndex(t => t.TransactionType);
        builder.HasIndex(t => t.IsDeleted);
    }
}
