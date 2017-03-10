using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Fairmas.PickupTracking.TheUniversalApp.Converters
{
    public sealed class DateTimeToShortMonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Example: 2017-02-12 => Feb 17

            var dateTime = (DateTime)value;
            var monthName = DateTimeFormatInfo.CurrentInfo.MonthNames[dateTime.Month - 1];
            var shortMonthValue = $"{monthName.Substring(0, 3)} {dateTime.ToString("yy", CultureInfo.CurrentUICulture)}";

            return shortMonthValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
