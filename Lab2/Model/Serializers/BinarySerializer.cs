using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab2
{
    class BinarySerializer : ISerializer
    {
        /// <summary>
        /// Deserializes object that is represented as binary stream
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public object Deserialize(Stream s)
        {
            BinaryFormatter bf = new BinaryFormatter();
            object tmp =  bf.Deserialize(s);
            s.Close();
            return tmp;
        }

        /// <summary>
        /// Serializes object into binary stream
        /// </summary>
        /// <param name="s"></param>
        /// <param name="obj"></param>
        public void Serialize(Stream s, object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, obj);
            s.Close();
        }
    }
}
