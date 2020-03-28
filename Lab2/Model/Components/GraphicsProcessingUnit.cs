namespace Lab2
{
    [System.Runtime.Serialization.DataContract]
    [FieldName(FieldName = "GPU")]
    [System.Serializable]
    class GraphicsProcessingUnit : Component
    {
        #region Private Fields

        /// <summary>
        /// Defines a cores frequency
        /// </summary>
        [FieldName(FieldName = "Memory frequency")]
        [System.Runtime.Serialization.DataMember(Name = "MemFreq")]
        private int  frequency;

        /// <summary>
        /// Defines GPU memory size
        /// </summary>
        [FieldName(FieldName = "Memory size")]
        [System.Runtime.Serialization.DataMember(Name = "MemSize")]
        private int memorySize;

        /// <summary>
        /// Defines the amount of GPU processors
        /// </summary>
        [FieldName(FieldName = "GPU cores")]
        [System.Runtime.Serialization.DataMember(Name = "GPUCores")]
        private int  processors;

        /// <summary>
        /// Defines a GPU bus width
        /// </summary>
        [FieldName(FieldName = "GPU bus width")]
        [System.Runtime.Serialization.DataMember(Name = "BusWidth")]
        private int  busWidth;

        /// <summary>
        /// Defines a recommended Power
        /// </summary>
        [FieldName(FieldName = "GPU recommended power")]
        [System.Runtime.Serialization.DataMember(Name = "RequiredPower")]
        private int  recommendedPower;

        #endregion
    }
}
