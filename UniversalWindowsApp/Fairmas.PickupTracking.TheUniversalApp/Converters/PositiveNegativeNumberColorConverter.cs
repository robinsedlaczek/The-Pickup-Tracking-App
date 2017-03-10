using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Fairmas.PickupTracking.TheUniversalApp.Converters
{
    public class PositiveNegativeNumberColorConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, String language)
        {
            var lessThanZero = false;

            if (value is double)
                lessThanZero = (double)value < 0;
            else if (value is decimal)
                lessThanZero = (decimal)value < 0;

            return lessThanZero ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.LightGreen);
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, String language)
        {
            throw new NotImplementedException();
        }
    }
}
