namespace ParkingFeeCalculatorLab;

public class PriceBook
{
    private readonly TimeSpan THIRTY_MINUTES = TimeSpan.FromMinutes(30);
    private readonly List<DayOfWeek> _weekend = [DayOfWeek.Saturday, DayOfWeek.Sunday];
    private readonly HashSet<DateTime> _nationalHolidays;

    public PriceBook()
    {
        _nationalHolidays = new HashSet<DateTime>
        {
            new DateTime(2025, 1, 1),   // New Year's Day
            // Add other national holidays here
        };
    }

    public long GetDailyFee(DailySession dailySession)
    {
        long todayFee = GetRegularFee(dailySession);
        long dailyLimit = IsHoliday(dailySession.Today)
            ? 2400L
            : 150L;
        return Math.Min(todayFee, dailyLimit);
    }

    private bool IsHoliday(DateTime today)
    {
        return _weekend.Contains(today.DayOfWeek) || _nationalHolidays.Contains(today.Date);
    }
    
    private long GetRegularFee(DailySession dailySession)
    {
        // 以 30 分鐘為單位，無條件進位
        long periods = (long)Math.Ceiling(dailySession.TodayDuration.TotalMinutes / THIRTY_MINUTES.TotalMinutes);
        return periods * (IsHoliday(dailySession.Today)
            ? 50
            : 30);
    }
}