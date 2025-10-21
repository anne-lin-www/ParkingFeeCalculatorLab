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

        public long CalculateFee(DateTime start, DateTime end)
        {
            // 跨天
            //      一個 duration 切多段，一天一段
            // 例假日與國定假日
            //      今天是哪一天
            //      一個 duration 切多段，一天一段

            var duration = end - start;

            if (IsShort(duration))
            {
                return 0L;
            }

            // 判斷是否為同一天進出
            // if (start.Date == end.Date)
            // {
            //     long fee = GetRegularFee(duration);
            //     return Math.Min(fee, 150L);
            // }
            // else // 跨日計費邏輯
            // {
                DateTime todayStart = start.Date;
                long totalFee = 0L;
                while (todayStart < end)
                {
                if (start > todayStart
                    && !(end < todayStart.AddDays(1L))) // 首日停車時間未滿上限 
                {
                    DateTime todaySessionStart = start;
                    DateTime todaySessionEnd = todayStart.AddDays(1L);
                    TimeSpan todayDuration = todaySessionEnd - todaySessionStart;

                    long todayFee = GetRegularFee(todayDuration);
                    totalFee += Math.Min(todayFee, 150L);
                }
                else if (!(start > todayStart)
                    && (end < todayStart.AddDays(1L))) // 最後一日停車時間未滿上限
                {
                    DateTime todaySessionStart = todayStart;
                    DateTime todaySessionEnd = end;
                    TimeSpan todayDuration = todaySessionEnd - todaySessionStart;

                    long todayFee = GetRegularFee(todayDuration);
                    totalFee += Math.Min(todayFee, 150L);
                }
                else if ((start > todayStart)
                        && (end < todayStart.AddDays(1L))) // 同一日
                {
                    DateTime todaySessionStart = start;
                    DateTime todaySessionEnd = end;
                    TimeSpan todayDuration = todaySessionEnd - todaySessionStart;

                    long todayFee = GetRegularFee(todayDuration);
                    totalFee += Math.Min(todayFee, 150L);
                }
                else
                {
                    totalFee += 150L;
                }
                    
                    todayStart = todayStart.AddDays(1L);
                }
                return totalFee;
            // }
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