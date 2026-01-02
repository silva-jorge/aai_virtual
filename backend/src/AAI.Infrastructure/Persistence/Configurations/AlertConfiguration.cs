using AAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração do Entity Framework Core para a entidade Alert
/// </summary>
public class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> builder)
    {
        builder.ToTable("Alerts");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.UserId)
            .IsRequired();

        builder.Property(a => a.AlertType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(a => a.Condition)
            .IsRequired()
            .HasColumnType("TEXT");

        builder.Property(a => a.IsActive)
            .IsRequired();

        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Asset)
            .WithMany()
            .HasForeignKey(a => a.AssetId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(a => a.History)
            .WithOne(h => h.Alert)
            .HasForeignKey(h => h.AlertId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => new { a.UserId, a.IsActive })
            .HasDatabaseName("IX_Alerts_UserId_IsActive");
    }
}
