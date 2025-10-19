

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
        public long CalculateFee(DateTime start, DateTime end)
        {
            long minsBetween = (long)(end - start).TotalMinutes;

            if (minsBetween < 15)
            {
                return 0L;
            }

            if(minsBetween < 30)
            {
                return 30L;
            }

            return 60L;
        }
    }
}