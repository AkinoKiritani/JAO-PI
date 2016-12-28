﻿using JAO_PI.Core.Controller;
using JAO_PI.Core.Utility;
using System.ComponentModel;
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
                Core.Classes.Find find = new Core.Classes.Find();
                Functions utility = new Functions();
                if (Main.CurrentSearch != null && Main.CurrentSearch.Equals(Main.SearchBox.Text) == true)
                {
                    Main.CurrentSearchIndex++;
                    if (Main.CurrentSearchIndex < Core.Classes.Find.SearchIndex.Count)
                    {
                        Main.CurrentEditor.ScrollToLine(Main.CurrentEditor.TextArea.Document.GetLineByOffset(Core.Classes.Find.SearchIndex[Main.CurrentSearchIndex]).LineNumber);
                        Main.CurrentEditor.Select((Core.Classes.Find.SearchIndex[Main.CurrentSearchIndex] - (Main.CurrentSearch.Length+1)), Main.CurrentSearch.Length);
                    }
                    else
                    {
                        MessageBox.Show(Core.Properties.Resources.NoFurtherResult, Core.Properties.Resources.ProgName, MessageBoxButton.OK, MessageBoxImage.Information);
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
                        while ((find = utility.FindString(Main.CurrentEditor, Main.CurrentSearch, Main.LastIndex, true)) != null)
                        {
                            if (find.Index == -1) break;
                            Main.LastIndex = find.Index + Main.CurrentSearch.Length + 1;
                            Core.Classes.Find.SearchIndex.Add(Main.LastIndex);
                        }
                        if (Core.Classes.Find.SearchIndex.Count > 0)
                        {
                            Main.CurrentEditor.ScrollToLine(Main.CurrentEditor.TextArea.Document.GetLineByOffset(Core.Classes.Find.SearchIndex[0]).LineNumber);
                            Main.CurrentEditor.Select((Core.Classes.Find.SearchIndex[0] - (Main.CurrentSearch.Length + 1)), Main.CurrentSearch.Length);
                        }
                        else
                        {
                            MessageBox.Show(Core.Properties.Resources.NoResult, Core.Properties.Resources.ProgName, MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }

        public void Closing(object sender, CancelEventArgs e)
        {
            Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }
    }
}
