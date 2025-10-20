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

        public long CalculateFee(DateTime start, DateTime end)
        {
            var duration = end - start;

            if (duration.TotalMinutes <= 15)
            {
                return 0L;
            }

            var thirtyMinutes = TimeSpan.FromMinutes(30);

            // 以 30 分鐘為單位，無條件進位
            long periods = (long)Math.Ceiling(duration.TotalMinutes / thirtyMinutes.TotalMinutes);
            long fee = periods * 30;

            return Math.Min(fee, 150L);
    
            // long minsBetween = (long)(end - start).TotalMinutes;

            // if (minsBetween <= 15)
            // {
            //     return 0L;
            // }

            // long regularFee = GetRegularFee(minsBetween);
        }

        private static long GetRegularFee(long minsBetween)
        {
            long periods = minsBetween / 30;

            return (periods + 1) * 30;
        }
    }
}