using Microsoft.Win32;
using System;
using System.IO;
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
        Classes.Generator generator = null;
        public Main()
        {
            InitializeComponent();

            Classes.MainController.RegisterTabControl(tabControl);
            Classes.MainController.RegisterEmptyMessage(Empty_Message);

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PAWN Files (*.inc, *.pwn)|*.inc;*.pwn|All files (*.*)|*.*";
            openFileDialog.Title = "Open PAWN File...";

            generator = new Classes.Generator();
        }
        private void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.GetLength(0) > 1)
            {
                string[] arg = arguments[1].Split('\\');
                TabItem tab = generator.TabItem(arg[arg.Length-1], File.ReadAllText(arguments[1], System.Text.Encoding.Default));
                Classes.MainController.tabControl.Items.Add(tab);
                Classes.MainController.tabControl.SelectedItem = tab;

                Classes.MainController.Empty_Message.Visibility = Visibility.Hidden;
                Classes.MainController.Empty_Message.IsEnabled = false;

                Classes.MainController.tabControl.Visibility = Visibility.Visible;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void Create_File_Click(object sender, RoutedEventArgs e)
        {
            TabItem tab = generator.TabItem("new.pwn", null);

            Classes.MainController.tabControl.Items.Add(tab);
            Classes.MainController.tabControl.SelectedItem = tab;

            Classes.MainController.Empty_Message.Visibility = Visibility.Hidden;
            Classes.MainController.Empty_Message.IsEnabled = false;

            Classes.MainController.tabControl.Visibility = Visibility.Visible;  
        }

        private void Open_File_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                TabItem tab = generator.TabItem(openFileDialog.SafeFileName, File.ReadAllText(openFileDialog.FileName, System.Text.Encoding.Default));
                Classes.MainController.tabControl.Items.Add(tab);
                Classes.MainController.tabControl.SelectedItem = tab;

                Classes.MainController.Empty_Message.Visibility = Visibility.Hidden;
                Classes.MainController.Empty_Message.IsEnabled = false;

                Classes.MainController.tabControl.Visibility = Visibility.Visible;
            }
        }

        private void Close_File_Click(object sender, RoutedEventArgs e)
        {
            Classes.MainController.Empty_Message.IsEnabled = true;
            Close_File.IsEnabled = false;
            Classes.MainController.Empty_Message.Visibility = Visibility.Visible;
        }
    }
}
