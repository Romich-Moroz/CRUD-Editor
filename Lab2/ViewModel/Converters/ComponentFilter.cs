using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Lab2
{
    [ValueConversion(typeof(Component), typeof(string))]
    class ComponentFilter : IMultiValueConverter
    {
        /// <summary>
        /// Select all components in collection value[0] of type value[1] and sets visibility of value[2] container
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            DependencyObject item = VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(value[2] as TextBlock)));
            if ((value[0] as Component)?.GetType() == value[1] as Type)
            {
                if (item is ComboBoxItem)
                {
                    (item as ComboBoxItem).Visibility = Visibility.Visible;
                }
                return (value[0] as Component)?.GetName();
                
            }
            if (item is ComboBoxItem)
            {
                (item as ComboBoxItem).Visibility = Visibility.Collapsed;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
