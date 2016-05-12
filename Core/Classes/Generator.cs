using ICSharpCode.AvalonEdit;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JAO_PI.Core.Classes
{
    class Generator
    {
        List<MenuItem> contextMenu = null;

        public Generator()
        {
            contextMenu = new List<MenuItem>();
            contextMenu.Add(new MenuItem() { Header = "Close" });
            contextMenu.Add(new MenuItem() { Header = "Rename" });
        }

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
            for(int i = 0; i != contextMenu.Count; i++)
            {
                menu.Items.Add(contextMenu[i]);
            }
            tab.ContextMenu = menu;
            return tab;
        }
    }
}