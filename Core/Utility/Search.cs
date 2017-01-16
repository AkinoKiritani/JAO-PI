using ICSharpCode.AvalonEdit;
using JAO_PI.Core.Classes;
using System.Globalization;
using System.Text;
using System.Windows;

namespace JAO_PI.Core.Utility
{
    static class Search
    {
        public static Find FindString(TextEditor Editor, string SearchQuery, int lastIndex, bool IgnoreCase)
        {
            Find find = new Find();
            CompareInfo myComp = CultureInfo.InvariantCulture.CompareInfo;

            string Text = Editor.Text;
            if (SearchQuery.Length > 0 && Text.Length > 0)
            {
                find.Index = myComp.IndexOf(Text, SearchQuery, lastIndex, (IgnoreCase == true) ? CompareOptions.IgnoreCase : CompareOptions.None);
                find.Line = (find.Index == -1) ? -1 : Editor.TextArea.Document.GetLineByOffset(find.Index).LineNumber;
                return find;
            }
            return find = null;
        }

        public static bool SetSearchInfo(string text)
        {
            if (Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility == Visibility.Visible)
            {
                if (Controller.Search.SearchInfo.Visibility == Visibility.Collapsed)
                {
                    Controller.Search.SearchInfo.Visibility = Visibility.Visible;
                }
                Controller.Search.SearchInfo.Text = text;
                return true;
            }
            return false;
        }
        public static bool SetSearchInfo(StringBuilder text)
        {
            if (Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility == Visibility.Visible)
            {
                if (Controller.Search.SearchInfo.Visibility == Visibility.Collapsed)
                {
                    Controller.Search.SearchInfo.Visibility = Visibility.Visible;
                }
                Controller.Search.SearchInfo.Text = text.ToString();
                return true;
            }
            return false;
        }
    }
}
