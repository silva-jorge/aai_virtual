using AAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração do Entity Framework Core para a entidade PriceHistory
/// </summary>
public class PriceHistoryConfiguration : IEntityTypeConfiguration<PriceHistory>
{
    public void Configure(EntityTypeBuilder<PriceHistory> builder)
    {
        builder.ToTable("PriceHistories");

        builder.HasKey(ph => ph.Id);

        builder.Property(ph => ph.AssetId)
            .IsRequired();

        builder.Property(ph => ph.Date)
            .IsRequired();

        builder.Property(ph => ph.OpenPrice)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(ph => ph.HighPrice)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(ph => ph.LowPrice)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(ph => ph.ClosePrice)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(ph => ph.AdjustedClose)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(ph => ph.Volume)
            .IsRequired();

        builder.HasOne(ph => ph.Asset)
            .WithMany()
            .HasForeignKey(ph => ph.AssetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ph => new { ph.AssetId, ph.Date })
            .IsUnique()
            .HasDatabaseName("IX_PriceHistories_AssetId_Date");
    }
}
