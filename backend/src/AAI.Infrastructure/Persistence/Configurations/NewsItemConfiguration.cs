using AAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuração do Entity Framework Core para a entidade NewsItem
/// </summary>
public class NewsItemConfiguration : IEntityTypeConfiguration<NewsItem>
{
    public void Configure(EntityTypeBuilder<NewsItem> builder)
    {
        builder.ToTable("NewsItems");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(n => n.Source)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(n => n.SourceUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(n => n.PublishedAt)
            .IsRequired();

        builder.Property(n => n.Content)
            .HasMaxLength(10000);

        builder.Property(n => n.AISummary)
            .HasMaxLength(500);

        builder.Property(n => n.Sentiment)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(n => n.RelevanceScore)
            .HasPrecision(5, 2);

        builder.Property(n => n.RelatedAssetsJson)
            .HasColumnType("TEXT");

        builder.Property(n => n.IsRead)
            .IsRequired();

        builder.HasIndex(n => n.PublishedAt)
            .HasDatabaseName("IX_NewsItems_PublishedAt");

        builder.HasIndex(n => n.IsRead)
            .HasDatabaseName("IX_NewsItems_IsRead");
    }
}
