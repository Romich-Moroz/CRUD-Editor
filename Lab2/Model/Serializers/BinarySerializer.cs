using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab2
{
    [Serialization(TypeDescription = "Binary serialization file(*.bin)|*.bin")]
    class BinarySerializer : ISerializer
    {
        /// <summary>
        /// Deserializes object that is represented as binary stream
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public T Deserialize<T>(Stream s)
        {
            BinaryFormatter bf = new BinaryFormatter();
            T tmp =  (T)bf.Deserialize(s);
            s.Close();
            return tmp;
        }

        /// <summary>
        /// Serializes object into binary stream
        /// </summary>
        /// <param name="s"></param>
        /// <param name="obj"></param>
        public void Serialize<T>(Stream s, T obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, obj);
            s.Close();
        }
    }
}
