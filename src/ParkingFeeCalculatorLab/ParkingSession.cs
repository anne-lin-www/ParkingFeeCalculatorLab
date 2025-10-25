namespace ParkingFeeCalculatorLab;

public class ParkingSession
{
    public string Plate { get; }
    private DateTime Start { get; }
    private DateTime? End { get; set; }

    public ParkingSession(string plate, DateTime start, DateTime? end)
    {
        Plate = plate;
        Start = start;
        End = end;
    }

    public List<DailySession> GetDailySessions()
    {
        List<TimeSpan> durations = new List<TimeSpan>();
        List<DailySession> dailySessions = new List<DailySession>(); 
        DateTime today = Start;
        DateTime todayStart = today.Date;
        while (todayStart < End)
        {
            DateTime tomorrwStart = todayStart.AddDays(1L);

            DateTime todaySessionStart = Start > todayStart ? Start : todayStart;
            DateTime todaySessionEnd = End.GetValueOrDefault() < tomorrwStart ? End.GetValueOrDefault() : tomorrwStart;
            TimeSpan todayDuration = todaySessionEnd - todaySessionStart;
            durations.Add(todayDuration);
            dailySessions.Add(new DailySession(today, todayDuration));
                
            todayStart = tomorrwStart;
        }

        return dailySessions;
    }

    public TimeSpan GetTotalDuration()
    {
        return End.GetValueOrDefault() - Start;
    }

    public void SetEnd(DateTime end)
    {
        End = end;
    }

    public static ParkingSession StartParking(string plate, DateTime startTime)
    {
        return new ParkingSession(plate, startTime, null);
    }

    public static void EndParking(ParkingSession? parkingSession, string endText)
    {
        parkingSession.SetEnd(DateTime.Parse(endText));
    }
}
