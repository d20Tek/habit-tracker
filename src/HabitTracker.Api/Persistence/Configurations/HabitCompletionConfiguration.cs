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
                    // convert DateTimeOffset back and forth to string for Sqlite support.
                    v => v.ToString(DbConstants.DateTimeOffsetFormat),
                    v => DateTimeOffset.Parse(v)
               );

        builder.Property(hc => hc.CompletionCount)
               .IsRequired();

        builder.HasIndex(hc => hc.CompletionDate);
    }
}