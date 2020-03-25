using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Lab2
{
    public class PropertyDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Template selector for properties which are field type dependent
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FieldInfo fi = (item as ComponentField).fieldInfo;
            FrameworkElement f = container as FrameworkElement;           
            if (fi.FieldType == typeof(bool))
            {

                return f.FindResource("BooleanDataTemplate") as DataTemplate;
            }
            if (fi.FieldType.IsEnum)
            {
                return f.FindResource("EnumerableDataTemplate") as DataTemplate;
            }
            if (fi.FieldType.IsSubclassOf(typeof(Component)))
            {
                return f.FindResource("ComponentSelectionDataTemplate") as DataTemplate;
            }
            if (fi.FieldType == typeof(int))
            {
                return f.FindResource("IntDataTemplate") as DataTemplate;
            }
            if (fi.FieldType == typeof(double))
            {
                return f.FindResource("DoubleDataTemplate") as DataTemplate;
            }
            return f.FindResource("DefaultDataTemplate") as DataTemplate;
        }
    }
}
