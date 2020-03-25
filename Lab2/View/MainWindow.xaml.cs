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

        private void ComputersList_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            (DataContext as ViewModel).TreeViewUpdateProperties(e.NewValue);
        }

        private void TextBox_PreviewKeyDownInt(object sender, KeyEventArgs e)
        {
            if (((e.Key < Key.D0) || (e.Key > Key.D9)) && (e.Key != Key.Back))
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewKeyDownDouble(object sender, KeyEventArgs e)
        {
            if (((e.Key < Key.D0) || (e.Key > Key.D9)) && (e.Key != Key.Back) && (e.Key != Key.OemPeriod))
            {
                e.Handled = true;
            }
        }
    }
}
