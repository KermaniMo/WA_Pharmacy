using System.Globalization;

namespace WA_Pharmacy.AppCode.PublicClass
{
    public class DateFunctions
    {

        public static string DateToShamsi(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();

            int year = pc.GetYear(date);
            int month = pc.GetMonth(date);
            int day = pc.GetDayOfMonth(date);

            return $"{year:0000}/{month:00}/{day:00}";
        }
        public static string DateToShamsi(DateOnly date)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dateTime = date.ToDateTime(TimeOnly.MinValue);

            int year = pc.GetYear(dateTime);
            int month = pc.GetMonth(dateTime);
            int day = pc.GetDayOfMonth(dateTime);

            return $"{year:0000}/{month:00}/{day:00}";
        }

        public static DateOnly? ToMiladi(string shamsiDate)
        {
            // 1. اگر نال بود سریع برگرد
            if (string.IsNullOrWhiteSpace(shamsiDate)) return null;

            try
            {
                shamsiDate = shamsiDate
                   .Replace("۰", "0").Replace("۱", "1").Replace("۲", "2")
                   .Replace("۳", "3").Replace("۴", "4").Replace("۵", "5")
                   .Replace("۶", "6").Replace("۷", "7").Replace("۸", "8")
                   .Replace("۹", "9");

                var parts = shamsiDate.Split('/');

                if (parts.Length != 3) return null;

                int year = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int day = int.Parse(parts[2]);

                var pc = new PersianCalendar();
                var dt = pc.ToDateTime(year, month, day, 0, 0, 0, 0);

                return DateOnly.FromDateTime(dt);
            }
            catch
            {
                return null;
            }
        }


    }
}
