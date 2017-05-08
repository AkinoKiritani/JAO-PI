using JAO_PI.Core.Utility;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

            Border FrameBorder = Core.Controller.Search.Head.Children[(int)Structures.SearchHeader.FrameBorder] as Border;
            FrameBorder.BorderBrush = SystemParameters.WindowGlassBrush;
        }

        public void Closing(object sender, CancelEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
            if (Core.Controller.Search.SearchInfo.Visibility == Visibility.Visible)
            {
                Core.Controller.Search.SearchInfo.Visibility = Visibility.Collapsed;

                foreach (Core.Controller.Tab tab in Core.Controller.Main.TabControlList)
                {
                    if (tab.SearchList != null)
                    {
                        tab.SearchList.Clear();
                    }
                }
            }
            e.Cancel = true;
        }

        // Header
        public void Head_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].DragMove();
        }

        public void Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Close();
        }

        public void Close_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock Close = sender as TextBlock;
            Close.Background = new SolidColorBrush(Color.FromRgb(199, 80, 80));
        }

        public void Close_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock Close = sender as TextBlock;
            Close.Background = new SolidColorBrush(Color.FromRgb(244, 67, 67));
        }

        // TabControl
        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem CurSelected = Core.Controller.Search.SearchControl.Items[Core.Controller.Search.SearchControl.SelectedIndex] as TabItem;
            TextBlock Header = Core.Controller.Search.Head.Children[(int)Structures.SearchHeader.HeaderBox] as TextBlock;
            Header.Text = CurSelected.Header.ToString();

            if (Core.Controller.Search.SearchControl.SelectedIndex == 2)
            {
                GoTo.RefreshLineOffset();
            }
        }

        public void Activated(object sender, EventArgs e)
        {
            Window SearchFrame = Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame];
            if (SearchFrame.Opacity != 100)
            {
                SearchFrame.Opacity = 100;

                Border FrameBorder = Core.Controller.Search.Head.Children[(int)Structures.SearchHeader.FrameBorder] as Border;
                FrameBorder.BorderBrush = SystemParameters.WindowGlassBrush;
            }

            if (Core.Controller.Search.SearchControl.SelectedIndex == 2)
            {
                GoTo.RefreshLineOffset();
            }
        }

        // Search
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.TabItem == (Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem));
            if (Index != null)
            {
                Core.Controller.Search.LastSearchTyp = Structures.LastSearch.Search;
                Core.Controller.Search.SearchBeginOffset = Index.Editor.CaretOffset;
                Search.DoSearch(Index, Core.Controller.Search.SearchBox);
            }
        }

        public void Count_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.TabItem == (Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem));
            if (Index != null)
            {
                Core.Controller.Search.LastSearchTyp = Structures.LastSearch.Count;
                Search.DoCount(Index, Core.Controller.Search.SearchBox);
            }
        }

        // Replace
        public void Do_Replace_All(object sender, RoutedEventArgs e)
        {
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.TabItem == (Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem));
            if (Index != null)
            {
                Core.Controller.Search.LastSearchTyp = Structures.LastSearch.ReplaceAll;
                Index.SearchList = Search.FindString(Index.Editor, Core.Controller.Replace.SearchBox_Replace.Text, false);
                Core.Controller.Search.CurrentSearchIndex = 0;
                Core.Controller.Search.CurrentSearch = Core.Controller.Replace.SearchBox_Replace.Text;

                int offset = 0;
                int lenght = Core.Controller.Replace.SearchBox_Replace.Text.Length;
                for (int i = 0; i != Index.SearchList.Count; i++)
                {
                    offset = Index.SearchList[i].Index;
                    Index.Editor.Document.Replace(offset, lenght, Core.Controller.Replace.ReplaceBox.Text);
                }

                if (Index.SearchList.Count > 0)
                {
                    Index.SearchList.Clear();
                }
            }
        }

        public void Do_Replace(object sender, RoutedEventArgs e)
        {
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.TabItem == (Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem));
            if (Index != null)
            {
                Core.Controller.Search.CurrentSearchIndex++;
                if (Core.Controller.Search.CurrentSearch != Core.Controller.Replace.SearchBox_Replace.Text) // do a new search
                {
                    Core.Controller.Search.LastSearchTyp = Structures.LastSearch.Replace;
                    Core.Controller.Search.SearchBeginOffset = Index.Editor.CaretOffset;
                    Index.SearchList = Search.FindString(Index.Editor, Core.Controller.Replace.SearchBox_Replace.Text, false);
                    Core.Controller.Search.CurrentSearchIndex = 0;
                    Core.Controller.Search.CurrentSearch = Core.Controller.Replace.SearchBox_Replace.Text;
                }
                
                // if a "Replace"match were skiped and then replace where clicked
                if (Core.Controller.Search.LastSearchTyp == Structures.LastSearch.ReplaceSearch)
                {
                    Core.Controller.Search.CurrentSearchIndex--;
                    Core.Controller.Search.LastSearchTyp = Structures.LastSearch.Replace;
                }

                // if a Search is already done with the "normal" Search, jump to the top
                if (Core.Controller.Search.LastSearchTyp != Structures.LastSearch.Replace)
                {
                    Core.Controller.Search.CurrentSearchIndex = 0;
                    Core.Controller.Search.LastSearchTyp = Structures.LastSearch.Replace;
                }
                                
                if (Core.Controller.Search.CurrentSearchIndex < Index.SearchList.Count)
                {
                    int offset = Index.SearchList[Core.Controller.Search.CurrentSearchIndex].Index;
                    int lenght = Core.Controller.Replace.SearchBox_Replace.Text.Length;
                    Index.Editor.Document.Replace(offset, lenght, Core.Controller.Replace.ReplaceBox.Text);

                    Core.Utility.Editor.SelectAndBringToView(Index.Editor, offset, lenght);
                }
            }
        }

        public void Do_Search_Replace_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.TabItem == (Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem));
            if (Index != null)
            {
                if (Core.Controller.Search.LastSearchTyp == Structures.LastSearch.Replace || Core.Controller.Search.LastSearchTyp == Structures.LastSearch.ReplaceSearch)
                {
                    Core.Controller.Search.CurrentSearchIndex++;
                    Core.Utility.Editor.SelectAndBringToView(Index.Editor, Index.SearchList[Core.Controller.Search.CurrentSearchIndex].Index, Core.Controller.Search.CurrentSearch.Length);
                    Core.Controller.Search.LastSearchTyp = Structures.LastSearch.ReplaceSearch;
                }
                else
                {
                    Core.Controller.Search.LastSearchTyp = Structures.LastSearch.ReplaceSearch;
                    Search.DoSearch(Index, Core.Controller.Replace.SearchBox_Replace);
                }
            }
        }

        public void WrapAround_Unchecked(object sender, RoutedEventArgs e)
        {
            Core.Controller.Search.WrapAround.IsChecked = false;
            Core.Controller.Replace.WrapAround.IsChecked = false;
        }

        public void WrapAround_Checked(object sender, RoutedEventArgs e)
        {
            Core.Controller.Search.WrapAround.IsChecked = true;
            Core.Controller.Replace.WrapAround.IsChecked = true;
        }

        public void MatchCase_Unchecked(object sender, RoutedEventArgs e)
        {
            Core.Controller.Search.MatchCase.IsChecked = false;
            Core.Controller.Replace.MatchCase.IsChecked = false;
        }

        public void MatchCase_Checked(object sender, RoutedEventArgs e)
        {
            Core.Controller.Search.MatchCase.IsChecked = true;
            Core.Controller.Replace.MatchCase.IsChecked = true;
        }
    }
}
