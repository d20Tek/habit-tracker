using System.Linq.Expressions;

namespace HabitTracker.Api.Features.Habits;

internal static class HabitQueries
{
    public static IQueryable<HabitResponse> QueryHabitsForUser(
        this DbSet<Habit> habits,
        string userId,
        int limitCompletions = 1) =>
        habits.Where(h => h.UserId == userId)
              .Include(h => h.Category)
              .Select(HabitQueries.ProjectHabitCompletions(limitCompletions))
              .AsNoTracking();

    public static IQueryable<HabitResponse> QueryHabitById(
        this DbSet<Habit> habits,
        int habitId,
        string userId,
        int limitCompletions = 1) =>
        habits.Include(h => h.Category)
              .Where(x => x.HabitId == habitId && x.UserId == userId)
              .Select(HabitQueries.ProjectHabitCompletions(limitCompletions))
              .AsNoTracking();

    public static Expression<Func<Habit, HabitResponse>> ProjectHabitCompletions(int limitCompletions) =>
        h => HabitResponse.FromEntity(h, h.DailyCompletions.OrderByDescending(dc => dc.CompletionDate)
                                                           .Take(limitCompletions));
}
