using System;

namespace Lab2
{
    /// <summary>
    /// Attribute that is used to recieve easy to read description of anything
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property|AttributeTargets.Class)]
    class FieldNameAttribute:Attribute
    {
        /// <summary>
        /// Defines metadata for humans to read
        /// </summary>
        public string FieldName { get; set; }
    }
}
