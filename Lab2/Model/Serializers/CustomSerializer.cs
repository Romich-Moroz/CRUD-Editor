using System.IO;

namespace Lab2
{
    [Serialization(TypeDescription = "Custom serialization file (*.csf)|*.csf")]
    class CustomSerializer : ISerializer
    {

        public T Deserialize<T>(Stream s)
        {

            throw new System.NotImplementedException();
        }

        public void Serialize<T>(Stream s, T obj)
        {
            
            throw new System.NotImplementedException();
        }
    }
}
