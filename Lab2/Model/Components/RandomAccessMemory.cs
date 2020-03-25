namespace Lab2
{
    [FieldName(FieldName = "RAM")]
    [System.Serializable]
    class RandomAccessMemory : Component
    {
        #region privateFields
        /// <summary>
        /// Defines the size of each plank
        /// </summary>
        [FieldName(FieldName = "Memory size")]
        private int size;

        /// <summary>
        /// Defines max frequency of each plank
        /// </summary>
        [FieldName(FieldName = "Maximum memory frequency")]
        private int maxMemoryFrequency;

        /// <summary>
        /// Defines max channels support of memory
        /// </summary>
        [FieldName(FieldName = "Maximum memory channels support")]
        private int maxChannelsSupport;

        #endregion

    }
}
