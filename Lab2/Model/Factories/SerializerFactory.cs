namespace Lab2
{
    static class SerializerFactory
    {
        /// <summary>
        /// Creates any serializer that is inherited from ISerializer interface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>() where T: ISerializer, new() 
        {
            return new T();
        }
}
}
