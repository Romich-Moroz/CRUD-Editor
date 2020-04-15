using System;

namespace PluginSupport
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ExtensionAttribute : Attribute
    {        
        public string Extension { get; set; }
    }
}
