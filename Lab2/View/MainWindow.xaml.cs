using System.Windows;
using System.Windows.Input;

namespace Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }

        /// <summary>
        /// Used to change representation of properties based on what is currently selected in treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComputersList_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            (DataContext as ViewModel).TreeViewUpdateProperties(e.NewValue);
        }

        /// <summary>
        /// Used to filter input and allows digits only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewKeyDownInt(object sender, KeyEventArgs e)
        {
            if (((e.Key < Key.D0) || (e.Key > Key.D9)) && (e.Key != Key.Back))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Used to filter input and allows digits and dot only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewKeyDownDouble(object sender, KeyEventArgs e)
        {
            if (((e.Key < Key.D0) || (e.Key > Key.D9)) && (e.Key != Key.Back) && (e.Key != Key.OemPeriod))
            {
                e.Handled = true;
            }
        }
    }
}
