namespace ParkingFeeCalculatorLab;

public class HolidayBook
{
    public bool IsHoliday(DateTime today)
    {
        var nationalHolidays = new HashSet<DateTime>
        {
            new DateTime(today.Year, 1, 1),   // New Year's Day
            new DateTime(today.Year, 12, 25), // Christmas
            // Add other national holidays here
        };
        var weekend = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };
        return nationalHolidays.Contains(today) || weekend.Contains(today.DayOfWeek);
    }
}