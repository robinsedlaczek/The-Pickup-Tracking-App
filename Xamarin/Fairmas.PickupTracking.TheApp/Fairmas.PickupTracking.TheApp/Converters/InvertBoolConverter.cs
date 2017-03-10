using System;
using System.Globalization;
using Xamarin.Forms;

namespace Fairmas.PickupTracking.TheApp.Converters
{
    public class InvertBoolConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public Object ConvertBack(Object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    
}
