using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using PluginSupport;
using System.Text;

namespace Lab2
{
    static class Model
    {

        static private Dictionary<string, Type> plugins = GetPluginsDictionary();

        /// <summary>
        /// Contains all available serializers for usage
        /// </summary>
        static private Dictionary<string, Type> serializers = GetSerializerDictionary();

        /// <summary>
        /// Returns a collection of fieldInfos about each class that should be manually created in order to create class of final type
        /// </summary>
        /// <param name="finalType"> Defines what type you want to create</param>
        /// <param name="componentBaseType"> Defines the base class which components of final type inherit from</param>
        /// <returns></returns>
        static public ObservableCollection<Type> GetCreatableTypesCollection(Type finalType, Type componentBaseType)
        {
            ObservableCollection<Type> result = new ObservableCollection<Type>(finalType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(fInfo => fInfo.FieldType.BaseType == componentBaseType).Select(f => f.FieldType));
            result.Add(finalType);
            return result;
        }

        /// <summary>
        /// Returns a collection of fields of one type specified by its name from collection of all possible component types
        /// </summary>
        /// <param name="componentsCollection"> Where to search collection</param>
        /// <param name="componentTypeName"> Type name for searching in collection</param>
        /// <returns></returns>
        static public ObservableCollection<FieldInfo> GetAllFieldsOfComponentByTypeName(ObservableCollection<Type> componentsCollection, string componentTypeName)
        {
            return new ObservableCollection<FieldInfo>(componentsCollection.Single(type => type.Name == componentTypeName).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
        } 

        /// <summary>
        /// Creates component with specified field values
        /// </summary>
        /// <param name="t"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        static public Component CreateComponent (Type t, object[] param)
        {
            Component c = (Component)Activator.CreateInstance(t);
            c.Update(param);
            return c;
        }

        /// <summary>
        /// Gets dictionary with all available serializers
        /// </summary>
        /// <returns></returns>
        static public Dictionary<string,Type> GetSerializerDictionary() 
        {
            Dictionary<string, Type> result = new Dictionary<string, Type>();
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ISerializer))))
            {
                SerializationAttribute attribute = t.GetCustomAttribute(typeof(SerializationAttribute)) as SerializationAttribute;
                if (attribute == null)
                {
                    throw new ArgumentNullException("Serialization attribute is null");
                }
                result.Add(attribute.TypeDescription, t);
            }
            return result;
        }

        /// <summary>
        /// Saves collection into file
        /// </summary>
        /// <param name="collection"></param>
        static public void SaveCollection(ObservableCollection<Component> collection)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = GetDialogFormats();
            fd.InitialDirectory = Directory.GetCurrentDirectory();
            if (fd.ShowDialog() == true)
            {
                string[] filters = fd.Filter.Split('|');
                int serializerIndex = (fd.FilterIndex - 1) % serializers.Count;
                Type t = serializers.ElementAt(serializerIndex).Value;
                ISerializer s = Activator.CreateInstance(t) as ISerializer;
                IPlugin plugin = null;
                if (fd.FilterIndex > serializers.Count)
                {
                    int pluginIndex = 0;
                    for (int i = serializers.Count; i < filters.Length / 2; i += serializers.Count, pluginIndex++) 
                    {
                        if (fd.FilterIndex > i)
                        {
                            break;
                        }
                    }
                    Type tPlugin = plugins.ElementAt(pluginIndex).Value;
                    plugin = Activator.CreateInstance(tPlugin) as IPlugin;
                }
                using (var ms = new MemoryStream())
                {
                    s.Serialize(ms, collection);
                    byte[] buf = ms.ToArray();
                    if (plugin != null)
                    {
                        buf = Encoding.Default.GetBytes(plugin.Encode(Encoding.Default.GetString(buf)));
                    }
                    File.WriteAllBytes(fd.FileName, buf);
                }
            }
        }

        /// <summary>
        /// Loads collection from file
        /// </summary>
        /// <returns></returns>
        static public ObservableCollection<Component> OpenCollection()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = GetDialogFormats();
            fd.InitialDirectory = Directory.GetCurrentDirectory();
            if (fd.ShowDialog() == true)
            {
                string[] filters = fd.Filter.Split('|');

                int serializerIndex = (fd.FilterIndex - 1) % serializers.Count;
                Type t = serializers.ElementAt(serializerIndex).Value;
                ISerializer s = Activator.CreateInstance(t) as ISerializer;
                IPlugin plugin = null;
                if (fd.FilterIndex > serializers.Count)
                {
                    int pluginIndex = 0;
                    for (int i = serializers.Count; i < filters.Length / 2; i += serializers.Count, pluginIndex++)
                    {
                        if (fd.FilterIndex > i)
                        {
                            break;
                        }
                    }
                    Type tPlugin = plugins.ElementAt(pluginIndex).Value;
                    plugin = Activator.CreateInstance(tPlugin) as IPlugin;
                }
                byte[] buf = File.ReadAllBytes(fd.FileName);
                if (plugin != null)
                {
                    buf = Encoding.Default.GetBytes(plugin.Decode(Encoding.Default.GetString(buf)));                    
                }
                using (MemoryStream ms = new MemoryStream(buf))
                {
                    return s.Deserialize<ObservableCollection<Component>>(ms);
                }                
            }
            return null;
        }

        /// <summary>
        /// Gets all plugins into dictionary
        /// </summary>
        /// <returns></returns>
        static public Dictionary<string, Type> GetPluginsDictionary()
        {
            Dictionary<string, Type> result = new Dictionary<string, Type>();
            string pluginsPath = Directory.GetCurrentDirectory() + "\\Plugins\\";
            if (!Directory.Exists(pluginsPath))
            {
                Directory.CreateDirectory(pluginsPath);
            }
            foreach (string str in Directory.GetFiles(pluginsPath))
            {
                try
                {
                    Assembly asm = Assembly.LoadFrom(str);
                    Type plugin = asm.GetTypes().Single(t => t.GetInterfaces().Contains(typeof(IPlugin)));
                    result.Add(plugin.Name, plugin);
                }
                catch
                {

                }
            }
            return result;   
        }

        /// <summary>
        /// Gets all plugin names
        /// </summary>
        /// <returns></returns>
        static public ObservableCollection<string> GetPluginNames()
        {
            return new ObservableCollection<string>(plugins.Keys);
        }

        /// <summary>
        /// Creates a new string for filter from base serializer string and applied plugin
        /// </summary>
        /// <param name="serializerString"></param>
        /// <param name="pluginName"></param>
        /// <param name="pluginExt"></param>
        /// <returns></returns>
        static private string CombinePluginAndSerializer(string serializerString, string pluginName, string pluginExt)
        {
            //Binary serialization file(*.bin)|*.bin
            serializerString = string.Format("({0}) {1}", pluginName, serializerString);
            for (int i = 1; i < serializerString.Length; i++)
            {
                if (serializerString[i-1] == '*' && serializerString[i] == '.')
                {
                    serializerString = serializerString.Insert(i+1, pluginExt);
                }
            }
            return serializerString;
        }

        /// <summary>
        /// Gets all file formats for open/save dialog
        /// </summary>
        /// <returns></returns>
        static private string GetDialogFormats()
        {
            string result = string.Join("|", serializers.Keys);
            foreach (string pluginName in plugins.Keys)
            {
                foreach (string serializerName in serializers.Keys)
                {
                    ExtensionAttribute attribute = (ExtensionAttribute)plugins[pluginName].GetCustomAttribute(typeof(ExtensionAttribute));
                    if (attribute == null)
                    {
                        throw new ArgumentNullException("Extension attribute for plugin " + pluginName + " is not assigned");
                    }
                    result += "|" + CombinePluginAndSerializer(serializerName, pluginName, attribute.Extension);
                }
            }
            return result;
        }
    }
}
