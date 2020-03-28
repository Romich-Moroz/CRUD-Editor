using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace Lab2
{
    static class Model
    {
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

        static public void SaveCollection(ObservableCollection<Component> collection)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = string.Join("|", serializers.Keys);
            fd.InitialDirectory = Directory.GetCurrentDirectory();
            if (fd.ShowDialog() == true)
            {
                string[] filters = fd.Filter.Split('|');
                string selectedFilter = filters[(fd.FilterIndex - 1) * 2] + "|" + filters[(fd.FilterIndex - 1) * 2 + 1];
                ISerializer s = Activator.CreateInstance(serializers[selectedFilter]) as ISerializer;
                s.Serialize(fd.OpenFile(), collection);
            }
        }

        static public ObservableCollection<Component> OpenCollection()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = string.Join("|", serializers.Keys);
            fd.InitialDirectory = Directory.GetCurrentDirectory();
            if (fd.ShowDialog() == true)
            {
                string[] filters = fd.Filter.Split('|');
                string selectedFilter = filters[(fd.FilterIndex - 1) * 2] + "|" + filters[(fd.FilterIndex - 1) * 2 + 1];
                ISerializer s = Activator.CreateInstance(serializers[selectedFilter]) as ISerializer;
                return s.Deserialize<ObservableCollection<Component>>(fd.OpenFile());
            }
            return null;
        }

    }
}
