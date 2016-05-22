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
        private Classes.EventHandler handler = null;
        Classes.Generator generator = null;
        public Main()
        {
            handler = new Classes.EventHandler();
            InitializeComponent();

            Controller.Main.RegisterTabControl(this.tabControl);
            Controller.Main.RegisterEmptyMessage(this.Empty_Message);
            Controller.Main.RegisterSaveOptions(this.Save, this.SaveAs, this.Close_File);

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PAWN Files (*.inc, *.pwn)|*.inc;*.pwn|Include Files (*.inc)|*.inc|Only Pawn Files (*.pwn)|*.pwn|All files (*.*)|*.*";
            openFileDialog.Title = "Open PAWN File...";

            generator = new Classes.Generator();

            RoutedCommand SaveCmd = new RoutedCommand();
            SaveCmd.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(SaveCmd, Save_Click));

            Restore.PreviewMouseLeftButtonUp += handler.Restore_PreviewMouseLeftButtonUp;
            Cut.PreviewMouseLeftButtonUp += handler.Cut_PreviewMouseLeftButtonUp;
            Copy.PreviewMouseLeftButtonUp += handler.Copy_PreviewMouseLeftButtonUp;
            Paste.PreviewMouseLeftButtonUp += handler.Paste_PreviewMouseLeftButtonUp;
        }

        private void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.GetLength(0) > 1)
            {
                string[] arg = arguments[1].Split('\\');
                TabItem tab = generator.TabItem(arguments[1], arg[arg.Length-1], File.ReadAllText(arguments[1], System.Text.Encoding.Default));
                
                Controller.Main.tabControl.Items.Add(tab);
                Controller.Main.tabControl.SelectedItem = tab;

                Controller.Main.Empty_Message.Visibility = Visibility.Hidden;
                Controller.Main.Empty_Message.IsEnabled = false;

                Controller.Main.tabControl.Visibility = Visibility.Visible;
                if (Controller.Main.tabControl.Items.Count == 1)
                {
                    Controller.Main.ToggleSaveOptions(true);
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
            
            Controller.Main.tabControl.Items.Add(tab);
            Controller.Main.tabControl.SelectedItem = tab;

            Controller.Main.Empty_Message.Visibility = Visibility.Hidden;
            Controller.Main.Empty_Message.IsEnabled = false;

            Controller.Main.tabControl.Visibility = Visibility.Visible;

            if (Controller.Main.tabControl.Items.Count == 1)
            {
                Controller.Main.ToggleSaveOptions(true);
            }
        }

        private void Open_File_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                TabItem tab = generator.TabItem(openFileDialog.FileName, openFileDialog.SafeFileName, File.ReadAllText(openFileDialog.FileName, System.Text.Encoding.Default));
                Controller.Main.tabControl.Items.Add(tab);
                Controller.Main.tabControl.SelectedItem = tab;

                Controller.Main.Empty_Message.Visibility = Visibility.Hidden;
                Controller.Main.Empty_Message.IsEnabled = false;

                Controller.Main.tabControl.Visibility = Visibility.Visible;

                if (Controller.Main.tabControl.Items.Count == 1)
                {
                    Controller.Main.ToggleSaveOptions(true);
                }
            }
        }

        private void Close_File_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.Main.tabControl.Items.Count == 0)
            {
                Controller.Main.ToggleSaveOptions(false);

                Controller.Main.Empty_Message.IsEnabled = true;
                Controller.Main.Empty_Message.Visibility = Visibility.Visible;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(Controller.Main.tabControl.Items.Count > 0 && Controller.Main.tabControl.Visibility == Visibility.Visible)
            { 
                Controller.Main.SaveTab(Controller.Main.tabControl.Items[Controller.Main.tabControl.SelectedIndex] as TabItem);
                MessageBox.Show("kek");
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.Main.tabControl.Items.Count > 0 && Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.Filter = "Only Pawn File (*.pwn)|*.pwn|Include File (*.inc)|*.inc|All files (*.*)|*.*";
                saveFileDialog.Title = "Save PAWN File...";
                if (saveFileDialog.ShowDialog() == true)
                {
                    Controller.Main.SaveTab(Controller.Main.tabControl.Items[Controller.Main.tabControl.SelectedIndex] as TabItem, saveFileDialog);
                }
            }
        }
    }
}
