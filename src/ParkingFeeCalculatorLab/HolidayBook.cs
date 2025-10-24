namespace ParkingFeeCalculatorLab;

public class HolidayBook
{
    private readonly List<DayOfWeek> _weekend = [DayOfWeek.Saturday, DayOfWeek.Sunday];
    private readonly HashSet<DateTime> _nationalHolidays;

    public HolidayBook()
    {
        _nationalHolidays = new HashSet<DateTime>
        {
            new DateTime(2025, 1, 1),   // New Year's Day
            // Add other national holidays here
        };
    }
    
    public bool IsHoliday(DateTime today)
    {
        return _weekend.Contains(today.DayOfWeek) || _nationalHolidays.Contains(today.Date);
    }
}