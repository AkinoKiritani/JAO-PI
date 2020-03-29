using JAO_PI.Core.Classes;
using JAO_PI.Core.Utility;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    public class MainMenu
    {
        public void CreateFileClick(object sender, RoutedEventArgs e)
        {
            Generator generator = new Generator();
            TabItem tab = generator.TabItem(Environment.CurrentDirectory, Core.Properties.Resources.NewFileName, null);

            Core.Controller.Main.tabControl.Items.Add(tab);
            Core.Controller.Main.tabControl.SelectedItem = tab;

            Toggle.TabControl(true);

            if (Core.Controller.Main.tabControl.Items.Count == 1)
            {
                Toggle.SaveOptions(true);
                Core.Controller.Main.CompileMenuItem.IsEnabled = true;
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Line].Visibility = Visibility.Visible;
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Column].Visibility = Visibility.Visible;
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Line].Content = Core.Properties.Resources.Line + ": 0";
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Column].Content = Core.Properties.Resources.Column + ": 1";
            }
        }
        public void OpenFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = Core.Properties.Resources.FileFilter,
                Title = Core.Properties.Resources.OpenFile
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => (x.TabItem.Uid + Tab.GetTabHeaderText(x.TabItem)) == openFileDialog.FileName);
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

        public void CloseAllClick(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0)
            {
                Tab.RemoveTempTabs();
                Core.Controller.Worker.CloseAllWorker.RunWorkerAsync();
            }
        }

        public void Replace(object sender, ExecutedRoutedEventArgs e)
        {
            Main.SelectAndOpenSearchTab(Structures.SearchControl.Replace);
        }

        public void CloseFileClick(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0)
            {
                TabControl.CloseFile(Core.Controller.Main.TabControlList.Find(x => x.Editor.Uid == Core.Controller.Main.CurrentEditor.Uid));
            }
        }

        public void AboutClick(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.CreditsFrame].Visibility = Visibility.Visible;
        }

        public void SaveClick(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                Tab.SaveTab(Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem);
            }
        }

        public void CompilerCloseClick(object sender, MouseButtonEventArgs e)
        {
            Tab.RemoveTempTabs();
            Core.Controller.Main.tabControl.SelectedItem = Core.Controller.Main.CompiledTabItem;
            Core.Controller.Main.CompiledTabItem = null;

            Core.Controller.Main.CompilerPanel.Visibility = Visibility.Collapsed;

            Core.Controller.Main.PanelHeight = Core.Controller.Main.MainView.RowDefinitions[(int)Structures.MainView.CompilerPanel];
            Core.Controller.Main.MainView.RowDefinitions[(int)Structures.MainView.CompilerPanel] = new RowDefinition()
            {
                Height = new GridLength(0),
                MinHeight = 0
            };
            Core.Controller.Main.MainView.RowDefinitions[(int)Structures.MainView.GridSplitter].Height = new GridLength(0);

            GridSplitter Splitter = Core.Controller.Main.MainView.Children[(int)Structures.MainView.GridSplitter] as GridSplitter;
            Splitter.Visibility = Visibility.Collapsed;
            Splitter.IsEnabled = false;
        }

        public void AnalyseClick(object sender, RoutedEventArgs e)
        {
            var file = @"file.inc";

            var checksum = Data.Utility.GetFileChecksum(file);
            if (!string.IsNullOrEmpty(checksum))
            {
                var dic = new Dictionary<string, string>();
                Data.Parser.Analysis(file, dic);
            }
        }

        public void SaveAsClick(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    OverwritePrompt = true,
                    Filter = Core.Properties.Resources.FileFilter,
                    Title = Core.Properties.Resources.SaveFile
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    TabItem Tab = Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem;
                    Core.Utility.Tab.SaveTab(Tab, saveFileDialog);
                    Core.Utility.Tab.UpdateTabHeaderText(Tab, saveFileDialog.SafeFileName);
                }
            }
        }
        public void ExitClick(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.MainFrame].Close();
        }
        public void UndoClick(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.CurrentEditor.Undo();
        }
        public void CutClick(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.CurrentEditor.Cut();
        }
        public void CopyClick(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.CurrentEditor.Copy();
        }
        public void PasteClick(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.CurrentEditor.Paste();
        }
        public void FindClick(object sender, RoutedEventArgs e)
        {
            Main.SelectAndOpenSearchTab(Structures.SearchControl.Search);
        }
        public void Search(object sender, ExecutedRoutedEventArgs e)
        {
            Main.SelectAndOpenSearchTab(Structures.SearchControl.Search);
        }
        public void FindNext(object sender, ExecutedRoutedEventArgs e)
        {
            Core.Controller.Search.CurrentSearchIndex++;

            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.TabItem == (Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem));
            if (Index != null)
            {
                if (Core.Controller.Search.CurrentSearchIndex < Index.SearchList.Count)
                {
                    Core.Utility.Editor.SelectAndBringToView(Index.Editor, Index.SearchList[Core.Controller.Search.CurrentSearchIndex].Index, Core.Controller.Search.CurrentSearch.Length);
                }
                else
                {
                    MessageBox.Show(Core.Properties.Resources.NoFurtherResult, Core.Properties.Resources.ProgName, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        public void CompileClick(object sender, RoutedEventArgs e)
        {
            Core.Controller.Worker.SaveWorker.RunWorkerAsync(); // Will trigger the Compilerworker
        }
        public void Compile(object sender, RoutedEventArgs e)
        {
            Core.Controller.Worker.SaveWorker.RunWorkerAsync(); // Will trigger the Compilerworker
        }
        public void CompilerPathClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog CompilerPathDialog = new OpenFileDialog()
            {
                Filter = Core.Properties.Resources.PathFilter,
                Title = Core.Properties.Resources.SetPath,
                InitialDirectory = Environment.CurrentDirectory
            };
            if (CompilerPathDialog.ShowDialog() == true)
            {
                Core.Properties.Settings.Default.CompilerPath = CompilerPathDialog.FileName;
                MessageBox.Show(Core.Properties.Resources.PathSet, Core.Properties.Resources.ProgName, MessageBoxButton.OK, MessageBoxImage.Information);
                Core.Properties.Settings.Default.Save();
            }
        }
        public void GoTo(object sender, ExecutedRoutedEventArgs e)
        {
            Main.SelectAndOpenSearchTab(Structures.SearchControl.GoTo);
        }
        public void GoToClick(object sender, RoutedEventArgs e)
        {
            Main.SelectAndOpenSearchTab(Structures.SearchControl.GoTo);
        }
    }
}
