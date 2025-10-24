namespace ParkingFeeCalculatorLab;

public class HolidayBook
{
    public bool IsHoliday(DateTime today)
    {
        var weekend = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };
        return weekend.Contains(today.DayOfWeek);
    }
}