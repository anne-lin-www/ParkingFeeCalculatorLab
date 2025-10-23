namespace ParkingFeeCalculatorLab;

public class DailySession
{
    private DateTime Today { get; }
    private TimeSpan TodayDuration { get; }
    
    public DailySession(DateTime today, TimeSpan todayDuration)
    {
        Today = today;
        TodayDuration = todayDuration;
    }
    
    public DateTime GetToday()
    {
        return Today;
    }
    
    public TimeSpan GetTodayDuration()
    {
        return TodayDuration;
    }
}