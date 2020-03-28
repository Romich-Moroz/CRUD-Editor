namespace Lab2
{
    [System.Runtime.Serialization.DataContract]
    [FieldName(FieldName = "RAM")]
    [System.Serializable]
    class RandomAccessMemory : Component
    {
        #region privateFields
        /// <summary>
        /// Defines the size of each plank
        /// </summary>
        [FieldName(FieldName = "Memory size")]
        [System.Runtime.Serialization.DataMember(Name = "MemSize")]
        private int size;

        /// <summary>
        /// Defines max frequency of each plank
        /// </summary>
        [FieldName(FieldName = "Maximum memory frequency")]
        [System.Runtime.Serialization.DataMember(Name = "MaxMemFreq")]
        private int maxMemoryFrequency;

        /// <summary>
        /// Defines max channels support of memory
        /// </summary>
        [FieldName(FieldName = "Maximum memory channels support")]
        [System.Runtime.Serialization.DataMember(Name = "MaxMemChannelsSupport")]
        private int maxChannelsSupport;

        #endregion

    }
}
