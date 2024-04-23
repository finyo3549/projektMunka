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
        /// <summary>
        /// Converts a boolean value to a string representation based on its truthiness. 
        /// Returns "Aktív" for true and "Inaktív" for false.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A string representation of the boolean state ("Aktív" or "Inaktív") if value is a boolean; otherwise, null.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;

            return (bool)value ? "Aktív" : "Inaktív";
        }

        /// <summary>
        /// Converts a value back to its original type.
        /// </summary>
        /// <param name="value">The object that is to be converted back.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter used in the conversion.</param>
        /// <param name="culture">The culture to use in the conversion.</param>
        /// <returns>A object that represents the converted back value.</returns>
        /// <exception cref="NotSupportedException">Always thrown because this method is not supported.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}
