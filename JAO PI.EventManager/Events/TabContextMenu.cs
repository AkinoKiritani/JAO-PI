using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    public class TabContextMenu
    {
        public void CloseItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            Controller.Tab Index = Controller.Main.TabControlList.Find(x => x.Close.Uid == item.Uid);

            Index.Editor.Clear();
            Grid grid = Index.TabItem.Content as Grid;
            Index.Editor = null;
            grid.Children.Remove(Index.Editor);
            grid = null;
            Controller.Main.TabControlList.Remove(Index);

            Controller.Main.tabControl.Items.Remove(Index.TabItem);
            if (Controller.Main.tabControl.Items.Count == 0)
            {
                Controller.Main.tabControl.Visibility = Visibility.Hidden;

                Controller.Main.Empty_Message.IsEnabled = true;
                Controller.Main.Empty_Message.Visibility = Visibility.Visible;
            }
        }

        public void RenameItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Rename clicked");
        }

        public void SaveItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Save clicked");
        }
    }
}
