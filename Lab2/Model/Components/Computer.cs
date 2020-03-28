namespace Lab2
{
    [System.Runtime.Serialization.DataContract]
    [FieldName(FieldName = "Computer")]
    [System.Serializable]
    class Computer : Component
    {

        #region Private Fields

        /// <summary>
        /// Defines a PC motherboard;
        /// </summary>
        [FieldName(FieldName = "Motherboard")]
        [System.Runtime.Serialization.DataMember(Name = "Motherboard")]
        private Motherboard motherboard;

        /// <summary>
        /// Defines a PC power supply;
        /// </summary>
        [FieldName(FieldName = "Power supply")]
        [System.Runtime.Serialization.DataMember(Name = "PowerSupply")]
        private PowerSupply powerSupply;

        /// <summary>
        /// Defines a PC CPU
        /// </summary>
        [FieldName(FieldName = "CPU")]
        [System.Runtime.Serialization.DataMember(Name = "CPU")]
        private CentralProcessingUnit centralProcessingUnit;

        /// <summary>
        /// Defines a PC RAM
        /// </summary>
        [FieldName(FieldName = "RAM")]
        [System.Runtime.Serialization.DataMember(Name = "RAM")]
        private RandomAccessMemory randomAccessMemory;

        /// <summary>
        /// Defines a PC GPU
        /// </summary>
        [FieldName(FieldName = "GPU")]
        [System.Runtime.Serialization.DataMember(Name = "GPU")]
        private GraphicsProcessingUnit graphicsProcessingUnit;

        /// <summary>
        /// Defines a PC storage
        /// </summary>
        [FieldName(FieldName = "Storage")]
        [System.Runtime.Serialization.DataMember(Name = "Storage")]
        private Storage storage;
        #endregion

    }
}
