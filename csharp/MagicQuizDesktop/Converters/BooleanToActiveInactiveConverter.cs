using System;
using System.Globalization;
using System.Windows.Data;

namespace MagicQuizDesktop.Converters
{
    /// <summary>
    /// A class that implements IValueConverter to convert Boolean values to the equivalent 'Active' or 'Inactive' strings in a specific culture.
    /// ConvertBack operation is not supported.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(string))]
    public class BooleanToActiveInactiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;

            return (bool)value ? "Aktív" : "Inaktív";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}
