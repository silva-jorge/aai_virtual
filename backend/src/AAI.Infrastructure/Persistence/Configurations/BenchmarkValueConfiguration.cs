using AAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração do Entity Framework Core para a entidade BenchmarkValue
/// </summary>
public class BenchmarkValueConfiguration : IEntityTypeConfiguration<BenchmarkValue>
{
    public void Configure(EntityTypeBuilder<BenchmarkValue> builder)
    {
        builder.ToTable("BenchmarkValues");

        builder.HasKey(bv => bv.Id);

        builder.Property(bv => bv.BenchmarkId)
            .IsRequired();

        builder.Property(bv => bv.Date)
            .IsRequired();

        builder.Property(bv => bv.Value)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(bv => bv.DailyReturn)
            .HasPrecision(8, 4);

        builder.Property(bv => bv.AccumulatedReturn)
            .HasPrecision(8, 4);

        builder.HasOne(bv => bv.Benchmark)
            .WithMany(b => b.Values)
            .HasForeignKey(bv => bv.BenchmarkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(bv => new { bv.BenchmarkId, bv.Date })
            .IsUnique()
            .HasDatabaseName("IX_BenchmarkValues_BenchmarkId_Date");
    }
}
