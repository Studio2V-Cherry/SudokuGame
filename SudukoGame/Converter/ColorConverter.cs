using System.Globalization;

namespace SudokuGame.Converter
{
    /// <summary>
    /// color converter for update
    /// </summary>
    /// <seealso cref="Microsoft.Maui.Controls.IValueConverter" />
    public class ColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return value;
            else if (value == null && parameter != null)
            {
                var colcod = parameter as Color;
                return colcod;
            }
            else
                return value;
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                var colcod = parameter as string;
                return Color.FromArgb(colcod);
            }
            return Colors.Transparent;
        }
    }
}
