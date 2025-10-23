namespace ParkingFeeCalculatorLab;

public class ParkingSession
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }

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
