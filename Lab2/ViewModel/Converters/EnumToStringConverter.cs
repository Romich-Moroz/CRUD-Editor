using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Lab2
{
    [ValueConversion(typeof(ComponentField),typeof(List<string>))]
    class EnumToStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts enum type to string list
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new List<string>(Enum.GetNames(((value as ComponentField).fieldInfo).FieldType));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
