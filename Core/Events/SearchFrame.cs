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
                    tab.SearchList.Clear();
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
            TextBlock Close = Core.Controller.Search.Head.Children[(int)Structures.SearchHeader.CloseBox] as TextBlock;
            Close.Background = new SolidColorBrush(Color.FromRgb(199, 80, 80));
        }

        public void Close_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock Close = Core.Controller.Search.Head.Children[(int)Structures.SearchHeader.CloseBox] as TextBlock;
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
