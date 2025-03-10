using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class DateColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isCurrentMonth)
            {
                return isCurrentMonth ? Color.FromArgb("#838281") : Color.FromArgb("#B5B4B3");
            }
            return Color.FromArgb("#B5B4B3");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isCurrentMonth)
            {
                return isCurrentMonth ? Color.FromArgb("#B5B4B3") : Color.FromArgb("#838281");
            }
            return Color.FromArgb("#838281");
        }
    }
}
