using System.Windows;

namespace JAO_PI.Core.Views
{
    /// <summary>
    /// Interaktionslogik für Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Create_File_Click(object sender, RoutedEventArgs e)
        {
            textEditor.Visibility = Visibility.Visible;
            textEditor.ShowLineNumbers = true;
        }
    }
}
