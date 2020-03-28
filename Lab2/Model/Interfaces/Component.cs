using System;
using System.Linq;
using System.Reflection;

namespace Lab2
{
    [System.Runtime.Serialization.DataContract]
    [Serializable]
    [System.Runtime.Serialization.KnownType("GetDerivedTypes")]
    /// <summary>
    /// Base class for all components of computer
    /// </summary>
    public abstract class Component
    {
        #region Types

        
        /// <summary>
        /// Defines a form factor for the motherboard
        /// </summary>
        public enum FormFactor { ATX, microATX, FlexATX };

        /// <summary>
        /// Defines a CPU socket for the motherboard
        /// </summary>
        public enum Socket { LGA1150, LGA1151, AM3 };

        /// <summary>
        /// Defines a chipset for the motherboard
        /// </summary>
        public enum Chipset { Z170, Z270, Z370, Z470 };

        /// <summary>
        /// Defines a storage type
        /// </summary>
        public enum StorageType { HDD, SSD };

        /// <summary>
        /// Defines a price segment type
        /// </summary>
        public enum PriceSegment { LowEnd, MiddleEnd, HighEnd };

        #endregion

        #region Private Fields

        /// <summary>
        /// Defines the name of the component
        /// </summary>
        [FieldName(FieldName = "Name")]
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        protected string name;

        /// <summary>
        /// Defines the name of the vendor
        /// </summary>
        [FieldName(FieldName = "Vendor")]
        [System.Runtime.Serialization.DataMember(Name = "Vendor")]
        protected string vendor;

        /// <summary>
        /// Defines the price segment of the component
        /// </summary>
        [FieldName(FieldName = "Price segment")]
        [System.Runtime.Serialization.DataMember(Name = "PriceSegment")]
        protected PriceSegment priceSegment;
        /// <summary>
        /// Defines the price of the component
        /// </summary>
        [FieldName(FieldName = "Price")]
        [System.Runtime.Serialization.DataMember(Name = "Price")]
        protected double price;

        #endregion

        private static Type[] GetDerivedTypes()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Component))).ToArray();
        }

        #region Public Methods

        /// <summary>
        /// Universal method that is used to get all fields as strings array of any class that inherits this class
        /// </summary>
        /// <returns></returns>
        public virtual object[] Read()
        {
            return this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Select(f => f.FieldType.BaseType != typeof(Component) ? f.GetValue(this).ToString() : f.GetValue(this)).ToArray();
        }

        /// <summary>
        /// Universal method that is used to update all fields of class that inherits this class
        /// </summary>
        /// <param name="p"></param>
        public virtual void Update(object[] p)
        {
            FieldInfo[] fInfo = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            for (int i = 0; i < fInfo.Length; i++)
            {
                object assignValue = null;
                var underlyingType = Nullable.GetUnderlyingType(fInfo[i].FieldType);
                if (underlyingType == null)
                {
                    if (fInfo[i].FieldType.IsEnum)
                    {
                        assignValue = Enum.Parse(fInfo[i].FieldType, p[i] as string);
                    }
                    else
                    {
                        if (fInfo[i].FieldType.BaseType == typeof(Component)) 
                        {

                            fInfo[i].SetValue(this, p[i]);

                            continue;
                        }
                        else
                        {
                            assignValue = Convert.ChangeType(p[i].ToString(), fInfo[i].FieldType, null);
                        }                       
                    }                  
                } 
                else
                {
                    if (String.IsNullOrEmpty(p[i] as string))
                    {
                        assignValue = null;
                    }
                    else
                    {
                        assignValue = Convert.ChangeType(p[i].ToString(), underlyingType, null);
                    }
                }
                fInfo[i].SetValue(this, assignValue); //'This' here reference to the type where exists field that we want to change 
            }
        }
        /// <summary>
        /// This method is used to get name field of class
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            return this.name;
        }

        #endregion

    }
}
