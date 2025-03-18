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
               .IsRequired();

        builder.Property(hc => hc.CompletionCount)
               .IsRequired();

        builder.HasIndex(hc => hc.CompletionDate);
    }
}