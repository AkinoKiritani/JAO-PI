using JAO_PI.Core.Classes;
using JAO_PI.Core.Utility;
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
        public void Create_File_Click(object sender, RoutedEventArgs e)
        {
            Generator generator = new Generator();
            TabItem tab = generator.TabItem(Environment.CurrentDirectory, "new.pwn", null);

            Core.Controller.Main.tabControl.Items.Add(tab);
            Core.Controller.Main.tabControl.SelectedItem = tab;

            Toggle.TabControl(true);

            if (Core.Controller.Main.tabControl.Items.Count == 1)
            {
                Functions utility = new Functions();
                Toggle.SaveOptions(true);
                Core.Controller.Main.CompileMenuItem.IsEnabled = true;
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Line].Visibility = Visibility.Visible;
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Column].Visibility = Visibility.Visible;
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Line].Content = Core.Properties.Resources.Line + ": 0";
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Column].Content = Core.Properties.Resources.Column + ": 1";
            }
        }
        public void Open_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Core.Properties.Resources.FileFilter;
            openFileDialog.Title = Core.Properties.Resources.OpenFile;
            if (openFileDialog.ShowDialog() == true)
            {
                Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => (x.TabItem.Uid + x.TabItem.Header) == openFileDialog.FileName);
                if(Index != null)
                {
                    return;
                }

                FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                Generator generator = new Generator();
                TabItem tab = generator.TabItem(openFileDialog.FileName, openFileDialog.SafeFileName, stream);

                Core.Controller.Main.tabControl.Items.Add(tab);
                Core.Controller.Main.tabControl.SelectedItem = tab;

                Toggle.TabControl(true);

                if (Core.Controller.Main.tabControl.Items.Count == 1)
                {
                    Functions utility = new Functions();
                    Toggle.SaveOptions(true);
                    Core.Controller.Main.CompileMenuItem.IsEnabled = true;
                    Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Line].Visibility   = Visibility.Visible;
                    Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Column].Visibility = Visibility.Visible;
                    Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Line].Content      = Core.Properties.Resources.Line + ": 0";
                    Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Column].Content    = Core.Properties.Resources.Column + ": 1";
                }
                stream.Dispose();
            }
            GC.ReRegisterForFinalize(openFileDialog);
        }

        public void Close_All_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0)
            {
                Core.Controller.Worker.CloseAllWorker.RunWorkerAsync();
            }
        }

        public void Close_File_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0)
            {
                TabControl.CloseFile(Core.Controller.Main.TabControlList.Find(x => x.Editor.Uid == Core.Controller.Main.CurrentEditor.Uid));
            }
        }
        public void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                Functions utility = new Functions();
                utility.SaveTab(Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem);
            }
        }
        public void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.Filter = Core.Properties.Resources.FileFilter;
                saveFileDialog.Title = Core.Properties.Resources.SaveFile;
                if (saveFileDialog.ShowDialog() == true)
                {
                    TabItem Tab = Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem;
                    Functions utility = new Functions();
                    utility.SaveTab(Tab, saveFileDialog);
                    Tab.Header = saveFileDialog.SafeFileName;
                }
            }
        }
        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.MainFrame].Close();
        }
        public void Undo_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.CurrentEditor.Undo();
        }
        public void Cut_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.CurrentEditor.Cut();
        }
        public void Copy_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.CurrentEditor.Copy();
        }
        public void Paste_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.CurrentEditor.Paste();
        }
        public void Find_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 &&
                Core.Controller.Main.tabControl.Visibility == Visibility.Visible &&
                Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility == Visibility.Collapsed)
            {
                if (Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility == Visibility.Collapsed)
                {
                    Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Visible;
                }
            }
        }
        public void Search(object sender, ExecutedRoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 &&
                Core.Controller.Main.tabControl.Visibility == Visibility.Visible &&
                Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility == Visibility.Collapsed)
            {
                if (Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility == Visibility.Collapsed)
                {
                    Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Visible;
                }
            }
        }
        public void FindNext(object sender, ExecutedRoutedEventArgs e)
        {
            Core.Controller.Main.CurrentSearchIndex++;
            if (Core.Controller.Main.CurrentSearchIndex < Find.SearchIndex.Count)
            {
                Core.Controller.Main.CurrentEditor.ScrollToLine(Core.Controller.Main.CurrentEditor.TextArea.Document.GetLineByOffset(Find.SearchIndex[Core.Controller.Main.CurrentSearchIndex]).LineNumber);
                Core.Controller.Main.CurrentEditor.Select((Find.SearchIndex[Core.Controller.Main.CurrentSearchIndex] - (Core.Controller.Main.CurrentSearch.Length + 1)), Core.Controller.Main.CurrentSearch.Length);
            }
            else
            {
                MessageBox.Show(Core.Properties.Resources.NoFurtherResult, Core.Properties.Resources.ProgName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void Compile_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Worker.SaveWorker.RunWorkerAsync(); // Will trigger the Compilerworker
        }
        public void Compile(object sender, RoutedEventArgs e)
        {
            Core.Controller.Worker.SaveWorker.RunWorkerAsync(); // Will trigger the Compilerworker
        }
        public void Compiler_Path_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog CompilerPathDialog = new OpenFileDialog();
            CompilerPathDialog = new OpenFileDialog();
            CompilerPathDialog.Filter = Core.Properties.Resources.PathFilter;
            CompilerPathDialog.Title = Core.Properties.Resources.SetPath;
            CompilerPathDialog.InitialDirectory = Environment.CurrentDirectory;
            if (CompilerPathDialog.ShowDialog() == true)
            {
                Core.Properties.Settings.Default.CompilerPath = CompilerPathDialog.FileName;
                MessageBox.Show(Core.Properties.Resources.PathSet, Core.Properties.Resources.ProgName, MessageBoxButton.OK, MessageBoxImage.Information);
                Core.Properties.Settings.Default.Save();
            }
        }
        public void GoTo(object sender, ExecutedRoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 &&
                Core.Controller.Main.tabControl.Visibility == Visibility.Visible &&
                Core.Controller.Main.Frames[(int)Structures.Frames.GoToFrame].Visibility == Visibility.Collapsed)
            {
                if (Core.Controller.Main.Frames[(int)Structures.Frames.GoToFrame].Visibility == Visibility.Collapsed)
                {
                    Core.Controller.Main.Frames[(int)Structures.Frames.GoToFrame].Visibility = Visibility.Visible;
                }
            }
        }
        public void GoTo_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 &&
                Core.Controller.Main.tabControl.Visibility == Visibility.Visible &&
                Core.Controller.Main.Frames[(int)Structures.Frames.GoToFrame].Visibility == Visibility.Collapsed)
            {
                if (Core.Controller.Main.Frames[(int)Structures.Frames.GoToFrame].Visibility == Visibility.Collapsed)
                {
                    Core.Controller.Main.Frames[(int)Structures.Frames.GoToFrame].Visibility = Visibility.Visible;
                }
            }
        }
    }
}
