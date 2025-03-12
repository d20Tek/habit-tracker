using HabitTracker.Api.Persistence.Configurations;

namespace HabitTracker.Api.Persistence;

internal class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Habit> Habits { get; set; }

    public DbSet<HabitCompletion> HabitCompletions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfiguration(new CategoryConfiguration())
                    .ApplyConfiguration(new HabitConfiguration())
                    .ApplyConfiguration(new HabitCompletionConfiguration());
}
