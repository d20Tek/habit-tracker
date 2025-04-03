using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HabitTracker.Api.Persistence.Configurations;

internal class HabitCompletionConfiguration : IEntityTypeConfiguration<HabitCompletion>
{
    public void Configure(EntityTypeBuilder<HabitCompletion> builder)
    {
        builder.HasKey(hc => hc.Id);

        builder.Property(hc => hc.Id)
               .ValueGeneratedOnAdd();

        builder.Property(hc => hc.CompletionDate)
               .IsRequired()
               .HasConversion(
                    v => v.ToString("yyyy-MM-dd HH:mm:ss zzz"),  // Store as string with offset
                    v => DateTimeOffset.Parse(v)                // Convert back to DateTimeOffset
               );

        builder.Property(hc => hc.CompletionCount)
               .IsRequired();

        builder.HasIndex(hc => hc.CompletionDate);
    }
}