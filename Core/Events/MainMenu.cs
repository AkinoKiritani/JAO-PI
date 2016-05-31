using ICSharpCode.AvalonEdit;
using JAO_PI.Core.Classes;
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
        Generator generator = null;
        Utility utility = null;
        
        public MainMenu()
        {
            generator = new Generator();
            utility = new Utility();
        }

        public void Undo_Click(object sender, RoutedEventArgs e)
        {
            TextEditor Editor = utility.GetTextEditor(Core.Controller.Main.tabControl.SelectedIndex);
            Editor.Undo();
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                Core.Controller.Main.SaveTab(Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem);
            }
        }

        public void SaveAs_Click(object sender, RoutedEventArgs e)
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

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Utility.Frames.MainFrame].Close();
        }

        public void FindNext(object sender, ExecutedRoutedEventArgs e)
        {
            Core.Controller.Main.CurrentSearchIndex++;
            if (Core.Controller.Main.CurrentSearchIndex < Find.SearchIndex.Count)
            {
                TextEditor Editor = utility.GetTextEditor(Core.Controller.Main.tabControl.SelectedIndex);
                Editor.ScrollToLine(Editor.TextArea.Document.GetLineByOffset(Find.SearchIndex[Core.Controller.Main.CurrentSearchIndex]).LineNumber);
                Editor.Select((Find.SearchIndex[Core.Controller.Main.CurrentSearchIndex] - (Core.Controller.Main.CurrentSearch.Length + 1)), Core.Controller.Main.CurrentSearch.Length);
            }
            else
            {
                MessageBox.Show("No further results", "JAO PI", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void Search(object sender, ExecutedRoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                Core.Controller.Main.Frames[(int)Utility.Frames.SearchFrame].Show();
            }
        }

        public void Find_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                Core.Controller.Main.Frames[(int)Utility.Frames.SearchFrame].Show();
            }
        }

        public void Close_File_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0)
            {
                Core.Controller.Main.ToggleSaveOptions(false);

                Core.Controller.Main.Empty_Message.IsEnabled = true;
                Core.Controller.Main.Empty_Message.Visibility = Visibility.Visible;

                TextEditor Editor = utility.GetTextEditor(Core.Controller.Main.tabControl.SelectedIndex);

                Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.Editor.Uid == Editor.Uid);
                Index.Editor.Clear();
                Grid grid = Index.TabItem.Content as Grid;
                Index.Editor = null;
                grid.Children.Remove(Index.Editor);
                grid = null;
                Core.Controller.Main.TabControlList.Remove(Index);

                Core.Controller.Main.tabControl.Items.Remove(Index.TabItem);
                if (Core.Controller.Main.tabControl.Items.Count == 0)
                {
                    Core.Controller.Main.tabControl.Visibility = Visibility.Hidden;

                    Core.Controller.Main.Empty_Message.IsEnabled = true;
                    Core.Controller.Main.Empty_Message.Visibility = Visibility.Visible;
                    Core.Controller.Main.EditItem.IsEnabled = false;
                }
            }
        }

        public void Compiler_Path_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog CompilerPathDialog = new OpenFileDialog();
            CompilerPathDialog = new OpenFileDialog();
            CompilerPathDialog.Filter = "PAWN Compiler (pawncc.exe)|pawncc.exe";
            CompilerPathDialog.Title = "Set Compiler Path ...";
            CompilerPathDialog.InitialDirectory = Environment.CurrentDirectory;
            if (CompilerPathDialog.ShowDialog() == true)
            {
                Core.Properties.Settings.Default.CompilerPath = CompilerPathDialog.FileName;
                MessageBox.Show("Path set", "JAO PI", MessageBoxButton.OK, MessageBoxImage.Information);
                Core.Properties.Settings.Default.Save();
            }
        }

        public void Cut_Click(object sender, RoutedEventArgs e)
        {
            TextEditor Editor = utility.GetTextEditor(Core.Controller.Main.tabControl.SelectedIndex);
            Editor.Cut();
        }

        public void Copy_Click(object sender, RoutedEventArgs e)
        {
            TextEditor Editor = utility.GetTextEditor(Core.Controller.Main.tabControl.SelectedIndex);
            Editor.Copy();
        }

        public void Paste_Click(object sender, RoutedEventArgs e)
        {
            TextEditor Editor = utility.GetTextEditor(Core.Controller.Main.tabControl.SelectedIndex);
            Editor.Paste();
        }

        public void Compile_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Worker.SaveWorker.RunWorkerAsync();
        }

        public void Compile(object sender, RoutedEventArgs e)
        {
            Core.Controller.Worker.SaveWorker.RunWorkerAsync();
        }

        public void Create_File_Click(object sender, RoutedEventArgs e)
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

        public void Open_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PAWN Files (*.inc, *.pwn)|*.inc;*.pwn|Include Files (*.inc)|*.inc|Only Pawn Files (*.pwn)|*.pwn|All files (*.*)|*.*";
            openFileDialog.Title = "Open PAWN File...";
            if (openFileDialog.ShowDialog() == true)
            {
                TabItem tab = generator.TabItem(openFileDialog.FileName, openFileDialog.SafeFileName, File.ReadAllText(openFileDialog.FileName, System.Text.Encoding.UTF8));
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
            GC.ReRegisterForFinalize(openFileDialog);
        }
    }
}
