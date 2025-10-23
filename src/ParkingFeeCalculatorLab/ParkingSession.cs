namespace ParkingFeeCalculatorLab;

public class ParkingSession
{
    private DateTime Start { get; }
    private DateTime End { get; }

    public ParkingSession(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public List<TimeSpan> GetDailyDurations()
    {
        List<TimeSpan> durations = new List<TimeSpan>();
        DateTime todayStart = Start.Date;
        while (todayStart < End)
        {
            DateTime tomorrwStart = todayStart.AddDays(1L);

            DateTime todaySessionStart = Start > todayStart ? Start : todayStart;
            DateTime todaySessionEnd = End < tomorrwStart ? End : tomorrwStart;
            TimeSpan todayDuration = todaySessionEnd - todaySessionStart;
            durations.Add(todayDuration);
                
            todayStart = tomorrwStart;
        }

        return durations;
    }

    public TimeSpan GetTotalDuration()
    {
        return End - Start;
    }
}
