using ICSharpCode.AvalonEdit;
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

            Classes.MainController.RegisterTabControl(this.tabControl);
            Classes.MainController.RegisterEmptyMessage(this.Empty_Message);
            Classes.MainController.RegisterSaveOptions(this.Save, this.SaveAs, this.Close_File);

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PAWN Files (*.inc, *.pwn)|*.inc;*.pwn|Include Files (*.inc)|*.inc|Only Pawn Files (*.pwn)|*.pwn|All files (*.*)|*.*";
            openFileDialog.Title = "Open PAWN File...";

            generator = new Classes.Generator();
        }
        private void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.GetLength(0) > 1)
            {
                string[] arg = arguments[1].Split('\\');
                TabItem tab = generator.TabItem(arguments[1], arg[arg.Length-1], File.ReadAllText(arguments[1], System.Text.Encoding.Default));
                
                Classes.MainController.tabControl.Items.Add(tab);
                Classes.MainController.tabControl.SelectedItem = tab;

                Classes.MainController.Empty_Message.Visibility = Visibility.Hidden;
                Classes.MainController.Empty_Message.IsEnabled = false;

                Classes.MainController.tabControl.Visibility = Visibility.Visible;
                if (Classes.MainController.tabControl.Items.Count == 1)
                {
                    Classes.MainController.ToggleSaveOptions(true);
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
            
            Classes.MainController.tabControl.Items.Add(tab);
            Classes.MainController.tabControl.SelectedItem = tab;

            Classes.MainController.Empty_Message.Visibility = Visibility.Hidden;
            Classes.MainController.Empty_Message.IsEnabled = false;

            Classes.MainController.tabControl.Visibility = Visibility.Visible;

            if (Classes.MainController.tabControl.Items.Count == 1)
            {
                Classes.MainController.ToggleSaveOptions(true);
            }
        }

        private void Open_File_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                TabItem tab = generator.TabItem(openFileDialog.FileName, openFileDialog.SafeFileName, File.ReadAllText(openFileDialog.FileName, System.Text.Encoding.Default));
                Classes.MainController.tabControl.Items.Add(tab);
                Classes.MainController.tabControl.SelectedItem = tab;

                Classes.MainController.Empty_Message.Visibility = Visibility.Hidden;
                Classes.MainController.Empty_Message.IsEnabled = false;

                Classes.MainController.tabControl.Visibility = Visibility.Visible;

                if (Classes.MainController.tabControl.Items.Count == 1)
                {
                    Classes.MainController.ToggleSaveOptions(true);
                }
            }
        }

        private void Close_File_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.MainController.tabControl.Items.Count == 0)
            {
                Classes.MainController.ToggleSaveOptions(false);

                Classes.MainController.Empty_Message.IsEnabled = true;
                Classes.MainController.Empty_Message.Visibility = Visibility.Visible;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            TabItem SaveTab = Classes.MainController.tabControl.Items[Classes.MainController.tabControl.SelectedIndex] as TabItem;
            Grid SaveGrid = SaveTab.Content as Grid;
            TextEditor SaveEditor = SaveGrid.Children[0] as TextEditor;
            
            System.Text.StringBuilder FileToSave = new System.Text.StringBuilder();
            FileToSave.Append(SaveTab.Uid);
            FileToSave.Append(SaveTab.Header);

            SaveEditor.Save(FileToSave.ToString());
            SaveEditor = null;
            SaveGrid = null;
            SaveTab = null;
            FileToSave = null;

            MessageBox.Show("Save Complete", "JAO PI");
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "Only Pawn File (*.pwn)|*.pwn|Include File (*.inc)|*.inc|All files (*.*)|*.*";
            saveFileDialog.Title = "Save PAWN File...";
            if(saveFileDialog.ShowDialog() == true)
            {
                TabItem SaveTab = Classes.MainController.tabControl.Items[Classes.MainController.tabControl.SelectedIndex] as TabItem;
                Grid SaveGrid = SaveTab.Content as Grid;
                TextEditor SaveEditor = SaveGrid.Children[0] as TextEditor;

                System.Text.StringBuilder FileToSave = new System.Text.StringBuilder();
                FileToSave.Append(SaveTab.Uid);
                FileToSave.Append(SaveTab.Header);

                SaveEditor.Save(saveFileDialog.FileName);
                SaveEditor = null;
                SaveGrid = null;
                SaveTab = null;
                FileToSave = null;

                MessageBox.Show("Save Complete", "JAO PI");
            }
        }
    }
}
