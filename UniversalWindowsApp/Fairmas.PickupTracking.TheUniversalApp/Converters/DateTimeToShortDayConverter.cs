using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Fairmas.PickupTracking.TheUniversalApp.Converters
{
    public sealed class DateTimeToShortDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Example: 2017-02-12 => Sun 12. Feb

            var dateTime = (DateTime)value;
            var monthName = DateTimeFormatInfo.CurrentInfo.MonthNames[dateTime.Month - 1];
            var dayName = dateTime.ToString("ddd", CultureInfo.CurrentUICulture);
            var shortDayValue = $"{dayName.Substring(0, 3)} {dateTime.ToString("dd", CultureInfo.CurrentUICulture)}. {monthName.Substring(0, 3)}";

            return shortDayValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
