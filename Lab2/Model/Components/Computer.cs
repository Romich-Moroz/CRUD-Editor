using System.Linq;
using System.Reflection;

namespace Lab2
{
    [FieldName(FieldName = "Computer")]
    class Computer : Component
    {

        #region Private Fields

        /// <summary>
        /// Defines a PC motherboard;
        /// </summary>
        [FieldName(FieldName = "Motherboard")]
        private Motherboard motherboard;

        /// <summary>
        /// Defines a PC power supply;
        /// </summary>
        [FieldName(FieldName = "Power supply")]
        private PowerSupply powerSupply;

        /// <summary>
        /// Defines a PC CPU
        /// </summary>
        [FieldName(FieldName = "CPU")]
        private CentralProcessingUnit centralProcessingUnit;

        /// <summary>
        /// Defines a PC RAM
        /// </summary>
        [FieldName(FieldName = "RAM")]
        private RandomAccessMemory randomAccessMemory;

        /// <summary>
        /// Defines a PC GPU
        /// </summary>
        [FieldName(FieldName = "GPU")]
        private GraphicsProcessingUnit graphicsProcessingUnit;

        /// <summary>
        /// Defines a PC storage
        /// </summary>
        [FieldName(FieldName = "Storage")]
        private Storage storage;
        #endregion

    }
}
