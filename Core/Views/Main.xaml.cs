using ICSharpCode.AvalonEdit;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace JAO_PI.Core.Views
{
    /// <summary>
    /// Interaktionslogik für Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private OpenFileDialog openFileDialog = null;
        public Main()
        {
            InitializeComponent();

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PAWN Files (*.inc, *.pwn)|*.inc;*.pwn|All files (*.*)|*.*";
            openFileDialog.Title = "Open PAWN File...";
            //openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Create_File_Click(object sender, RoutedEventArgs e)
        {
            TextEditor text = new TextEditor();
            text.FontSize = 13;
            text.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            text.FontFamily = new FontFamily("Consolas");
            text.ShowLineNumbers = true;
            text.Margin = new Thickness(0, 0, 5, 0);

            Grid grid = new Grid();
            grid.Children.Add(text);

            TabItem tab = new TabItem();
            tab.Header = "new.pwn";
            tab.Content = grid;

            tabControl.Items.Add(tab);
            tabControl.SelectedItem = tab;

            Empty_Message.Visibility = Visibility.Hidden;
            Empty_Message.IsEnabled = false;

            tabControl.Visibility = Visibility.Visible;            
        }

        private void Open_File_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                TextEditor text = new TextEditor();
                text.FontSize = 13;
                text.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                text.FontFamily = new FontFamily("Consolas");
                text.ShowLineNumbers = true;
                text.Text = File.ReadAllText(openFileDialog.FileName, System.Text.Encoding.Default);
                text.Margin = new Thickness(0, 0, 5, 0);

                Grid grid = new Grid();
                grid.Children.Add(text);

                TabItem tab = new TabItem();
                tab.Header = openFileDialog.SafeFileName;
                tab.Content = grid;

                tabControl.Items.Add(tab);
                tabControl.SelectedItem = tab;

                Empty_Message.Visibility = Visibility.Hidden;
                Empty_Message.IsEnabled = false;

                tabControl.Visibility = Visibility.Visible;
            }
        }

        private void Close_File_Click(object sender, RoutedEventArgs e)
        {
            Empty_Message.IsEnabled = true;
            Close_File.IsEnabled = false;
            Empty_Message.Visibility = Visibility.Visible;
        }
    }
}
