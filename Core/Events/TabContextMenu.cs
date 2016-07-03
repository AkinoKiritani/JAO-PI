using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    public class TabContextMenu
    {
        internal void CloseItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem CloseItem = sender as MenuItem;
            TabControl.CloseFile(Core.Controller.Main.TabControlList.Find(x => x.Close.Uid == CloseItem.Uid));          
        }

        internal void RenameItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Rename clicked");
        }

        internal void SaveItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem SaveItem = sender as MenuItem;
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.Save.Uid == SaveItem.Uid);
            Core.Classes.Utility utility = new Core.Classes.Utility();
            utility.SaveTab(Index.TabItem);
        }
    }
}
