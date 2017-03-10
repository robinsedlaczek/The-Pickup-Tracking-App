using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Fairmas.PickupTracking.TheUniversalApp.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, String language)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, String language)
        {
            throw new NotImplementedException();
        }
    }
}
