using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;

namespace Lab2
{
    [Serialization(TypeDescription = "Xml serialization file (*.xml)|*.xml")]
    class XmlSerializer : ISerializer
    {
        public T Deserialize<T>(Stream s)
        {
            DataContractSerializer xmls = new DataContractSerializer(typeof(T));
            T tmp = (T)xmls.ReadObject(s);
            s.Close();
            return tmp;
        }

        public void Serialize<T>(Stream s, T obj)
        {
            DataContractSerializer xmls = new DataContractSerializer(typeof(T));          
            xmls.WriteObject(s, obj);
            s.Close();
        }
    }
}
