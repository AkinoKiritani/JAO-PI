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
            Core.Controller.Search.SearchControl.SelectionChanged += SearchEvents.SelectionChanged;
            Core.Controller.Search.SearchControl.Loaded -= SearchEvents.Loaded;
        }

        public void Closing(object sender, CancelEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
            if (Core.Controller.Search.SearchInfo.Visibility == Visibility.Visible)
            {
                Core.Controller.Search.SearchInfo.Visibility = Visibility.Collapsed;
            }
            e.Cancel = true;
        }

        // TabControl
        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem CurSelected = Core.Controller.Search.SearchControl.Items[Core.Controller.Search.SearchControl.SelectedIndex] as TabItem;
            Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Title = CurSelected.Header.ToString();
        }

        // Search
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            if(Core.Controller.Search.SearchBox != null)
            {
                if (Core.Controller.Search.CurrentSearch != null && Core.Controller.Search.CurrentSearch.Equals(Core.Controller.Search.SearchBox.Text) == true)
                {
                    Core.Controller.Search.CurrentSearchIndex++;
                    if (Core.Controller.Search.CurrentSearchIndex < Core.Classes.Find.SearchIndex.Count)
                    {
                        Core.Controller.Main.CurrentEditor.ScrollToLine(Core.Controller.Main.CurrentEditor.TextArea.Document.GetLineByOffset(Core.Classes.Find.SearchIndex[Core.Controller.Search.CurrentSearchIndex]).LineNumber);
                        Core.Controller.Main.CurrentEditor.Select((Core.Classes.Find.SearchIndex[Core.Controller.Search.CurrentSearchIndex] - (Core.Controller.Search.CurrentSearch.Length)), Core.Controller.Search.CurrentSearch.Length);
                    }
                    else
                    {
                        Core.Utility.Search.SetSearchInfo(Core.Properties.Resources.NoFurtherResult);
                    }
                }
                else
                {
                    if (Core.Classes.Find.SearchIndex.Count > 0)
                    {
                        Core.Classes.Find.SearchIndex.Clear();
                        Core.Controller.Search.CurrentSearchIndex = 0;
                        Core.Controller.Main.LastIndex = 0;
                    }

                    Core.Controller.Search.CurrentSearch = Core.Controller.Search.SearchBox.Text;
                    if (Core.Controller.Search.CurrentSearch != null)
                    {
                        Core.Classes.Find find = new Core.Classes.Find();
                        while ((find = Core.Utility.Search.FindString(Core.Controller.Main.CurrentEditor, Core.Controller.Search.CurrentSearch, Core.Controller.Main.LastIndex, !(Core.Controller.Search.MatchCase.IsChecked.Value))) != null)
                        {
                            if (find.Index == -1) break;
                            Core.Controller.Main.LastIndex = find.Index + Core.Controller.Search.CurrentSearch.Length;
                            Core.Classes.Find.SearchIndex.Add(Core.Controller.Main.LastIndex);
                        }
                        if (Core.Classes.Find.SearchIndex.Count > 0)
                        {
                            Core.Controller.Main.CurrentEditor.ScrollToLine(Core.Controller.Main.CurrentEditor.TextArea.Document.GetLineByOffset(Core.Classes.Find.SearchIndex[0]).LineNumber);
                            int index = Core.Classes.Find.SearchIndex[0] - (Core.Controller.Search.CurrentSearch.Length);
                            if (index < 0)
                            {
                                index = 0;
                            }
                            Core.Controller.Main.CurrentEditor.Select(index, Core.Controller.Search.CurrentSearch.Length);

                            StringBuilder ResultText = new StringBuilder();
                            ResultText.Append(Core.Properties.Resources.Result);
                            ResultText.Append(Core.Classes.Find.SearchIndex.Count);

                            Core.Utility.Search.SetSearchInfo(ResultText);
                        }
                        else
                        {
                            Core.Utility.Search.SetSearchInfo(Core.Properties.Resources.NoResult);
                        }
                    }
                }
            }
        }

        public void Count_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Search.SearchBox != null)
            {
                if (Core.Controller.Search.CurrentSearch != null && Core.Controller.Search.CurrentSearch.Equals(Core.Controller.Search.SearchBox.Text) == true)
                {
                    StringBuilder ResultText = new StringBuilder();
                    ResultText.Append(Core.Properties.Resources.Result);
                    ResultText.Append(Core.Classes.Find.SearchIndex.Count);

                    Core.Utility.Search.SetSearchInfo(ResultText);
                    return;
                }
                else
                {
                    if (Core.Classes.Find.SearchIndex.Count > 0)
                    {
                        Core.Classes.Find.SearchIndex.Clear();
                        Core.Controller.Search.CurrentSearchIndex = 0;
                        Core.Controller.Main.LastIndex = 1;
                    }
                    Core.Controller.Search.CurrentSearch = Core.Controller.Search.SearchBox.Text;
                    if (Core.Controller.Search.CurrentSearch != null)
                    {
                        Core.Classes.Find find = new Core.Classes.Find();
                        while ((find = Core.Utility.Search.FindString(Core.Controller.Main.CurrentEditor, Core.Controller.Search.CurrentSearch, Core.Controller.Main.LastIndex, !(Core.Controller.Search.MatchCase.IsChecked.Value))) != null)
                        {
                            if (find.Index == -1) break;
                            Core.Controller.Main.LastIndex = find.Index + Core.Controller.Search.CurrentSearch.Length;
                            Core.Classes.Find.SearchIndex.Add(Core.Controller.Main.LastIndex);
                        }
                        if (Core.Classes.Find.SearchIndex.Count > 0)
                        {
                            if (Core.Controller.Search.SearchInfo.Visibility == Visibility.Collapsed)
                            {
                                Core.Controller.Search.SearchInfo.Visibility = Visibility.Visible;
                            }
                            StringBuilder ResultText = new StringBuilder();
                            ResultText.Append(Core.Properties.Resources.Result);
                            ResultText.Append(Core.Classes.Find.SearchIndex.Count);

                            Core.Utility.Search.SetSearchInfo(ResultText);
                        }
                        else
                        {
                            Core.Utility.Search.SetSearchInfo(Core.Properties.Resources.NoResult);
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
