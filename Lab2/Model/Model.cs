using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Lab2
{
    static class Model
    {
        /// <summary>
        /// Returns a collection of fieldInfos about each class that should be manually created in order to create class of final type
        /// </summary>
        /// <param name="finalType"> Defines what type you want to create</param>
        /// <param name="componentBaseType"> Defines the base class which components of final type inherit from</param>
        /// <returns></returns>
        static public ObservableCollection<Type> GetCreatableTypesCollection(Type finalType, Type componentBaseType)
        {
            ObservableCollection<Type> result = new ObservableCollection<Type>(finalType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(fInfo => fInfo.FieldType.BaseType == componentBaseType).Select(f => f.FieldType));
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
            return new ObservableCollection<FieldInfo>(componentsCollection.Single(type => type.Name == componentTypeName).GetFields(BindingFlags.Instance | BindingFlags.NonPublic));
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


    }
}
