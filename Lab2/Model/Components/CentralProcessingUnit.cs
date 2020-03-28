namespace Lab2
{
    [System.Runtime.Serialization.DataContract]
    [FieldName(FieldName = "CPU")]
    [System.Serializable]
    class CentralProcessingUnit : Component
    {
        #region Private Fields

        /// <summary>
        /// Defines a CPU socket
        /// </summary>
        [FieldName(FieldName = "Socket")]
        [System.Runtime.Serialization.DataMember(Name = "Socket")]
        private Socket socket;

        /// <summary>
        /// Defines amount of CPU cores
        /// </summary>
        [FieldName(FieldName = "Cores")]
        [System.Runtime.Serialization.DataMember(Name = "Cores")]
        private int cores;

        /// <summary>
        /// Defines amount of CPU threads
        /// </summary>
        [FieldName(FieldName = "Threads")]
        [System.Runtime.Serialization.DataMember(Name = "Threads")]
        private int threads;

        /// <summary>
        /// Defines max frequency of CPU
        /// </summary>
        [FieldName(FieldName = "Max CPU frequency")]
        [System.Runtime.Serialization.DataMember(Name = "CPUFreq")]
        private int maxFrequency;

        /// <summary>
        /// Defines CPU power consumption 
        /// </summary>
        [FieldName(FieldName = "Thermal design power")]
        [System.Runtime.Serialization.DataMember(Name = "TDP")]
        private int thermalDesignPower;

        /// <summary>
        /// true, if CPU contains integrated GPU
        /// </summary>
        [FieldName(FieldName = "Integrated GPU")]
        [System.Runtime.Serialization.DataMember(Name = "GPUExists")]
        private bool integratedGpu;
        #endregion

    }
}
