using HabitTracker.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Persistence;

internal class HabitTrackerDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Habit> Habits { get; set; }

    public DbSet<HabitCompletion> HabitCompletions { get; set; }

    public HabitTrackerDbContext(DbContextOptions<HabitTrackerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Category
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.CategoryId);

            entity.Property(c => c.CategoryId)
                  .ValueGeneratedOnAdd();

            entity.Property(c => c.UserId)
                  .IsRequired()
                  .HasMaxLength(32);

            entity.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(100);
        });

        // Configure Habit
        modelBuilder.Entity<Habit>(entity =>
        {
            entity.HasKey(h => h.HabitId);

            entity.Property(h => h.HabitId)
                  .ValueGeneratedOnAdd();

            entity.Property(h => h.UserId)
                  .IsRequired()
                  .HasMaxLength(32);

            entity.Property(h => h.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(h => h.Description)
                  .HasMaxLength(500)
                  .IsRequired(false);

            entity.Property(h => h.TargetAttempts)
                  .IsRequired();

            entity.HasOne<Category>(h => h.Category)
                  .WithMany()
                  .HasForeignKey(h => h.CategoryId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany<HabitCompletion>(h => h.DailyCompletions)
                  .WithOne()
                  .HasForeignKey("HabitId")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure HabitCompletion
        modelBuilder.Entity<HabitCompletion>(entity =>
        {
            entity.HasKey(hc => hc.Id);

            entity.Property(hc => hc.Id)
                  .ValueGeneratedOnAdd();

            entity.Property(hc => hc.CompletionDate)
                  .IsRequired();

            entity.Property(hc => hc.CompletionCount)
                  .IsRequired();
        });
    }
}
