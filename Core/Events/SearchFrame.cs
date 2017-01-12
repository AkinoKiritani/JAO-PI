using JAO_PI.Core.Controller;
using JAO_PI.Core.Utility;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace JAO_PI.EventsManager
{
    public class SearchFrame
    {
        // Main
        public void Loaded(object sender, RoutedEventArgs e)
        {
            SearchFrame SearchEvents = new SearchFrame();
            Search.SearchControl.SelectionChanged += SearchEvents.SelectionChanged;
            Search.SearchControl.Loaded -= SearchEvents.Loaded;
        }

        public void Closing(object sender, CancelEventArgs e)
        {
            Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
            if (Search.SearchInfo.Visibility == Visibility.Visible)
            {
                Search.SearchInfo.Visibility = Visibility.Collapsed;
            }
            e.Cancel = true;
        }

        // TabControl
        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem CurSelected = Search.SearchControl.Items[Search.SearchControl.SelectedIndex] as TabItem;
            Main.Frames[(int)Structures.Frames.SearchFrame].Title = CurSelected.Header.ToString();
        }

        // Search
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            if(Search.SearchBox != null)
            {
                Functions utility = new Functions();
                if (Search.CurrentSearch != null && Search.CurrentSearch.Equals(Search.SearchBox.Text) == true)
                {
                    Search.CurrentSearchIndex++;
                    if (Search.CurrentSearchIndex < Core.Classes.Find.SearchIndex.Count)
                    {
                        Main.CurrentEditor.ScrollToLine(Main.CurrentEditor.TextArea.Document.GetLineByOffset(Core.Classes.Find.SearchIndex[Search.CurrentSearchIndex]).LineNumber);
                        Main.CurrentEditor.Select((Core.Classes.Find.SearchIndex[Search.CurrentSearchIndex] - (Search.CurrentSearch.Length)), Search.CurrentSearch.Length);
                    }
                    else
                    {
                        utility.SetSearchInfo(Core.Properties.Resources.NoFurtherResult);
                    }
                }
                else
                {
                    if (Core.Classes.Find.SearchIndex.Count > 0)
                    {
                        Core.Classes.Find.SearchIndex.Clear();
                        Search.CurrentSearchIndex = 0;
                        Main.LastIndex = 0;
                    }

                    Search.CurrentSearch = Search.SearchBox.Text;
                    if (Search.CurrentSearch != null)
                    {
                        Core.Classes.Find find = new Core.Classes.Find();
                        while ((find = utility.FindString(Main.CurrentEditor, Search.CurrentSearch, Main.LastIndex, !(Search.MatchCase.IsChecked.Value))) != null)
                        {
                            if (find.Index == -1) break;
                            Main.LastIndex = find.Index + Search.CurrentSearch.Length;
                            Core.Classes.Find.SearchIndex.Add(Main.LastIndex);
                        }
                        if (Core.Classes.Find.SearchIndex.Count > 0)
                        {
                            Main.CurrentEditor.ScrollToLine(Main.CurrentEditor.TextArea.Document.GetLineByOffset(Core.Classes.Find.SearchIndex[0]).LineNumber);
                            int index = Core.Classes.Find.SearchIndex[0] - (Search.CurrentSearch.Length);
                            if (index < 0)
                            {
                                index = 0;
                            }
                            Main.CurrentEditor.Select(index, Search.CurrentSearch.Length);

                            StringBuilder ResultText = new StringBuilder();
                            ResultText.Append(Core.Properties.Resources.Result);
                            ResultText.Append(Core.Classes.Find.SearchIndex.Count);

                            utility.SetSearchInfo(ResultText);
                        }
                        else
                        {
                            utility.SetSearchInfo(Core.Properties.Resources.NoResult);
                        }
                    }
                }
            }
        }

        public void Count_Click(object sender, RoutedEventArgs e)
        {
            if (Search.SearchBox != null)
            {
                Functions utility = new Functions();
                if (Search.CurrentSearch != null && Search.CurrentSearch.Equals(Search.SearchBox.Text) == true)
                {
                    StringBuilder ResultText = new StringBuilder();
                    ResultText.Append(Core.Properties.Resources.Result);
                    ResultText.Append(Core.Classes.Find.SearchIndex.Count);

                    utility.SetSearchInfo(ResultText);
                    return;
                }
                else
                {
                    if (Core.Classes.Find.SearchIndex.Count > 0)
                    {
                        Core.Classes.Find.SearchIndex.Clear();
                        Search.CurrentSearchIndex = 0;
                        Main.LastIndex = 1;
                    }
                    Search.CurrentSearch = Search.SearchBox.Text;
                    if (Search.CurrentSearch != null)
                    {
                        Core.Classes.Find find = new Core.Classes.Find();
                        while ((find = utility.FindString(Main.CurrentEditor, Search.CurrentSearch, Main.LastIndex, !(Search.MatchCase.IsChecked.Value))) != null)
                        {
                            if (find.Index == -1) break;
                            Main.LastIndex = find.Index + Search.CurrentSearch.Length;
                            Core.Classes.Find.SearchIndex.Add(Main.LastIndex);
                        }
                        if (Core.Classes.Find.SearchIndex.Count > 0)
                        {
                            if (Search.SearchInfo.Visibility == Visibility.Collapsed)
                            {
                                Search.SearchInfo.Visibility = Visibility.Visible;
                            }
                            StringBuilder ResultText = new StringBuilder();
                            ResultText.Append(Core.Properties.Resources.Result);
                            ResultText.Append(Core.Classes.Find.SearchIndex.Count);

                            utility.SetSearchInfo(ResultText);
                        }
                        else
                        {
                            utility.SetSearchInfo(Core.Properties.Resources.NoResult);
                        }
                    }
                }
            }
        }

        // Replace
        public void Do_Replace_All(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Click");
        }

        public void Do_Replace(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Click");
        }

        public void Do_Search_Replace_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Click");
        }
    }
}
