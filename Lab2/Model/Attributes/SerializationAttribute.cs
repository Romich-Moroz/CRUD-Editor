using System;

namespace Lab2
{
    [AttributeUsage(AttributeTargets.Class)]
    class SerializationAttribute : Attribute
    {
        public string TypeDescription { get; set; }
    }
}
