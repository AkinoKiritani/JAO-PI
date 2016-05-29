using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    public class TabContextMenu
    {
        internal void CloseItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.Close.Uid == item.Uid);

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

        internal void RenameItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Rename clicked");
        }

        internal void SaveItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Save clicked");
        }
    }
}
