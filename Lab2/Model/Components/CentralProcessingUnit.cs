namespace Lab2
{
    [FieldName(FieldName = "CPU")]
    [System.Serializable]
    class CentralProcessingUnit : Component
    {
        #region Private Fields

        /// <summary>
        /// Defines a CPU socket
        /// </summary>
        [FieldName(FieldName = "Socket")]
        private Socket socket;

        /// <summary>
        /// Defines amount of CPU cores
        /// </summary>
        [FieldName(FieldName = "Cores")]
        private int cores;

        /// <summary>
        /// Defines amount of CPU threads
        /// </summary>
        [FieldName(FieldName = "Threads")]
        private int threads;

        /// <summary>
        /// Defines max frequency of CPU
        /// </summary>
        [FieldName(FieldName = "Max CPU frequency")]
        private int maxFrequency;

        /// <summary>
        /// Defines CPU power consumption 
        /// </summary>
        [FieldName(FieldName = "Thermal design power")]
        private int thermalDesignPower;

        /// <summary>
        /// true, if CPU contains integrated GPU
        /// </summary>
        [FieldName(FieldName = "Integrated GPU")]
        private bool integratedGpu;
        #endregion

    }
}
