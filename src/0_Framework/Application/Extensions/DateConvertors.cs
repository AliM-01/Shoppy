using System.Globalization;

namespace _0_Framework.Application.Extensions;

public static class DateConvertors
{
    #region ToShamsi

    public static string ToShamsi(this DateTime value)
    {
        var pc = new PersianCalendar();
        return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
               pc.GetDayOfMonth(value).ToString("00");
    }

    #endregion

    #region ToDetailedShamsi

    public static string ToDetailedShamsi(this DateTime value)
    {
        var pc = new PersianCalendar();
        return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
               pc.GetDayOfMonth(value).ToString("00") + " " +
               pc.GetHour(value) + ":" + pc.GetMinute(value) + ":" + pc.GetSecond(value);
    }

    #endregion

    #region ToMiladi

    public static DateTime ToMiladi(this string persianDate)
    {
        persianDate = persianDate.ToEnglishNumber();
        var year = Convert.ToInt32(persianDate.Substring(0, 4));
        var month = Convert.ToInt32(persianDate.Substring(5, 2));
        var day = Convert.ToInt32(persianDate.Substring(8, 2));
        var hour = Convert.ToInt32(persianDate.Substring(11, 2));
        var minute = Convert.ToInt32(persianDate.Substring(14, 2));
        var seconds = Convert.ToInt32(persianDate.Substring(17, 2));

        var miladyDateTime = new DateTime(year, month, day, hour, minute, seconds, new PersianCalendar());

        return miladyDateTime;
    }

    #endregion

    #region ToFileName

    public static string ToFileName(this DateTime value)
    {
        return $"{value.Year:0000}_{value.Month:00}_{value.Day:00}_{value.Hour:00}-{value.Minute:00}_{value.Second:00}_"
            + Guid.NewGuid().ToString("N").Substring(0, 4);
    }

    #endregion

    #region Private Tools

    private static readonly string[] Pn = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
    private static readonly string[] En = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    private static string ToEnglishNumber(this string stringNum)
    {
        var cash = stringNum;
        for (var i = 0; i < 10; i++)
            cash = cash.Replace(Pn[i], En[i]);

        return cash;
    }
    private static string ToFarsiNumber(this int intNum)
    {
        var cash = intNum.ToString();
        for (var i = 0; i < 10; i++)
            cash = cash.Replace(En[i], Pn[i]);

        return cash;
    }

    #endregion
}
