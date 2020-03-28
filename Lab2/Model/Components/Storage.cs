namespace Lab2
{
    [System.Runtime.Serialization.DataContract]
    [FieldName(FieldName = "Storage")]
    [System.Serializable]
    class Storage : Component
    {
        #region Private Fields

        /// <summary>
        /// Defines a storage type
        /// </summary>
        [FieldName(FieldName = "Storage type")]
        [System.Runtime.Serialization.DataMember(Name = "StorageType")]
        private StorageType storageType;

        /// <summary>
        /// Defines a size of the storage in gigabytes
        /// </summary>
        [FieldName(FieldName = "Storage size")]
        [System.Runtime.Serialization.DataMember(Name = "StorageSize")]
        private int storageSize;

        /// <summary>
        /// defines a read speed in megabytes
        /// </summary>
        [FieldName(FieldName = "Maximum read speed")]
        [System.Runtime.Serialization.DataMember(Name = "MaxReadSpeed")]
        private int readSpeed;

        /// <summary>
        /// defines a write speed in megabytes
        /// </summary>
        [FieldName(FieldName = "Maximum write speed")]
        [System.Runtime.Serialization.DataMember(Name = "MaxWriteSpeed")]
        private int writeSpeed;

        /// <summary>
        /// defines a buffer size in megabytes
        /// </summary>
        [FieldName(FieldName = "Buffer size")]
        [System.Runtime.Serialization.DataMember(Name = "BufferSize")]
        private int bufferSize;
        #endregion

    }
}
