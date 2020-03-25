namespace Lab2
{
    [FieldName(FieldName = "Storage")]
    [System.Serializable]
    class Storage : Component
    {
        #region Private Fields

        /// <summary>
        /// Defines a storage type
        /// </summary>
        [FieldName(FieldName = "Storage type")]
        private StorageType storageType;

        /// <summary>
        /// Defines a size of the storage in gigabytes
        /// </summary>
        [FieldName(FieldName = "Storage size")]
        private int storageSize;

        /// <summary>
        /// defines a read speed in megabytes
        /// </summary>
        [FieldName(FieldName = "Maximum read speed")]
        private int readSpeed;

        /// <summary>
        /// defines a write speed in megabytes
        /// </summary>
        [FieldName(FieldName = "Maximum write speed")]
        private int writeSpeed;

        /// <summary>
        /// defines a buffer size in megabytes
        /// </summary>
        [FieldName(FieldName = "Buffer size")]
        private int bufferSize;
        #endregion

    }
}
