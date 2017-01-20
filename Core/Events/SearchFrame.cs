using JAO_PI.Core.Utility;
using System.ComponentModel;
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

                foreach (Core.Controller.Tab tab in Core.Controller.Main.TabControlList)
                {
                    tab.SearchList.Clear();
                }
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
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.TabItem == (Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem));
            if (Index != null)
            {
                Index.State |= Structures.States.Searching;
                Search.DoSearch(Index, Core.Controller.Search.SearchBox);
            }
        }

        public void Count_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.TabItem == (Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem));
            if (Index != null)
            {
                Index.State |= Structures.States.Searching;
                Search.DoCount(Index, Core.Controller.Search.SearchBox);
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
