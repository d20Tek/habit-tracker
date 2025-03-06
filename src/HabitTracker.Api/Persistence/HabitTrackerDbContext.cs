using HabitTracker.Api.Domain;
using HabitTracker.Api.Persistence.Configurations;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfiguration(new CategoryConfiguration())
                    .ApplyConfiguration(new HabitConfiguration())
                    .ApplyConfiguration(new HabitCompletionConfiguration());
}
