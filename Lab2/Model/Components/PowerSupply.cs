namespace Lab2
{
    [FieldName(FieldName = "Power supply")]
    class PowerSupply : Component
    {
        #region Private Fields

        /// <summary>
        /// Defines a power output without efficiency concern
        /// </summary>
        [FieldName(FieldName = "Designed power")]
        private int designedPower;

        /// <summary>
        /// Defines efficiency of power supply
        /// </summary>
        [FieldName(FieldName = "Efficiency")]
        private int efficiency;

        #endregion

    }
}
