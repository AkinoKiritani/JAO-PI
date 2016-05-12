using ICSharpCode.AvalonEdit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JAO_PI.Core.Classes
{
    class Generator
    {
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
            menu.Items.Add(new MenuItem() { Header = "Close" });
            menu.Items.Add(new MenuItem() { Header = "Rename" });
            tab.ContextMenu = menu;
            return tab;
        }
    }
}
