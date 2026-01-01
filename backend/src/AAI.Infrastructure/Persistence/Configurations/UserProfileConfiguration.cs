using AAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAI.Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UserProfiles");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.RiskProfile)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(u => u.InvestmentGoal)
            .HasMaxLength(500);

        builder.Property(u => u.VolatilityTolerance)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(u => u.TimeHorizonMonths)
            .IsRequired();

        builder.Property(u => u.RebalanceThresholdPercent)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(u => u.TargetAllocationJson)
            .IsRequired()
            .HasColumnType("TEXT");

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(u => u.PasswordSalt)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt);

        builder.Property(u => u.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationship: 1:1 with Portfolio
        builder.HasOne(u => u.Portfolio)
            .WithOne(p => p.User)
            .HasForeignKey<Portfolio>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(u => u.IsDeleted);
    }
}
