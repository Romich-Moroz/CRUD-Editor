using System;
using System.Globalization;
using System.Windows.Data;

namespace Lab2
{
    [ValueConversion(typeof(Component), typeof(string))]
    class ComponentNameConverter : IValueConverter
    {
        /// <summary>
        /// Gets a name field from existing component
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Component)?.GetName();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
