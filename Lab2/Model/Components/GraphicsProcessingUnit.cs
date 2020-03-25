namespace Lab2
{
    [FieldName(FieldName = "GPU")]
    [System.Serializable]
    class GraphicsProcessingUnit : Component
    {
        #region Private Fields

        /// <summary>
        /// Defines a cores frequency
        /// </summary>
        [FieldName(FieldName = "Memory frequency")]
        private int  frequency;

        /// <summary>
        /// Defines GPU memory size
        /// </summary>
        [FieldName(FieldName = "Memory size")]
        private int memorySize;

        /// <summary>
        /// Defines the amount of GPU processors
        /// </summary>
        [FieldName(FieldName = "GPU cores")]
        private int  processors;

        /// <summary>
        /// Defines a GPU bus width
        /// </summary>
        [FieldName(FieldName = "GPU bus width")]
        private int  busWidth;

        /// <summary>
        /// Defines a recommended Power
        /// </summary>
        [FieldName(FieldName = "GPU recommended power")]
        private int  recommendedPower;

        #endregion
    }
}
