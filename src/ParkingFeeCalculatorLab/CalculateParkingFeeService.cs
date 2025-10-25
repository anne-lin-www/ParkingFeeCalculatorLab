namespace ParkingFeeCalculatorLab;

public class CalculateParkingFeeService
{
    private readonly TimeSpan FIFTEEN_MINUTES = TimeSpan.FromMinutes(15);
    private readonly IPriceBookRepository _priceBookRepository;
    private readonly IParkingSessionRepository _parkingSessionRepository;

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

    // Repository 的職責、物件的生命週期
    //   ex: calculate(ParkingSession parkingSession)
    //   solution: Repository + Factory
    //   Tips:
    //      先 hard code 刻出形狀，再讓 IDE 幫你填上值 (Green)
    //      用新測項來抽進真實邏輯 (Red)
    //      改寫成真實邏輯 (Green)
    //      如有需要，重構 (Refactor)
    // 以將行為委託給 Entity，以取代「資料操作」
    
    public CalculateParkingFeeService(IPriceBookRepository bookRepository, IParkingSessionRepository parkingSessionRepository)
    {
        _priceBookRepository = bookRepository;
        _parkingSessionRepository = parkingSessionRepository;
    }

    public long CalculateFee(string plate)
    {
        ParkingSession? parkingSession = _parkingSessionRepository.Find(plate);
        if (parkingSession is null)
        {
            return 0L;
        }
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