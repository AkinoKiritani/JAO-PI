using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    public class MainMenu
    {
        Classes.Generator generator = null;
        Classes.Utility utility = null;
        private OpenFileDialog openFileDialog = null;
        
        public MainMenu()
        {
            generator = new Classes.Generator();
            utility = new Classes.Utility();

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PAWN Files (*.inc, *.pwn)|*.inc;*.pwn|Include Files (*.inc)|*.inc|Only Pawn Files (*.pwn)|*.pwn|All files (*.*)|*.*";
            openFileDialog.Title = "Open PAWN File...";
        }

        public void Undo_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ICSharpCode.AvalonEdit.TextEditor Editor = utility.GetTextEditor(Controller.Main.tabControl.SelectedIndex);
            Editor.Undo();
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.Main.tabControl.Items.Count > 0 && Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                Controller.Main.SaveTab(Controller.Main.tabControl.Items[Controller.Main.tabControl.SelectedIndex] as TabItem);
            }
        }

        public void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.Main.tabControl.Items.Count > 0 && Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.Filter = "Only Pawn File (*.pwn)|*.pwn|Include File (*.inc)|*.inc|All files (*.*)|*.*";
                saveFileDialog.Title = "Save PAWN File...";
                if (saveFileDialog.ShowDialog() == true)
                {
                    TabItem Tab = Controller.Main.tabControl.Items[Controller.Main.tabControl.SelectedIndex] as TabItem;
                    Controller.Main.SaveTab(Tab, saveFileDialog);
                    Tab.Header = saveFileDialog.SafeFileName;
                }
            }
        }

        public void Close_File_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.Main.tabControl.Items.Count == 0)
            {
                Controller.Main.ToggleSaveOptions(false);

                Controller.Main.Empty_Message.IsEnabled = true;
                Controller.Main.Empty_Message.Visibility = Visibility.Visible;
            }
        }

        public void Compiler_Path_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog CompilerPathDialog = new OpenFileDialog();
            CompilerPathDialog = new OpenFileDialog();
            CompilerPathDialog.Filter = "PAWN Compiler (pawncc.exe)|pawncc.exe";
            CompilerPathDialog.Title = "Set Compiler Path ...";
            CompilerPathDialog.InitialDirectory = Environment.CurrentDirectory;
            if (CompilerPathDialog.ShowDialog() == true)
            {
                Properties.Settings.Default.CompilerPath = CompilerPathDialog.FileName;
                MessageBox.Show("Path set", "JAO PI");
                Properties.Settings.Default.Save();
            }
        }

        public void Cut_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ICSharpCode.AvalonEdit.TextEditor Editor = utility.GetTextEditor(Controller.Main.tabControl.SelectedIndex);
            Editor.Cut();
        }

        public void Copy_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ICSharpCode.AvalonEdit.TextEditor Editor = utility.GetTextEditor(Controller.Main.tabControl.SelectedIndex);
            Editor.Copy();
        }

        public void Paste_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ICSharpCode.AvalonEdit.TextEditor Editor = utility.GetTextEditor(Controller.Main.tabControl.SelectedIndex);
            Editor.Paste();
        }

        public void Compile_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Controller.Worker.SaveWorker.RunWorkerAsync();
        }

        public void Compile(object sender, ExecutedRoutedEventArgs e)
        {
            Controller.Worker.SaveWorker.RunWorkerAsync();
        }

        public void Create_File_Click(object sender, RoutedEventArgs e)
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


        public void Open_File_Click(object sender, RoutedEventArgs e)
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
    }
}
