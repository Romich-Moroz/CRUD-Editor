using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Lab2
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #region Commands

        /// <summary>
        /// Defines an action to do on create command
        /// </summary>
        public ICommand CreateCommand { get; set; }

        /// <summary>
        /// Defines an action to do on update command
        /// </summary>
        public ICommand UpdateCommand { get; set; }

        /// <summary>
        /// Defines an action to do on delete command
        /// </summary>
        public ICommand DeleteCommand { get; set; }

        #endregion

        /// <summary>
        /// Default costructor for view model
        /// </summary>
        public ViewModel()
        {
            this.CreateCommand = new RelayCommand<Type>(CreateComponent, CanCreateComponent);
            this.UpdateCommand = new RelayCommand<Component>(UpdateComponent, CanUpdateComponent);
            this.DeleteCommand = new RelayCommand<Component>(DeleteComponent, CanDeleteComponent);
        }

        #region Properties      
        
        private Type _SelectedComponentType;
        /// <summary>
        /// Property that defines which Tree View component was selected and determines user interface that needs to be created
        /// </summary>
        public Type SelectedComponentType
        {
            get
            {
                return _SelectedComponentType;
            }
            set
            {
                if (_SelectedComponentType != value)
                {
                    _SelectedComponentType = value;
                    if (value != null)
                    {
                        SelectedComponentFieldsList = new ObservableCollection<ComponentField>(Model.GetAllFieldsOfComponentByTypeName(CreatableTypes, value.Name).Select(f => new ComponentField(f, f.FieldType == typeof(bool) ? "False" : null)));
                    }
                    else 
                    {
                        SelectedComponentFieldsList = null;
                    }
                    FilteredComponentsCollection = FilterCollectionByType(ComponentCollection, SelectedComponentType);
                    SelectedComponentInstance = null;
                }               
            }
        }


        private Component _SelectedComponentInstance;
        /// <summary>
        /// Defines what component instance is currently selected and what data should be loaded to created fields
        /// </summary>
        public Component SelectedComponentInstance
        {
            get
            {
                return _SelectedComponentInstance;
            }
            set
            {
                if (_SelectedComponentInstance != value)
                {
                    _SelectedComponentInstance = value;
                    if (value != null)
                    {
                        ReadComponent(value);
                    }
                                     
                }
            }
        }

        public ObservableCollection<Component> FilteredComponentsCollection { get; set; }

        /// <summary>
        /// Defines a collection of fields that should be displayed
        /// </summary>
        public ObservableCollection<ComponentField> SelectedComponentFieldsList { get; set; }
        /// <summary>
        /// Defines all items in the tree view control
        /// </summary>
        public ObservableCollection<ComputerItem> ComputerCollection { get; set; } = new ObservableCollection<ComputerItem>();

        /// <summary>
        /// Defines all available components to create
        /// </summary>
        public ObservableCollection<Type> CreatableTypes { get; private set; } = Model.GetCreatableTypesCollection(typeof(Computer),typeof(Component));

        /// <summary>
        /// Contains all created components
        /// </summary>
        public ObservableCollection<Component> ComponentCollection { get; set; } = new ObservableCollection<Component>();

        #endregion

        #region Private Methods

        /// <summary>
        /// Create command method used to create components
        /// </summary>
        /// <param name="t"></param>
        private void CreateComponent(Type t)
        {           
            Component comp = Model.CreateComponent(t, SelectedComponentFieldsList.Select(field => field.fieldValue).ToArray());
            if (ComponentCollection.Where(c => c.GetName() == comp.GetName() && c.GetType() == t).Count() == 0)
            {
                ComponentCollection.Add(comp);
                if (comp.GetType() == typeof(Computer))
                {
                    ComputerCollection.Add(new ComputerItem(comp as Computer, SelectedComponentFieldsList));
                }
                FilteredComponentsCollection = FilterCollectionByType(ComponentCollection, SelectedComponentType);
            }
            else
            {
                MessageBox.Show("Component of such type with such name is already added!", "Error", MessageBoxButton.OK);
            }
            
        }

        /// <summary>
        /// Used to define if there is any kind of component selected
        /// </summary>
        /// <returns></returns>
        private bool CanCreateComponent()
        {
            return (SelectedComponentType == null) || (SelectedComponentFieldsList.Where(f => f.fieldInfo.FieldType.BaseType == typeof(Component) ? f.fieldValue == null : string.IsNullOrEmpty(f.fieldValue?.ToString())).Count() != 0) ? false : true;
        }

        /// <summary>
        /// Returns a new collection with all items of type t in collection c
        /// </summary>
        /// <param name="collection"> Where to search collection</param>
        /// <param name="t"> Instance type to search</param>
        private ObservableCollection<Component> FilterCollectionByType(ObservableCollection<Component> c, Type t)
        {
            return new ObservableCollection<Component>(c.Where(item => item.GetType() == t));
        }

        /// <summary>
        /// Reads all fields from component parameter and writes them to created fields
        /// </summary>
        /// <param name="c"></param>
        private void ReadComponent(Component c)
        {
            object[] fieldValues = _SelectedComponentInstance.Read();
            for (int i = 0; i < fieldValues.Count(); i++)
            {
                if (fieldValues[i]?.GetType() == typeof(string))
                {
                    SelectedComponentFieldsList[i].fieldValue = fieldValues[i] as string;
                }
                else
                {
                    SelectedComponentFieldsList[i].fieldValue = fieldValues[i] as Component;
                }
                
            }
        }

        /// <summary>
        /// Updates component instance from user input
        /// </summary>
        /// <param name="t"></param>
        private void UpdateComponent(Component t)
        {

            int index = 0;
            if (t is Computer) //Required to find match in computer collection because it might be impossible to find it after update call
            {
                index = ComputerCollection.IndexOf(ComputerCollection.Single(i => i.Pc == t));
            }
            t.Update(SelectedComponentFieldsList.Select(field => field.fieldValue).ToArray());
            SelectedComponentType = null;
            SelectedComponentType = t.GetType();        
            SelectedComponentInstance = t;
            List<ComputerItem> tmp = ComputerCollection.Where(i => i.Fields.Where(f => f.fieldInfo.FieldType == t.GetType()).Count() != 0).ToList();
            tmp.ForEach(i => i.Fields.Where(f => f.fieldInfo.FieldType == t.GetType()).ToList().ForEach(f => { f.fieldInfo.SetValue(i.Pc, t); f.fieldValue = t; }));
            if (t is Computer)
            {
                ComputerCollection[index] = new ComputerItem(t as Computer, SelectedComponentFieldsList);
            }
            var a = ComputerCollection;
            ComputerCollection = null;
            ComputerCollection = a;


        }

        /// <summary>
        /// Check if component instance is selected and all fields are valid
        /// </summary>
        /// <returns></returns>5
        private bool CanUpdateComponent()
        {
            return (SelectedComponentType == null) || (SelectedComponentFieldsList.Where(f => f.fieldInfo.FieldType.BaseType == typeof(Component) ? f.fieldValue == null : string.IsNullOrEmpty(f.fieldValue?.ToString())).Count() != 0) || (SelectedComponentInstance == null) ? false : true;
        }

        /// <summary>
        /// Deletes selected component instance
        /// </summary>
        /// <param name="t"></param>
        private void DeleteComponent(Component t)
        {
            if (t.GetType() == typeof(Computer))
            {
                var c = ComputerCollection.Where(f => f.Pc == t).Single();
                ComputerCollection.Remove(c);
            }
            else
            {
                List<ComputerItem> tmp = ComputerCollection.Where(i => i.Fields.Where(f => f.fieldInfo.FieldType == t.GetType()).Count() != 0).ToList();
                tmp.ForEach(i => i.Fields.Where(f => f.fieldInfo.FieldType == t.GetType()).ToList().ForEach(f => { f.fieldInfo.SetValue(i.Pc, null); f.fieldValue = null;  }));
                var a = ComputerCollection;
                ComputerCollection = null;
                ComputerCollection = a;
            }
            ComponentCollection.Remove(t);
            
        }

        /// <summary>
        /// Checks if component instance is selected
        /// </summary>
        /// <returns></returns>
        private bool CanDeleteComponent()
        {
            return SelectedComponentInstance == null ? false : true;
        }

        #endregion

        /// <summary>
        /// Used to select component on properties panel depending on what component is selected in tree view
        /// </summary>
        /// <param name="value"></param>
        public void TreeViewUpdateProperties(object value)
        {
            if (value is ComputerItem)
            {
                SelectedComponentType = (value as ComputerItem).Pc.GetType();
                SelectedComponentInstance = (value as ComputerItem).Pc;
            }
            if (value is ComponentField && (value as ComponentField).fieldValue?.GetType().BaseType == typeof(Component))
            {
                SelectedComponentType = (value as ComponentField).fieldInfo.FieldType;
                SelectedComponentInstance = (value as ComponentField).fieldValue as Component;
            }
        }
        
    }
}
