using Microsoft.Win32;
using System.IO;
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
            MenuItem RenameItem = sender as MenuItem;
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.Rename.Uid == RenameItem.Uid);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = Core.Properties.Resources.FileFilter;
            saveFileDialog.Title = Core.Properties.Resources.SaveFile;
            saveFileDialog.FileName = Index.TabItem.Header.ToString();
            if (saveFileDialog.ShowDialog() == true)
            {
                System.Text.StringBuilder FileToSave = new System.Text.StringBuilder();
                FileToSave.Append(Index.TabItem.Uid);
                FileToSave.Append(Index.TabItem.Header);
                string file = FileToSave.ToString();
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                Core.Utility.Functions utility = new Core.Utility.Functions();
                utility.SaveTab(Index.TabItem, saveFileDialog);
                Index.TabItem.Header = saveFileDialog.SafeFileName;
            }
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
