using System;

public class TimeHelper
{
    public static double TimeOffset { get; set; }

    public static double GetTotalMilliseconds
    {
        get
        {
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            return (System.DateTime.UtcNow - epochStart).TotalMilliseconds + TimeOffset;
        }
    }

    public static void SetTimeOffest(long serverTime)
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        double total = (System.DateTime.UtcNow - epochStart).TotalMilliseconds;
        TimeOffset = serverTime - total;
        TimeOffset = 0;
    }

    public static double GetTimeStamp()
    {
        DateTime value = DateTime.Now;
        TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        double ts = span.TotalMilliseconds + TimeOffset;

        return ts;
    }

    public static double GetRemainingTimeInSeconds(double endTime)
    {
        return (endTime - GetTotalMilliseconds) / 1000;
    }

    public static string FormatSecondsToHours(int sec)
    {
        string hours = FormatTimeToString(sec / 3600);
        string minutes = FormatTimeToString(sec / 60 % 60);
        string seconds = FormatTimeToString(sec % 60);
        return String.Format("{0}:{1}:{2}", hours, minutes, seconds);
    }
    public static string FormatSecondsToMinutes(int sec)
    {
        string minutes = FormatTimeToString(sec / 60);
        string seconds = FormatTimeToString(sec % 60);
        return String.Format("{0}:{1}", minutes, seconds);
    }

    private static string FormatTimeToString(int time)
    {
        if (time < 10)
        {
            return String.Format("0{0}", time);
        }
        return time.ToString();
    }
}
