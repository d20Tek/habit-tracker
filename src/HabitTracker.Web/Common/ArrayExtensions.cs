namespace HabitTracker.Web.Common;

public static class ArrayExtensions
{
    public static void ReplaceFirst<T>(this T[] array, Predicate<T> match, T newItem)
    {
        int index = Array.FindIndex(array, match);
        if (index >= 0)
        {
            array[index] = newItem;
        }
    }
}
