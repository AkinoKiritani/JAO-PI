using ICSharpCode.AvalonEdit;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace JAO_PI.Core.Utility
{
    static class Search
    {
        public static Controller.Find FindString(TextEditor Editor, string SearchQuery, int lastIndex, bool IgnoreCase)
        {
            Controller.Find find = new Controller.Find();
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

        public static List<Controller.Find> FindString(TextEditor Editor, string SearchQuery, bool IgnoreCase)
        {
            Controller.Find find = new Controller.Find();
            int lastIndex = 0;
            CompareInfo myComp = CultureInfo.InvariantCulture.CompareInfo;

            List<Controller.Find> curSearch = new List<Controller.Find>();

            string Text = Editor.Text;
            if (SearchQuery.Length > 0 && Text.Length > 0)
            {
                while ((find.Index = myComp.IndexOf(Text, SearchQuery, lastIndex, (IgnoreCase == true) ? CompareOptions.IgnoreCase : CompareOptions.None)) != -1)
                {
                    lastIndex = find.Index + SearchQuery.Length;
                    find.Line = (find.Index == -1) ? -1 : Editor.TextArea.Document.GetLineByOffset(find.Index).LineNumber;
                    curSearch.Add(new Controller.Find()
                    {
                        Index = find.Index,
                        Line = find.Line
                    });
                }
                return curSearch;
            }
            return curSearch = null;
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

        public static void DoSearch(Controller.Tab Index, TextBox SearchBox)
        {
            Index.State |= Structures.States.Searching;
            if (SearchBox != null)
            {
                if (Controller.Search.CurrentSearch != null && Controller.Search.CurrentSearch.Equals(SearchBox.Text) == true)
                {
                    Controller.Search.CurrentSearchIndex++;
                    if (Index.SearchList != null && Controller.Search.CurrentSearchIndex < Index.SearchList.Count)
                    {
                        Index.Editor.ScrollToLine(Index.Editor.TextArea.Document.GetLineByOffset(Index.SearchList[Controller.Search.CurrentSearchIndex].Index).LineNumber);
                        Index.Editor.Select(Index.SearchList[Controller.Search.CurrentSearchIndex].Index, Controller.Search.CurrentSearch.Length);
                    }
                    else
                    {
                        SetSearchInfo(Properties.Resources.NoFurtherResult);
                    }
                }
                else
                {
                    if (Index.SearchList != null && Index.SearchList.Count > 0)
                    {
                        Index.SearchList.Clear();
                    }

                    Controller.Search.CurrentSearchIndex = 0;
                    Controller.Main.LastIndex = 0;
                    Controller.Search.CurrentSearch = SearchBox.Text;

                    if (Controller.Search.CurrentSearch != null)
                    {
                        Index.SearchList = FindString(Index.Editor, Controller.Search.CurrentSearch, !(Controller.Search.MatchCase.IsChecked.Value));
                        if (Index.SearchList != null && Index.SearchList.Count > 0)
                        {
                            Index.Editor.ScrollToLine(Index.Editor.TextArea.Document.GetLineByOffset(Index.SearchList[0].Index).LineNumber);
                            Index.Editor.Select(Index.SearchList[0].Index, Controller.Search.CurrentSearch.Length);

                            StringBuilder ResultText = new StringBuilder();
                            ResultText.Append(Properties.Resources.Result);
                            ResultText.Append(Index.SearchList.Count);

                            SetSearchInfo(ResultText);
                        }
                        else
                        {
                            SetSearchInfo(Properties.Resources.NoResult);
                        }
                    }
                }
            }
            Index.State &= ~Structures.States.Searching;
        }
        public static void DoCount(Controller.Tab Index, TextBox SearchBox)
        {
            if (SearchBox != null)
            {
                if (Controller.Search.CurrentSearch != null && Controller.Search.CurrentSearch.Equals(SearchBox.Text) == true)
                {
                    StringBuilder ResultText = new StringBuilder();
                    ResultText.Append(Properties.Resources.Result);
                    ResultText.Append(Index.SearchList.Count);

                    SetSearchInfo(ResultText);
                    return;
                }
                else
                {
                    if (Index.SearchList.Count > 0)
                    {
                        Index.SearchList.Clear();
                        Controller.Search.CurrentSearchIndex = 0;
                        Controller.Main.LastIndex = 1;
                    }
                    Controller.Search.CurrentSearch = SearchBox.Text;
                    if (Index.SearchList != null && Controller.Search.CurrentSearch != null)
                    {
                        Index.SearchList = FindString(Index.Editor, Controller.Search.CurrentSearch, !(Controller.Search.MatchCase.IsChecked.Value));
                        if (Index.SearchList != null && Index.SearchList.Count > 0)
                        {
                            if (Controller.Search.SearchInfo.Visibility == Visibility.Collapsed)
                            {
                                Controller.Search.SearchInfo.Visibility = Visibility.Visible;
                            }
                            StringBuilder ResultText = new StringBuilder();
                            ResultText.Append(Properties.Resources.Result);
                            ResultText.Append(Index.SearchList.Count);

                            SetSearchInfo(ResultText);
                        }
                    }
                }
            }
        }
    }
}
