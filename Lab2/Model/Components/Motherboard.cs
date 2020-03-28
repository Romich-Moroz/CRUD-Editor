namespace Lab2
{
    [System.Runtime.Serialization.DataContract]
    [FieldName(FieldName = "Motherboard")]
    [System.Serializable]
    class Motherboard : Component
    {
        #region Private Fields

        /// <summary>
        /// Defines a form factor for the motherboard
        /// </summary>
        [FieldName(FieldName = "Form factor")]
        [System.Runtime.Serialization.DataMember(Name = "FormFactor")]
        private FormFactor formFactor;

        /// <summary>
        /// Defines a CPU socket for the motherboard
        /// </summary>
        [FieldName(FieldName = "Socket")]
        [System.Runtime.Serialization.DataMember(Name = "Socket")]
        private Socket socket;

        /// <summary>
        /// Defines a chipset for the motherboard
        /// </summary>
        [FieldName(FieldName = "Chipset")]
        [System.Runtime.Serialization.DataMember(Name = "Chipset")]
        private Chipset chipset;

        /// <summary>
        /// Defines how many RAM slots are available 
        /// </summary>
        [FieldName(FieldName = "Maximum memory slots")]
        [System.Runtime.Serialization.DataMember(Name = "MaxMemSlots")]
        private int maxMemorySlots;

        /// <summary>
        /// Defines the max supported RAM frequency
        /// </summary>
        [FieldName(FieldName = "Maximum memory frequency")]
        [System.Runtime.Serialization.DataMember(Name = "MaxMemFreq")]
        private int maxMemoryFrequency;

        /// <summary>
        /// Defines how many parallel channels are supported
        /// </summary>
        [FieldName(FieldName = "Maximum memory channels support")]
        [System.Runtime.Serialization.DataMember(Name = "MaxMemChannelsSupport")]
        private int maxMemoryChannelsSupport;

        /// <summary>
        /// Defines how many SSD slots are supported
        /// </summary>
        [FieldName(FieldName = "Maximum ssd slots")]
        [System.Runtime.Serialization.DataMember(Name = "MaxSsdSlots")]
        private int maxSsdSlots;

        /// <summary>
        /// Defines support for GPU integrated into CPU
        /// </summary>
        [FieldName(FieldName = "Integrated GPU support")]
        [System.Runtime.Serialization.DataMember(Name = "IntegratedGpuSupport")]
        private bool integratedGpuSupport;

        #endregion
    }
}
