namespace ParkingFeeCalculatorLab;

public class CalculateParkingFeeService
{
    private readonly TimeSpan FIFTEEN_MINUTES = TimeSpan.FromMinutes(15);
    private readonly IPriceBookRepository _priceBookRepository;

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

    // 物件生成與計算邏輯的耦合
    // ex: new PriceBook();
    // solution: Repository Pattern
    
    public CalculateParkingFeeService(IPriceBookRepository bookRepository)
    {
        _priceBookRepository = bookRepository;
    }

    public long CalculateFee(ParkingSession parkingSession)
    {
        PriceBook priceBook = _priceBookRepository.GetPriceBook();
        var duration = parkingSession.GetTotalDuration();

        if (IsShort(duration))
        {
            return 0L;
        }
        
        var dailySessions = parkingSession.GetDailySessions();
        
        return dailySessions.Sum(dailySession => priceBook.GetDailyFee(dailySession));
    }

    private bool IsShort(TimeSpan duration)
    {
        return duration.TotalMinutes <= FIFTEEN_MINUTES.TotalMinutes;
    }
}