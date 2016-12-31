using JAO_PI.Core.Controller;
using JAO_PI.Core.Utility;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace JAO_PI.EventsManager
{
    public class SearchFrame
    {
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            if(Main.SearchBox != null)
            {
                Functions utility = new Functions();
                if (Main.CurrentSearch != null && Main.CurrentSearch.Equals(Main.SearchBox.Text) == true)
                {
                    Main.CurrentSearchIndex++;
                    if (Main.CurrentSearchIndex < Core.Classes.Find.SearchIndex.Count)
                    {
                        Main.CurrentEditor.ScrollToLine(Main.CurrentEditor.TextArea.Document.GetLineByOffset(Core.Classes.Find.SearchIndex[Main.CurrentSearchIndex]).LineNumber);
                        Main.CurrentEditor.Select((Core.Classes.Find.SearchIndex[Main.CurrentSearchIndex] - (Main.CurrentSearch.Length)), Main.CurrentSearch.Length);
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
                        Main.CurrentSearchIndex = 0;
                        Main.LastIndex = 0;
                    }

                    Main.CurrentSearch = Main.SearchBox.Text;
                    if (Main.CurrentSearch != null)
                    {
                        Core.Classes.Find find = new Core.Classes.Find();
                        while ((find = utility.FindString(Main.CurrentEditor, Main.CurrentSearch, Main.LastIndex, true)) != null)
                        {
                            if (find.Index == -1) break;
                            Main.LastIndex = find.Index + Main.CurrentSearch.Length;
                            Core.Classes.Find.SearchIndex.Add(Main.LastIndex);
                        }
                        if (Core.Classes.Find.SearchIndex.Count > 0)
                        {
                            Main.CurrentEditor.ScrollToLine(Main.CurrentEditor.TextArea.Document.GetLineByOffset(Core.Classes.Find.SearchIndex[0]).LineNumber);
                            int index = Core.Classes.Find.SearchIndex[0] - (Main.CurrentSearch.Length);
                            if (index < 0)
                            {
                                index = 0;
                            }
                            Main.CurrentEditor.Select(index, Main.CurrentSearch.Length);

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
            if (Main.SearchBox != null)
            {
                Functions utility = new Functions();
                if (Main.CurrentSearch != null && Main.CurrentSearch.Equals(Main.SearchBox.Text) == true)
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
                        Main.CurrentSearchIndex = 0;
                        Main.LastIndex = 1;
                    }
                    Main.CurrentSearch = Main.SearchBox.Text;
                    if (Main.CurrentSearch != null)
                    {
                        Core.Classes.Find find = new Core.Classes.Find();
                        while ((find = utility.FindString(Main.CurrentEditor, Main.CurrentSearch, Main.LastIndex, true)) != null)
                        {
                            if (find.Index == -1) break;
                            Main.LastIndex = find.Index + Main.CurrentSearch.Length;
                            Core.Classes.Find.SearchIndex.Add(Main.LastIndex);
                        }
                        if (Core.Classes.Find.SearchIndex.Count > 0)
                        {
                            if (Main.SearchInfo.Visibility == Visibility.Collapsed)
                            {
                                Main.SearchInfo.Visibility = Visibility.Visible;
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
        public void Closing(object sender, CancelEventArgs e)
        {
            Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
            if (Main.SearchInfo.Visibility == Visibility.Visible)
            {
                Main.SearchInfo.Visibility = Visibility.Collapsed;
            }
            e.Cancel = true;
        }
    }
}
