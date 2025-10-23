namespace ParkingFeeCalculatorLab
{
    public class ParkingFeeCalculator
    {
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

        private readonly TimeSpan THIRTY_MINUTES = TimeSpan.FromMinutes(30);
        private readonly TimeSpan FIFTEEN_MINUTES = TimeSpan.FromMinutes(15);

        public long CalculateFee(ParkingSession parkingSession)
        {
            // 跨天
            //      一個 duration 切多段，一天一段
            // 例假日與國定假日
            //      今天是哪一天
            //      一個 duration 切多段，一天一段

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

            // n -> 2n => O(n)
            
            var durations = parkingSession.GetDailyDurations();
            
            long totalFee = 0L;
            foreach (var dailyDuration in durations)
            {
                long todayFee = GetRegularFee(dailyDuration);
                totalFee += Math.Min(todayFee, 150L);
            }
            
            return totalFee;
        }

        private long GetRegularFee(TimeSpan duration)
        {
            // 以 30 分鐘為單位，無條件進位
            long periods = (long)Math.Ceiling(duration.TotalMinutes / THIRTY_MINUTES.TotalMinutes);
            long fee = periods * 30;
            return fee;
        }

        private bool IsShort(TimeSpan duration)
        {
            return duration.TotalMinutes <= FIFTEEN_MINUTES.TotalMinutes;
        }
    }
}