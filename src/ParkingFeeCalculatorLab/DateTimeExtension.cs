namespace ParkingFeeCalculatorLab;

public static class DateTimeExtension
{
    // 傳入 local DateTime 轉換成 epoch 毫秒
    public static long ToTimestamp(this DateTime dateTime)
    {
        var elapsedTime = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (long)elapsedTime.TotalMilliseconds;
    }

    // 儲存的 epoch 毫秒轉回 local DateTime
    public static DateTime FromTimestamp(this long epochMilli)
    {
        var utcDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(epochMilli);
        return utcDateTime.ToLocalTime();
    }
}