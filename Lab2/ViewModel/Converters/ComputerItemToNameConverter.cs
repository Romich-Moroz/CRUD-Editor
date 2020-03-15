using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Lab2
{
    class ComputerItemToNameConverter : IValueConverter
    {
        /// <summary>
        /// Converts computer item or computer field to text that should be displayed
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ComputerItem)
            {
                return "PC: " + (value as ComputerItem).Pc.GetName();
            }
            var tmp = value as ComponentField;
            FieldNameAttribute attribute = (FieldNameAttribute)tmp.fieldInfo?.GetCustomAttribute(typeof(FieldNameAttribute));
            string FieldName;
            if (attribute != null)
            {              
                FieldName = attribute.FieldName+": ";
                if (tmp.fieldValue == null)
                {
                    return FieldName + "Unassigned";
                }
            }
            else
            {
                FieldName = "Undefined field name: ";
            }    
            return tmp.fieldValue?.GetType().BaseType == typeof(Component) ? FieldName + (tmp.fieldValue as Component)?.GetName() : FieldName + tmp.fieldValue?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
