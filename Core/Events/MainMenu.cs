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
        Core.Classes.Generator generator = null;
        Core.Classes.Utility utility = null;
        private OpenFileDialog openFileDialog = null;
        
        public MainMenu()
        {
            generator = new Core.Classes.Generator();
            utility = new Core.Classes.Utility();

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PAWN Files (*.inc, *.pwn)|*.inc;*.pwn|Include Files (*.inc)|*.inc|Only Pawn Files (*.pwn)|*.pwn|All files (*.*)|*.*";
            openFileDialog.Title = "Open PAWN File...";
        }

        internal void Undo_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ICSharpCode.AvalonEdit.TextEditor Editor = utility.GetTextEditor(Core.Controller.Main.tabControl.SelectedIndex);
            Editor.Undo();
        }

        internal void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                Core.Controller.Main.SaveTab(Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem);
            }
        }

        internal void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.Filter = "Only Pawn File (*.pwn)|*.pwn|Include File (*.inc)|*.inc|All files (*.*)|*.*";
                saveFileDialog.Title = "Save PAWN File...";
                if (saveFileDialog.ShowDialog() == true)
                {
                    TabItem Tab = Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem;
                    Core.Controller.Main.SaveTab(Tab, saveFileDialog);
                    Tab.Header = saveFileDialog.SafeFileName;
                }
            }
        }

        internal void Close_File_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count == 0)
            {
                Core.Controller.Main.ToggleSaveOptions(false);

                Core.Controller.Main.Empty_Message.IsEnabled = true;
                Core.Controller.Main.Empty_Message.Visibility = Visibility.Visible;
            }
        }

        internal void Compiler_Path_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog CompilerPathDialog = new OpenFileDialog();
            CompilerPathDialog = new OpenFileDialog();
            CompilerPathDialog.Filter = "PAWN Compiler (pawncc.exe)|pawncc.exe";
            CompilerPathDialog.Title = "Set Compiler Path ...";
            CompilerPathDialog.InitialDirectory = Environment.CurrentDirectory;
            if (CompilerPathDialog.ShowDialog() == true)
            {
                Core.Properties.Settings.Default.CompilerPath = CompilerPathDialog.FileName;
                MessageBox.Show("Path set", "JAO PI");
                Core.Properties.Settings.Default.Save();
            }
        }

        internal void Cut_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ICSharpCode.AvalonEdit.TextEditor Editor = utility.GetTextEditor(Core.Controller.Main.tabControl.SelectedIndex);
            Editor.Cut();
        }

        internal void Copy_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ICSharpCode.AvalonEdit.TextEditor Editor = utility.GetTextEditor(Core.Controller.Main.tabControl.SelectedIndex);
            Editor.Copy();
        }

        internal void Paste_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ICSharpCode.AvalonEdit.TextEditor Editor = utility.GetTextEditor(Core.Controller.Main.tabControl.SelectedIndex);
            Editor.Paste();
        }

        internal void Compile_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Core.Controller.Worker.SaveWorker.RunWorkerAsync();
        }

        internal void Compile(object sender, ExecutedRoutedEventArgs e)
        {
            Core.Controller.Worker.SaveWorker.RunWorkerAsync();
        }

        internal void Create_File_Click(object sender, RoutedEventArgs e)
        {
            TabItem tab = generator.TabItem(Environment.CurrentDirectory, "new.pwn", null);

            Core.Controller.Main.tabControl.Items.Add(tab);
            Core.Controller.Main.tabControl.SelectedItem = tab;

            Core.Controller.Main.Empty_Message.Visibility = Visibility.Hidden;
            Core.Controller.Main.Empty_Message.IsEnabled = false;

            Core.Controller.Main.tabControl.Visibility = Visibility.Visible;

            if (Core.Controller.Main.tabControl.Items.Count == 1)
            {
                Core.Controller.Main.ToggleSaveOptions(true);
            }
        }


        internal void Open_File_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                TabItem tab = generator.TabItem(openFileDialog.FileName, openFileDialog.SafeFileName, File.ReadAllText(openFileDialog.FileName, System.Text.Encoding.Default));
                Core.Controller.Main.tabControl.Items.Add(tab);
                Core.Controller.Main.tabControl.SelectedItem = tab;

                Core.Controller.Main.Empty_Message.Visibility = Visibility.Hidden;
                Core.Controller.Main.Empty_Message.IsEnabled = false;

                Core.Controller.Main.tabControl.Visibility = Visibility.Visible;

                if (Core.Controller.Main.tabControl.Items.Count == 1)
                {
                    Core.Controller.Main.ToggleSaveOptions(true);
                }
            }
        }
    }
}
