using AAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração do Entity Framework Core para a entidade MarketEvent
/// </summary>
public class MarketEventConfiguration : IEntityTypeConfiguration<MarketEvent>
{
    public void Configure(EntityTypeBuilder<MarketEvent> builder)
    {
        builder.ToTable("MarketEvents");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.EventType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(m => m.SourceUrl)
            .HasMaxLength(500);

        builder.Property(m => m.PublishedAt)
            .IsRequired();

        builder.Property(m => m.AffectedAssetsJson)
            .HasColumnType("TEXT");

        builder.Property(m => m.ImpactAnalysis)
            .HasMaxLength(2000);

        builder.Property(m => m.Severity)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(m => m.IsProcessed)
            .IsRequired();

        builder.Property(m => m.IsAlertSent)
            .IsRequired();

        builder.HasIndex(m => m.PublishedAt)
            .HasDatabaseName("IX_MarketEvents_PublishedAt");

        builder.HasIndex(m => m.IsProcessed)
            .HasDatabaseName("IX_MarketEvents_IsProcessed")
            .HasFilter("IsProcessed = 0");
    }
}
