using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Func.CheckHabits;

public class HabitsDbContext : DbContext
{
    public HabitsDbContext(DbContextOptions<HabitsDbContext> options) 
        : base(options)
    {
    }

    public DbSet<ContentLink> ContentLinks { get; set; }
}
