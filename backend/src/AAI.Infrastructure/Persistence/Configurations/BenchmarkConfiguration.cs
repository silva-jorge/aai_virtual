using AAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração do Entity Framework Core para a entidade Benchmark
/// </summary>
public class BenchmarkConfiguration : IEntityTypeConfiguration<Benchmark>
{
    public void Configure(EntityTypeBuilder<Benchmark> builder)
    {
        builder.ToTable("Benchmarks");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Symbol)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(b => b.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(b => b.Description)
            .HasMaxLength(500);

        builder.Property(b => b.IsActive)
            .IsRequired();

        builder.HasMany(b => b.Values)
            .WithOne(v => v.Benchmark)
            .HasForeignKey(v => v.BenchmarkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(b => b.Symbol)
            .IsUnique()
            .HasDatabaseName("IX_Benchmarks_Symbol");

        builder.HasIndex(b => b.IsActive)
            .HasDatabaseName("IX_Benchmarks_IsActive");
    }
}
