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

    public static bool ReplaceFirstOrAdd<T>(this List<T> list, Predicate<T> match, T newItem)
    {
        int index = list.FindIndex(match);
        if (index >= 0)
        {
            list[index] = newItem;
            return false;
        }
        else
        {
            list.Add(newItem);
            return true;
        }
    }
}
