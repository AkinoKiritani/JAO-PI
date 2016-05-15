﻿using ICSharpCode.AvalonEdit;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Windows.Input;

namespace JAO_PI.Core.Classes
{
    class Generator
    {
        List<TabController> kek = new List<TabController>();
        public TabItem TabItem(string header, string content)
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

            ContextMenu menu = new ContextMenu();

            MenuItem CloseItem = Menuitem("Close", MainController.RandomString(10));
            menu.Items.Add(CloseItem);

            MenuItem RenameItem = Menuitem("Rename", MainController.RandomString(10));
            menu.Items.Add(RenameItem);

            tab.ContextMenu = menu;
            kek.Add(new TabController
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
            TabController k = kek.Find(x => x.Close.Uid == item.Uid);

            k.Editor.Clear();
            Grid grid = k.TabItem.Content as Grid;
            grid.Children.Remove(k.Editor);

            MainController.tabControl.Items.Remove(k.TabItem);
            if(MainController.tabControl.Items.Count == 0) MainController.tabControl.Visibility = Visibility.Hidden;
        }
    }
}