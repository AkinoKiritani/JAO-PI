using ICSharpCode.AvalonEdit;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace JAO_PI.Core.Classes
{
    class Generator
    {
        List<TabController> TabControlList = new List<TabController>();
        public TabItem TabItem(string path, string header, string content)
        {

            TextEditor Editor = new TextEditor();
            Editor.FontSize = 13;
            Editor.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            Editor.FontFamily = new FontFamily("Consolas");
            Editor.ShowLineNumbers = true;
            Editor.Text = content;
            Editor.Margin = new Thickness(0, 0, 5, 0);

            Grid grid = new Grid();
            grid.Children.Add(Editor);
            
            TabItem tab = new TabItem();
            tab.Header = header;
            tab.Content = grid;
            
            if (path.Contains(header) == true)
            {
                path = path.Remove(path.Length - header.Length, header.Length);
            }
            tab.Uid = path;

            ContextMenu menu = new ContextMenu();

            MenuItem CloseItem = Menuitem("Close", MainController.RandomString(10));
            menu.Items.Add(CloseItem);

            MenuItem RenameItem = Menuitem("Rename", MainController.RandomString(10));
            menu.Items.Add(RenameItem);

            tab.ContextMenu = menu;
            TabControlList.Add(new TabController
            {
                TabItem = tab,
                Editor = Editor,
                Close = CloseItem,
                Rename = RenameItem                
            });
            return tab;
        }

        private MenuItem Menuitem(string Header, string Uid)
        {
            MenuItem Item = new MenuItem();
            Item.Header = Header;
            Item.Uid = Uid;
            Item.PreviewMouseLeftButtonUp += Item_PreviewMouseLeftButtonUp;
            return Item;
        }

        private void Item_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            TabController Index = TabControlList.Find(x => x.Close.Uid == item.Uid);

            Index.Editor.Clear();
            Grid grid = Index.TabItem.Content as Grid;
            Index.Editor = null;
            grid.Children.Remove(Index.Editor);
            grid = null;
            TabControlList.Remove(Index);

            MainController.tabControl.Items.Remove(Index.TabItem);
            if (MainController.tabControl.Items.Count == 0)
            {
                MainController.tabControl.Visibility = Visibility.Hidden;

                MainController.Empty_Message.IsEnabled = true;
                MainController.Empty_Message.Visibility = Visibility.Visible;
            }
        }
    }
}