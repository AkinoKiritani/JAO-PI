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
                TabItem item = Core.Controller.Main.tabControl.Items[Control.SelectedIndex] as TabItem;
                Grid EditorGrid = item.Content as Grid;
                Core.Controller.Main.CurrentEditor = EditorGrid.Children[0] as ICSharpCode.AvalonEdit.TextEditor;
                
                Core.Controller.Main.StatusBarItems[(int)Core.Utility.Structures.StatusBar.Line].Content = Core.Properties.Resources.Line + ": " + Core.Controller.Main.CurrentEditor.TextArea.Caret.Line;
                Core.Controller.Main.StatusBarItems[(int)Core.Utility.Structures.StatusBar.Column].Content = Core.Properties.Resources.Column + ": " + Core.Controller.Main.CurrentEditor.TextArea.Caret.Column;
            }
            else
            {
                GC.ReRegisterForFinalize(Core.Controller.Main.CurrentEditor);
                GC.Collect();
                Core.Controller.Main.CurrentEditor = null;
            }
        }

        internal static void CloseAllFiles()
        {
            while(Core.Controller.Main.TabControlList.Count > 0)
            {
                CloseFile(Core.Controller.Main.TabControlList[0]);
            }
        }

        internal static void CloseFile(Core.Controller.Tab Index)
        {
            if(Index.Editor.Document.FileName.Contains(".JAOnotsaved"))
            {
                MessageBoxResult result = MessageBox.Show(Core.Properties.Resources.CloseSave, Core.Properties.Resources.CloseSaveHeader, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    Core.Utility.Functions utility = new Core.Utility.Functions();
                    utility.SaveTab(Index.TabItem);
                }
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
                Core.Controller.Main.StatusBarItems[(int)Core.Utility.Structures.StatusBar.Line].Visibility = Visibility.Collapsed;
                Core.Controller.Main.StatusBarItems[(int)Core.Utility.Structures.StatusBar.Column].Visibility = Visibility.Collapsed;
            }
            GC.ReRegisterForFinalize(Index);
            GC.Collect();
        }
    }
}
