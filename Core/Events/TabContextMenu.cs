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
            MenuItem ReaameItem = sender as MenuItem;
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.Rename.Uid == ReaameItem.Uid);
        }

        internal void SaveItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem SaveItem = sender as MenuItem;
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.Save.Uid == SaveItem.Uid);
            Core.Utility.Functions utility = new Core.Utility.Functions();
            utility.SaveTab(Index.TabItem);
        }
    }
}
