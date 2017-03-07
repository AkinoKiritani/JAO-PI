using System;
using System.Windows;
using System.Windows.Controls;

namespace JAO_PI.EventsManager
{
    public class TabControl
    {
        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.TabControl Control = sender as System.Windows.Controls.TabControl;

            if (Control.SelectedIndex >= 0)
            {
                TabItem item = Control.SelectedItem as TabItem;
                Grid EditorGrid = item.Content as Grid;
                Core.Controller.Main.CurrentEditor = EditorGrid.Children[0] as ICSharpCode.AvalonEdit.TextEditor;
                
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Line].Content = Core.Properties.Resources.Line + ": " + Core.Controller.Main.CurrentEditor.TextArea.Caret.Line;
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Column].Content = Core.Properties.Resources.Column + ": " + Core.Controller.Main.CurrentEditor.TextArea.Caret.Column;
                
                Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.TabItem == item);
                if(Index != null)
                {
                    if(Index.Tmp == true)
                    {
                        Core.Controller.Main.CompileMenuItem.IsEnabled = false;
                    }
                    else
                    {
                        Core.Controller.Main.CompileMenuItem.IsEnabled = true;
                    }
                }
            }
            else
            {
                GC.ReRegisterForFinalize(Core.Controller.Main.CurrentEditor);
                GC.Collect();
                Core.Controller.Main.CurrentEditor = null;
            }
        }

        internal static void CloseFile(Core.Controller.Tab Index)
        {
            if(!Index.State.HasFlag(Structures.States.Saved))
            {
                MessageBoxResult result = MessageBox.Show(Core.Properties.Resources.CloseSave, Core.Properties.Resources.CloseSaveHeader, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    Core.Utility.Tab.SaveTab(Index.TabItem);
                }
            }

            if(Index.TabItem == Core.Controller.Main.CompiledTabItem)
            {
                Core.Utility.Tab.RemoveTempTabs();
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

            Index.Editor.Clear();
            Grid grid = Index.TabItem.Content as Grid;
            Index.Editor = null;
            grid.Children.Remove(Index.Editor);
            grid = null;
            Core.Controller.Main.TabControlList.Remove(Index);

            Index.TabItem.ContextMenu.Items.Clear();
            Core.Controller.Main.tabControl.Items.Remove(Index.TabItem);
            if (Core.Controller.Main.tabControl.Items.Count == 0)
            {
                Core.Utility.Toggle.TabControl(false);
                Core.Utility.Toggle.SaveOptions(false);
                Core.Controller.Main.CompileMenuItem.IsEnabled = false;
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Line].Visibility = Visibility.Collapsed;
                Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Column].Visibility = Visibility.Collapsed;
            }
            GC.ReRegisterForFinalize(Index);
            GC.Collect();
        }
    }
}
