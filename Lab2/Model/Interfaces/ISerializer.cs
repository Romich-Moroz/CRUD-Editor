using System.IO;

namespace Lab2
{
    interface ISerializer
    {
        /// <summary>
        /// Serializes object
        /// </summary>
        /// <param name="s"></param>
        /// <param name="obj"></param>
        void Serialize(Stream s, object obj);

        /// <summary>
        /// Deserializes object
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        object Deserialize(Stream s);
    }
}
