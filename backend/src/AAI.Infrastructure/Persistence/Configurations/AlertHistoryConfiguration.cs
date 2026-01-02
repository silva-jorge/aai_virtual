using AAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração do Entity Framework Core para a entidade AlertHistory
/// </summary>
public class AlertHistoryConfiguration : IEntityTypeConfiguration<AlertHistory>
{
    public void Configure(EntityTypeBuilder<AlertHistory> builder)
    {
        builder.ToTable("AlertHistories");

        builder.HasKey(ah => ah.Id);

        builder.Property(ah => ah.AlertId)
            .IsRequired();

        builder.Property(ah => ah.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ah => ah.Message)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(ah => ah.TriggeredAt)
            .IsRequired();

        builder.Property(ah => ah.IsRead)
            .IsRequired();

        builder.Property(ah => ah.RelatedEntityType)
            .HasMaxLength(100);

        builder.HasOne(ah => ah.Alert)
            .WithMany(a => a.History)
            .HasForeignKey(ah => ah.AlertId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ah => new { ah.AlertId, ah.TriggeredAt })
            .HasDatabaseName("IX_AlertHistories_AlertId_TriggeredAt");

        builder.HasIndex(ah => ah.IsRead)
            .HasDatabaseName("IX_AlertHistories_IsRead");
    }
}
