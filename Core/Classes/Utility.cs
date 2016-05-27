using ICSharpCode.AvalonEdit;
using System;
using System.Linq;
using System.Windows.Controls;

namespace JAO_PI.Core.Classes
{
    class Utility
    {
        public TextEditor GetTextEditor(int index)
        {
            TabItem item = Controller.Main.tabControl.Items[index] as TabItem;
            Grid EditorGrid = item.Content as Grid;
            return EditorGrid.Children[0] as TextEditor;
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcefghijklmnopqrstuvw";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public MenuItem Menuitem(string Header, string Uid)
        {
            MenuItem Item = new MenuItem();
            Item.Header = Header;
            Item.Uid = Uid;
            return Item;
        }
    }
}
