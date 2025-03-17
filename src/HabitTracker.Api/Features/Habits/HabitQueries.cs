using System.Linq.Expressions;

namespace HabitTracker.Api.Features.Habits;

internal static class HabitQueries
{
    public static IQueryable<HabitResponse> QueryHabitsForUser(this DbSet<Habit> habits, string userId, int takeCompletions = 1) =>
        habits.Where(h => h.UserId == userId)
              .Include(h => h.Category)
              .Select(HabitQueries.ProjectHabitCompletions(takeCompletions))
              .AsNoTracking();

    public static IQueryable<HabitResponse> QueryHabitById(this DbSet<Habit> habits, int habitId, string userId, int takeCompletions = 1) =>
        habits.Include(h => h.Category)
              .Where(x => x.HabitId == habitId && x.UserId == userId)
              .Select(HabitQueries.ProjectHabitCompletions(takeCompletions))
              .AsNoTracking();

    public static Expression<Func<Habit, HabitResponse>> ProjectHabitCompletions(int takeCompletions) =>
        h => HabitResponse.FromEntity(h, h.DailyCompletions.OrderByDescending(dc => dc.CompletionDate)
                                                           .Take(takeCompletions));
}
