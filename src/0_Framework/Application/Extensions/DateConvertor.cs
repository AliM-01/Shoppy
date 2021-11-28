using System.Globalization;

namespace _0_Framework.Application.Extensions
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            var pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }

        public static string ToDetailedShamsi(this DateTime value)
        {
            var pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00") + " " +
                   pc.GetHour(value) + " : " + pc.GetMinute(value);
        }
    }
}