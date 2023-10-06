using Microsoft.Maui.Graphics;
using System.Globalization;

namespace SudokuGame.Converter
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return value;
           else  if (value == null&& parameter != null)
            {
                var colcod = parameter as Color;
                return colcod;
            }
            else
                return value;
        }

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
