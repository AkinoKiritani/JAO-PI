using ICSharpCode.AvalonEdit;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            MainController.RegisterTabControl(this.tabControl);
            MainController.RegisterEmptyMessage(this.Empty_Message);
            MainController.RegisterSaveOptions(this.Save, this.SaveAs, this.Close_File);

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PAWN Files (*.inc, *.pwn)|*.inc;*.pwn|Include Files (*.inc)|*.inc|Only Pawn Files (*.pwn)|*.pwn|All files (*.*)|*.*";
            openFileDialog.Title = "Open PAWN File...";

            generator = new Classes.Generator();

            RoutedCommand SaveCmd = new RoutedCommand();
            SaveCmd.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(SaveCmd, Save_Click));
        }

        private void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.GetLength(0) > 1)
            {
                string[] arg = arguments[1].Split('\\');
                TabItem tab = generator.TabItem(arguments[1], arg[arg.Length-1], File.ReadAllText(arguments[1], System.Text.Encoding.Default));
                
                MainController.tabControl.Items.Add(tab);
                MainController.tabControl.SelectedItem = tab;

                MainController.Empty_Message.Visibility = Visibility.Hidden;
                MainController.Empty_Message.IsEnabled = false;

                MainController.tabControl.Visibility = Visibility.Visible;
                if (MainController.tabControl.Items.Count == 1)
                {
                    MainController.ToggleSaveOptions(true);
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void Create_File_Click(object sender, RoutedEventArgs e)
        {
            TabItem tab = generator.TabItem(Environment.CurrentDirectory, "new.pwn", null);
            
            MainController.tabControl.Items.Add(tab);
            MainController.tabControl.SelectedItem = tab;

            MainController.Empty_Message.Visibility = Visibility.Hidden;
            MainController.Empty_Message.IsEnabled = false;

            MainController.tabControl.Visibility = Visibility.Visible;

            if (MainController.tabControl.Items.Count == 1)
            {
                MainController.ToggleSaveOptions(true);
            }
        }

        private void Open_File_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                TabItem tab = generator.TabItem(openFileDialog.FileName, openFileDialog.SafeFileName, File.ReadAllText(openFileDialog.FileName, System.Text.Encoding.Default));
                MainController.tabControl.Items.Add(tab);
                MainController.tabControl.SelectedItem = tab;

                MainController.Empty_Message.Visibility = Visibility.Hidden;
                MainController.Empty_Message.IsEnabled = false;

                MainController.tabControl.Visibility = Visibility.Visible;

                if (MainController.tabControl.Items.Count == 1)
                {
                    MainController.ToggleSaveOptions(true);
                }
            }
        }

        private void Close_File_Click(object sender, RoutedEventArgs e)
        {
            if (MainController.tabControl.Items.Count == 0)
            {
                MainController.ToggleSaveOptions(false);

                MainController.Empty_Message.IsEnabled = true;
                MainController.Empty_Message.Visibility = Visibility.Visible;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(MainController.tabControl.Items.Count > 0 && MainController.tabControl.Visibility == Visibility.Visible)
            { 
                MainController.SaveTab(MainController.tabControl.Items[MainController.tabControl.SelectedIndex] as TabItem);
                MessageBox.Show("kek");
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (MainController.tabControl.Items.Count > 0 && MainController.tabControl.Visibility == Visibility.Visible)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.Filter = "Only Pawn File (*.pwn)|*.pwn|Include File (*.inc)|*.inc|All files (*.*)|*.*";
                saveFileDialog.Title = "Save PAWN File...";
                if (saveFileDialog.ShowDialog() == true)
                {
                    MainController.SaveTab(MainController.tabControl.Items[MainController.tabControl.SelectedIndex] as TabItem, saveFileDialog);
                }
            }
        }
    }
}
