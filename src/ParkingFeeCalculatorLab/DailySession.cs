namespace ParkingFeeCalculatorLab;

public class DailySession
{
    public DateTime Today { get; }
    public TimeSpan TodayDuration { get; }
    
    public DailySession(DateTime today, TimeSpan todayDuration)
    {
        Today = today;
        TodayDuration = todayDuration;
    }
}