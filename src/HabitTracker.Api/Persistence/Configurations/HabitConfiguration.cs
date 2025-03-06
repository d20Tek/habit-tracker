using HabitTracker.Api.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Persistence.Configurations;

internal class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.HasKey(h => h.HabitId);

        builder.Property(h => h.HabitId)
               .ValueGeneratedOnAdd();

        builder.Property(h => h.UserId)
               .IsRequired()
               .HasMaxLength(32);

        builder.Property(h => h.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(h => h.Description)
               .HasMaxLength(500)
               .IsRequired(false);

        builder.Property(h => h.TargetAttempts)
               .IsRequired();

        builder.HasOne(h => h.Category)
               .WithMany()
               .HasForeignKey(h => h.CategoryId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(h => h.DailyCompletions)
               .WithOne()
               .HasForeignKey("HabitId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
