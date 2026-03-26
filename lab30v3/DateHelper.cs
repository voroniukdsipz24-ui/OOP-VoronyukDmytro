namespace lab30v3;

public class DateHelper
{
    public bool IsWeekend(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday ||
               date.DayOfWeek == DayOfWeek.Sunday;
    }

    public int DaysBetween(DateTime start, DateTime end)
    {
        return Math.Abs((end - start).Days);
    }

    public DateTime AddBusinessDays(DateTime date, int days)
    {
        DateTime result = date;
        int added = 0;

        while (added < days)
        {
            result = result.AddDays(1);

            if (!IsWeekend(result))
                added++;
        }

        return result;
    }
}