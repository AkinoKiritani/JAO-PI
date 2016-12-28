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
            Core.Utility.Functions utility = new Core.Utility.Functions();
            if(Index.Editor.Document.FileName.Contains(".JAOnotsaved"))
            {
                MessageBoxResult result = MessageBox.Show(Core.Properties.Resources.CloseSave, Core.Properties.Resources.CloseSaveHeader, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
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
                Core.Controller.Main.tabControl.Visibility = System.Windows.Visibility.Collapsed;

                Core.Controller.Main.Empty_Message.IsEnabled = true;
                Core.Controller.Main.Empty_Message.Visibility = System.Windows.Visibility.Visible;
                Core.Controller.Main.EditItem.IsEnabled = false;
                utility.ToggleSaveOptions(false);
                Core.Controller.Main.CompileMenuItem.IsEnabled = false;
                Core.Controller.Main.StatusBarItems[(int)Core.Utility.Structures.StatusBar.Line].Visibility = System.Windows.Visibility.Collapsed;
                Core.Controller.Main.StatusBarItems[(int)Core.Utility.Structures.StatusBar.Column].Visibility = System.Windows.Visibility.Collapsed;
            }
            GC.ReRegisterForFinalize(Index);
            GC.Collect();
        }
    }
}
