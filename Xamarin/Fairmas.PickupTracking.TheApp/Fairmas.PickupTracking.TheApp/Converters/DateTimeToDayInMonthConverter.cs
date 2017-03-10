using System;
using System.Globalization;
using Xamarin.Forms;

namespace Fairmas.PickupTracking.TheApp.Converters
{
    class DateTimeToDayInMonthConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.ToString("ddd, dd MMM");
        }

        public Object ConvertBack(Object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
