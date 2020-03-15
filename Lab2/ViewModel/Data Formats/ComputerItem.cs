using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Lab2
{
    class ComputerItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Defines a computer
        /// </summary>
        public Component Pc { get; set; }

        /// <summary>
        /// Fields collection of computer specified in Pc field
        /// </summary>
        public ObservableCollection<ComponentField> Fields { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="c"></param>
        /// <param name="f"></param>
        public ComputerItem (Computer c, ObservableCollection<ComponentField> f)
        {
            this.Pc = c;
            this.Fields = f;
        }
    }
}
