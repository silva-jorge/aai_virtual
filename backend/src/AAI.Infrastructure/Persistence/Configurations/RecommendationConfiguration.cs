using AAI.Domain.Entities;
using AAI.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
{
    public void Configure(EntityTypeBuilder<Recommendation> builder)
    {
        builder.ToTable("Recommendations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.PortfolioId)
            .IsRequired();

        builder.Property(r => r.ActionType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(r => r.Ticker)
            .HasMaxLength(20);

        builder.Property(r => r.Quantity)
            .HasColumnType("decimal(18,8)");

        builder.Property(r => r.EstimatedValue)
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(r => r.Rationale)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(r => r.ImpactJson)
            .HasColumnType("TEXT");

        builder.Property(r => r.Priority)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(r => r.RejectionReason)
            .HasMaxLength(500);

        // Relationships
        builder.HasOne(r => r.Portfolio)
            .WithMany()
            .HasForeignKey(r => r.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(r => r.PortfolioId);
        builder.HasIndex(r => r.Status);
        builder.HasIndex(r => r.CreatedAt);
    }
}
