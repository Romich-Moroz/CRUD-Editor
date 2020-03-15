using System;
using System.ComponentModel;
using System.Reflection;

namespace Lab2
{
    class ComponentField : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        /// <summary>
        /// Field of the parent
        /// </summary>
        public FieldInfo fieldInfo { get; set; }

        /// <summary>
        /// Parent type
        /// </summary>
        public object fieldValue { get; set; }

        /// <summary>
        /// Default costructor this class
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="fieldValue"></param>
        public ComponentField(FieldInfo fieldInfo, object fieldValue)
        {
            this.fieldInfo = fieldInfo;
            this.fieldValue = fieldValue;
        }

        
    }
}
