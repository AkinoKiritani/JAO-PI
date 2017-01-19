using ICSharpCode.AvalonEdit;
using JAO_PI.Core.Classes;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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

        public static void DoSearch(Controller.Tab Index, TextBox SearchBox)
        {
            if (Controller.Search.SearchBox != null)
            {
                if (Controller.Search.CurrentSearch != null && Controller.Search.CurrentSearch.Equals(Controller.Search.SearchBox.Text) == true)
                {
                    Controller.Search.CurrentSearchIndex++;
                    if (Controller.Search.CurrentSearchIndex < Find.SearchIndex.Count)
                    {
                        Index.Editor.ScrollToLine(Index.Editor.TextArea.Document.GetLineByOffset(Find.SearchIndex[Controller.Search.CurrentSearchIndex]).LineNumber);
                        Index.Editor.Select((Find.SearchIndex[Controller.Search.CurrentSearchIndex] - (Controller.Search.CurrentSearch.Length)), Controller.Search.CurrentSearch.Length);
                    }
                    else
                    {
                        SetSearchInfo(Properties.Resources.NoFurtherResult);
                    }
                }
                else
                {
                    if (Find.SearchIndex.Count > 0)
                    {
                        Find.SearchIndex.Clear();
                        Controller.Search.CurrentSearchIndex = 0;
                        Controller.Main.LastIndex = 0;
                    }

                    Controller.Search.CurrentSearch = Controller.Search.SearchBox.Text;
                    if (Controller.Search.CurrentSearch != null)
                    {
                        Find find = new Find();
                        while ((find = FindString(Controller.Main.CurrentEditor, Controller.Search.CurrentSearch, Controller.Main.LastIndex, !(Controller.Search.MatchCase.IsChecked.Value))) != null)
                        {
                            if (find.Index == -1) break;
                            Controller.Main.LastIndex = find.Index + Controller.Search.CurrentSearch.Length;
                            Find.SearchIndex.Add(Controller.Main.LastIndex);
                        }
                        if (Find.SearchIndex.Count > 0)
                        {
                            Index.Editor.ScrollToLine(Index.Editor.TextArea.Document.GetLineByOffset(Find.SearchIndex[0]).LineNumber);
                            int index = Find.SearchIndex[0] - (Controller.Search.CurrentSearch.Length);
                            if (index < 0)
                            {
                                index = 0;
                            }
                            Index.Editor.Select(index, Controller.Search.CurrentSearch.Length);

                            StringBuilder ResultText = new StringBuilder();
                            ResultText.Append(Properties.Resources.Result);
                            ResultText.Append(Find.SearchIndex.Count);

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
        public static void DoCount(TextBox SearchBox)
        {
            if (Controller.Search.SearchBox != null)
            {
                if (Controller.Search.CurrentSearch != null && Controller.Search.CurrentSearch.Equals(Core.Controller.Search.SearchBox.Text) == true)
                {
                    StringBuilder ResultText = new StringBuilder();
                    ResultText.Append(Properties.Resources.Result);
                    ResultText.Append(Find.SearchIndex.Count);

                    SetSearchInfo(ResultText);
                    return;
                }
                else
                {
                    if (Find.SearchIndex.Count > 0)
                    {
                        Find.SearchIndex.Clear();
                        Controller.Search.CurrentSearchIndex = 0;
                        Controller.Main.LastIndex = 1;
                    }
                    Controller.Search.CurrentSearch = Controller.Search.SearchBox.Text;
                    if (Controller.Search.CurrentSearch != null)
                    {
                        Find find = new Find();
                        while ((find = FindString(Controller.Main.CurrentEditor, Controller.Search.CurrentSearch, Controller.Main.LastIndex, !(Controller.Search.MatchCase.IsChecked.Value))) != null)
                        {
                            if (find.Index == -1) break;
                            Controller.Main.LastIndex = find.Index + Controller.Search.CurrentSearch.Length;
                            Find.SearchIndex.Add(Controller.Main.LastIndex);
                        }
                        if (Find.SearchIndex.Count > 0)
                        {
                            if (Controller.Search.SearchInfo.Visibility == Visibility.Collapsed)
                            {
                                Controller.Search.SearchInfo.Visibility = Visibility.Visible;
                            }
                            StringBuilder ResultText = new StringBuilder();
                            ResultText.Append(Properties.Resources.Result);
                            ResultText.Append(Find.SearchIndex.Count);

                            SetSearchInfo(ResultText);
                        }
                        else
                        {
                            SetSearchInfo(Properties.Resources.NoResult);
                        }
                    }
                }
            }
        }
    }
}
