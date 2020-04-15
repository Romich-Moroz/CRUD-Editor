using PluginSupport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lab2
{
    /* 
    /// Serialization format
    /// Class:ClassName;
    ///     FieldName:FieldValue;
    ///     FieldName:FieldValue;
    /// EndClass;
    /// 
    /// Class:ClassName;
    ///     Class:ClassName;
    ///         FieldName:FieldValue;
    ///     EndClass;
    ///     FieldName:FieldValue;
    /// EndClass;
    /// 
    */
    /// <summary>
    /// Provides serialization in custom format
    /// </summary>
    [Serialization(TypeDescription = "Custom serialization file (*.csf)|*.csf")]
    class CustomSerializer : ISerializer
    {
        /// <summary>
        /// Contains amount of tabs in current line
        /// </summary>
        private int tabCount;

        /// <summary>
        /// Stuct for storing line parsing results
        /// </summary>
        private struct ParseResult
        {
            /// <summary>
            /// Specifies what type of line was read
            /// </summary>
            public enum ParseResultEnum { Container, ContainerEnd, Class, ComplexField, ClassEnd, Field };

            /// <summary>
            /// Specifies what type of line was read
            /// </summary>
            public ParseResultEnum lastParseResult;

            /// <summary>
            /// Contains typeName for parser to create
            /// </summary>
            public string typeName;

            /// <summary>
            /// Contains value to assing to type
            /// </summary>
            public string value;

            public ParseResult(ParseResultEnum r, string type, string value)
            {
                this.lastParseResult = r;
                this.typeName = type;
                this.value = value;
            }
        }
        /// <summary>
        /// Contains info about last parsing operation
        /// </summary>
        private ParseResult lastParse;

        /// <summary>
        /// Contains all line from serialized file
        /// </summary>
        private List<string> textEntries;
        
        
        /// <summary>
        /// Deserializes file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public T Deserialize<T>(Stream s)
        {

            using (StreamReader sr = new StreamReader(s))
            {
                textEntries = new List<string>(sr.ReadToEnd().Split('\n'));
                return Parse<T>();
            }
        }

        /// <summary>
        /// Parses text entries and creates required objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T Parse<T>()
        {
            lastParse = ParseLine(textEntries[0]);
            int tabs = GetTabsCount(textEntries[0]);
            textEntries.RemoveAt(0);
            if (lastParse.lastParseResult == ParseResult.ParseResultEnum.Container)
            {
                dynamic container = CreateInstance<T>(lastParse.typeName);                
                while(GetTabsCount(textEntries[0]) != tabs)
                {
                    dynamic o = GetGenericMethod("Parse", typeof(T).GetGenericArguments()[0]).Invoke(this, null);
                    container.Add(o);
                }
                textEntries.RemoveAt(0);
                return container;
            }
            if (lastParse.lastParseResult == ParseResult.ParseResultEnum.Class)
            {
                T obj = CreateInstance<T>(lastParse.typeName);
                FieldInfo[] fInfo = GetFieldInfos(obj);
                while (GetTabsCount(textEntries[0]) != tabs)
                {
                    object fieldValue = Parse<object>();
                    if (IsTypePrimitive(fieldValue.GetType()))
                    {
                        FieldInfo f = fInfo.Single(field => field.Name == lastParse.typeName);
                        f.SetValue(obj, GetValidValue(f, fieldValue as string));
                    }
                    else
                    {
                        fInfo.Single(f => f.Name == lastParse.typeName).SetValue(obj, fieldValue);
                    }
                    
                }
                textEntries.RemoveAt(0);
                return obj;
            }
            if (lastParse.lastParseResult == ParseResult.ParseResultEnum.ComplexField)
            {
                ParseResult r = lastParse;
                int localTabs = GetTabsCount(textEntries[0]) - 1;
                textEntries.Insert(0, GetTabs(localTabs) + lastParse.value);
                T obj = Parse<T>();
                lastParse = r;
                return obj;
            }
            if (lastParse.lastParseResult == ParseResult.ParseResultEnum.Field)
            {
                return (T)Convert.ChangeType(lastParse.value, typeof(T));
            }
            throw new InvalidDataException("Invalid data format");
        }

        /// <summary>
        /// Serializes object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="obj"></param>
        public void Serialize<T>(Stream s, T obj)
        {
            
            using (StreamWriter sw = new StreamWriter(s))
            {
                if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
                {
                    tabCount = 1;
                    string result = string.Format("Container:{0}.{1}\n", obj.GetType().Namespace, obj.GetType().Name);
                    IEnumerable<object> collection = (IEnumerable<object>)obj;
                    using (var collectionElem = collection.GetEnumerator())
                    {
                        while (collectionElem.MoveNext())
                        {
                            result += GetSerializationString(collectionElem.Current, collectionElem.Current.GetType().Name, null);
                        }
                    }
                    sw.Write(result + "EndContainer");
                }
                else
                {
                    tabCount = 0;
                    sw.Write(GetSerializationString(obj, obj.GetType().Name, null));
                }
            } 
        }

        /// <summary>
        /// Recursive function for representing object as serialized string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="complexFieldName"></param>
        /// <returns></returns>
        private string GetSerializationString<T>(T obj, string name, string complexFieldName)
        {
            string result = null;
            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
            {
               
                IEnumerable<object> collection = (IEnumerable<object>)obj;
                result = string.Format("Container:{0}.{1}\n",obj.GetType().Namespace,obj.GetType().Name);
                using (var collectionElem = collection.GetEnumerator())
                {
                    while (collectionElem.MoveNext())
                    {
                        result += GetSerializationString(collectionElem.Current, collectionElem.Current.GetType().Name, null);
                    }
                }
                result += "EndContainer\n";
            }
            else
            {          
                FieldInfo[] fieldInfos = GetFieldInfos(obj);
                if (fieldInfos.Length > 1 && !(obj is string))
                {
                    if (complexFieldName != null)
                    {
                        complexFieldName += "=";
                    }
                    result = string.Format("{0}{1}Class:{2}.{3}\n", GetTabs(tabCount), complexFieldName,obj.GetType().Namespace, name);
                    tabCount++;

                    foreach (var info in fieldInfos)
                    { 
                        if (IsTypePrimitive(info.FieldType))
                        {
                            result += GetSerializationString(info.GetValue(obj), info.Name, null);
                        }
                        else
                        {
                            result += GetSerializationString(info.GetValue(obj), info.FieldType.Name, info.Name);
                        }
                        
                    }
                    tabCount--;
                    result += GetTabs(tabCount)+"EndClass\n";
                }
                else
                {
                    if (obj is string)
                    {
                        result = string.Format("{0}{1}={2}\n", GetTabs(tabCount), name, obj);
                    }
                    else
                    {
                        result = string.Format("{0}{1}={2}\n", GetTabs(tabCount), name, fieldInfos[0].GetValue(obj));
                    }
                                        
                }
                
            }
            return result;
        }

        /// <summary>
        /// Gets fields infos of specified object
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private FieldInfo[] GetFieldInfos(object o)
        {
            return o.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        /// <summary>
        /// Returns specified number of tabs
        /// </summary>
        /// <param name="tabCount"></param>
        /// <returns></returns>
        private string GetTabs(int tabCount)
        {
            string tmp = null;
            for (int i = 0; i < tabCount; i++)
            {
                tmp += "\t";
            }
            return tmp;
        }

        /// <summary>
        /// Check if type is primitive or complex
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool IsTypePrimitive(Type t)
        {
            return t.IsPrimitive || t.Equals(typeof(string)) || t.IsEnum || t.Equals(typeof(decimal));
        }

        /// <summary>
        /// Parses current line to determine results
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private ParseResult ParseLine(string line)
        {
            line = line.Trim();
            string[] strings = line.Split(':', '=');
            if (strings.Length == 1)
            {
                if (strings[0] == "EndClass")
                {
                    return new ParseResult(ParseResult.ParseResultEnum.ClassEnd, null, null);
                } 
                if (strings[0] == "EndContainer")
                {
                    return new ParseResult(ParseResult.ParseResultEnum.ContainerEnd, null, null);
                }                    
            }
            if (strings.Length == 2)
            {
                if (strings[0] == "Container")
                {
                    return new ParseResult(ParseResult.ParseResultEnum.Container, strings[1], null);
                }
                if (strings[0] == "Class")
                {
                    return new ParseResult(ParseResult.ParseResultEnum.Class, strings[1], null);
                }
                return new ParseResult(ParseResult.ParseResultEnum.Field, strings[0], strings[1]);
            }
            if (strings.Length == 3)
            {
                if (strings[1] == "Class")
                {
                    return new ParseResult(ParseResult.ParseResultEnum.ComplexField, strings[0], strings[1] + ":" + strings[2]);
                }
            }
            throw new FileFormatException("Invalid file format");           
        }

        /// <summary>
        /// Creates instance of specified type using type name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private T CreateInstance<T>(string name)
        {
            Type type = Type.GetType(name);
            if (type != null)
            {
                if (type.ContainsGenericParameters)
                {
                    return (T)Activator.CreateInstance(typeof(T));
                }
                else
                {
                    return (T)Activator.CreateInstance(type);
                }
                
            }
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(name);
                if (type != null)
                {
                    if (type.ContainsGenericParameters)
                    {
                        return (T)Activator.CreateInstance(typeof(T));
                    }
                    else
                    {
                        return (T)Activator.CreateInstance(type);
                    }
                }
            }
            throw new Exception("Cannot create unknown type");
        }

        /// <summary>
        /// Gets tabs count in specified line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private int GetTabsCount(string line)
        {
            return line.Where(c => c == '\t').Count();
        }

        /// <summary>
        /// Converts string value to object
        /// </summary>
        /// <param name="fInfo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private object GetValidValue(FieldInfo fInfo, string value)
        {
            object assignValue = null;
            var underlyingType = Nullable.GetUnderlyingType(fInfo.FieldType);
            if (underlyingType == null)
            {
                if (fInfo.FieldType.IsEnum)
                {
                    assignValue = Enum.Parse(fInfo.FieldType, value);
                }
                else
                {
                    assignValue = Convert.ChangeType(value, fInfo.FieldType, null);
                }
            }
            else
            {
                if (String.IsNullOrEmpty(value))
                {
                    assignValue = null;
                }
                else
                {
                    assignValue = Convert.ChangeType(value, underlyingType, null);
                }
            }
            return assignValue;
        }

        /// <summary>
        /// Creates generic method 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="argType"></param>
        /// <returns></returns>
        private MethodInfo GetGenericMethod(string name, Type argType)
        {
            MethodInfo method = this.GetType().GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
            return method.MakeGenericMethod(argType);
        }
    }
}
