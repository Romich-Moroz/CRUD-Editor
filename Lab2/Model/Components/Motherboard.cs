namespace Lab2
{
    [FieldName(FieldName = "Motherboard")]
    class Motherboard : Component
    {
        #region Private Fields

        /// <summary>
        /// Defines a form factor for the motherboard
        /// </summary>
        [FieldName(FieldName = "Form factor")]
        private FormFactor formFactor;

        /// <summary>
        /// Defines a CPU socket for the motherboard
        /// </summary>
        [FieldName(FieldName = "Socket")]
        private Socket socket;

        /// <summary>
        /// Defines a chipset for the motherboard
        /// </summary>
        [FieldName(FieldName = "Chipset")]
        private Chipset chipset;

        /// <summary>
        /// Defines how many RAM slots are available 
        /// </summary>
        [FieldName(FieldName = "Maximum memory slots")]
        private int maxMemorySlots;

        /// <summary>
        /// Defines the max supported RAM frequency
        /// </summary>
        [FieldName(FieldName = "Maximum memory frequency")]
        private int maxMemoryFrequency;

        /// <summary>
        /// Defines how many parallel channels are supported
        /// </summary>
        [FieldName(FieldName = "Maximum memory channels support")]
        private int maxMemoryChannelsSupport;

        /// <summary>
        /// Defines how many SSD slots are supported
        /// </summary>
        [FieldName(FieldName = "Maximum ssd slots")]
        private int maxSsdSlots;

        /// <summary>
        /// Defines support for GPU integrated into CPU
        /// </summary>
        [FieldName(FieldName = "Integrated GPU support")]
        private bool integratedGpuSupport;

        #endregion
    }
}
