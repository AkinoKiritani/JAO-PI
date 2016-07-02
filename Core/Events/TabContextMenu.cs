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
            TabControl.CloseFile(Core.Controller.Main.TabControlList.Find(x => x.Close.Uid == item.Uid));          
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
