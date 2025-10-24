namespace ParkingFeeCalculatorLab;

public class HolidayBook
{
    private readonly TimeSpan THIRTY_MINUTES = TimeSpan.FromMinutes(30);
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

    public long GetDailyFee(DailySession dailySession)
    {
        long todayFee = GetRegularFee(dailySession);
        long dailyLimit = IsHoliday(dailySession.GetToday())
            ? 2400L
            : 150L; // TODO: 根據假日或國定假日，調整上限
        return Math.Min(todayFee, dailyLimit);
    }

    private long GetRegularFee(DailySession dailySession)
    {
        // 以 30 分鐘為單位，無條件進位
        long periods = (long)Math.Ceiling(dailySession.GetTodayDuration().TotalMinutes / THIRTY_MINUTES.TotalMinutes);
        return periods * (IsHoliday(dailySession.GetToday())
            ? 50
            : 30);
    }
}