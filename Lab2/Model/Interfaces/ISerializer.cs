using System;
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
        void Serialize<T>(Stream s, T obj);

        /// <summary>
        /// Deserializes object
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        T Deserialize<T>(Stream s);
    }
}
