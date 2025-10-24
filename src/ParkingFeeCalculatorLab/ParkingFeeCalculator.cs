namespace ParkingFeeCalculatorLab;

public class ParkingFeeCalculator
{
    private readonly HolidayBook holidayBook;
    private readonly TimeSpan FIFTEEN_MINUTES = TimeSpan.FromMinutes(15);
    // Topic: 如何製作一個「能上新聞的」停車費計算機
    // 停車場
    // 15 分鐘內免費
    // 平日
    //      每小時 60 元（以半小時計）
    //      當日上限 150 元（隔日另計）
    // 假日與國定假日
    //      每小時 100 元（以半小時計）
    //      當日上限 2400 元（隔日另計）
    //
    // [0, 15] mins → 0 => Special Case
    // (15, 30] mins → 30
    // (30, 60] mins → 60
    // (60, 90] mins → 90
    // (90, 120] mins → 120
    // (120, 150] 分鐘 → 150

    public ParkingFeeCalculator()
    {
        holidayBook = new HolidayBook();
    }

    public long CalculateFee(ParkingSession parkingSession)
    {
        var duration = parkingSession.GetTotalDuration();

        if (IsShort(duration))
        {
            return 0L;
        }
            
        // Data Clumps + Primitive Obsession
        // Lack of domain knowledge
        // each iteration in the loop:
        //     calculate daily duration             => parking behavior
        //     calculate fee with daily duration    => charging behavior

        // 透過加上「假日計費」的邏輯，來把「算帳的領域物件」「逼」出來。
            
        var dailySessions = parkingSession.GetDailySessions();
            
        long totalFee = 0L;
        foreach (var dailySession in dailySessions)
        {
            var daily = holidayBook.GetDailyFee(dailySession);
            totalFee += daily;
        }
            
        return totalFee;
    }

    private bool IsShort(TimeSpan duration)
    {
        return duration.TotalMinutes <= FIFTEEN_MINUTES.TotalMinutes;
    }
}