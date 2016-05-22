using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace JAO_PI.Core.Classes
{
    class EventHandler
    {
        internal void Restore_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Restore clicked");
        }

        internal void Cut_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Cut clicked");
        }

        internal void Copy_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Copy clicked");
        }

        internal void Paste_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Paste clicked");
        }

        internal void CloseItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
