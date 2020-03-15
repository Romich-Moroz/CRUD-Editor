using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Lab2
{
    [ValueConversion(typeof(Type), typeof(string))]
    class FieldMetadataConverter : IValueConverter
    {
        /// <summary>
        /// Gets metadata from fieldinfo
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FieldNameAttribute attribute = (FieldNameAttribute)((value as Type)?.GetCustomAttribute(typeof(FieldNameAttribute)) ?? (value as FieldInfo)?.GetCustomAttribute(typeof(FieldNameAttribute)));
            return attribute != null ? attribute.FieldName : "Undefined field name";          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
