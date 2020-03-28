namespace Lab2
{
    [System.Runtime.Serialization.DataContract]
    [FieldName(FieldName = "Power supply")]
    [System.Serializable]
    class PowerSupply : Component
    {
        #region Private Fields

        /// <summary>
        /// Defines a power output without efficiency concern
        /// </summary>
        [FieldName(FieldName = "Designed power")]
        [System.Runtime.Serialization.DataMember(Name = "DesignedPower")]
        private int designedPower;

        /// <summary>
        /// Defines efficiency of power supply
        /// </summary>
        [FieldName(FieldName = "Efficiency")]
        [System.Runtime.Serialization.DataMember(Name = "Efficiency")]
        private int efficiency;

        #endregion

    }
}
