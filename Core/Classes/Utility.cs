using ICSharpCode.AvalonEdit;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace JAO_PI.Core.Classes
{
    class Utility
    {
        public enum Frames
        {
            MainFrame,
            SearchFrame
        }

        public TextEditor GetTextEditor(int index)
        {
            TabItem item = Controller.Main.tabControl.Items[index] as TabItem;
            Grid EditorGrid = item.Content as Grid;
            return EditorGrid.Children[0] as TextEditor;
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcefghijklmnopqrstuvwxyz";
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

        public Find FindString(TextEditor Editor, string SearchQuery, int lastIndex, bool IgnoreCase)
        {
            Find find = new Find();
            CompareInfo myComp = CultureInfo.InvariantCulture.CompareInfo;

            string Text = Editor.Text;
            find.Index = myComp.IndexOf(Text, SearchQuery, lastIndex, (IgnoreCase == true) ? CompareOptions.IgnoreCase: CompareOptions.None);
            find.Line = (find.Index == -1) ? -1 : Editor.TextArea.Document.GetLineByOffset(find.Index).LineNumber;
            return find;
        }
    }
}
