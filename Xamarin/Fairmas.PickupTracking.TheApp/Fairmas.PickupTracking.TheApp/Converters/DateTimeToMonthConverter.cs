using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fairmas.PickupTracking.TheApp.Converters
{
    public class DateTimeToMonthConverter : IValueConverter
    {
        

        public Object Convert(Object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.ToString("MMM yyyy");
        }

        public Object ConvertBack(Object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
