using System.Windows;
using System.Windows.Controls;

namespace JAO_PI.Core.Utility
{
    static class Toggle
    {
        public static void SaveOptions(bool toggle)
        {
            foreach (MenuItem item in Controller.Main.SaveOptions)
            {
                item.IsEnabled = toggle;
            }
        }
        public static void TabControl(bool toggle)
        {
            if(toggle)
            {
                Controller.Main.tabControl.Visibility = Visibility.Visible;
                Controller.Main.Empty_Message.Visibility = Visibility.Collapsed;
                Controller.Main.Empty_Message.IsEnabled = false;
            }
            else
            {
                Controller.Main.tabControl.Visibility = Visibility.Collapsed;
                Controller.Main.Empty_Message.Visibility = Visibility.Visible;
                Controller.Main.Empty_Message.IsEnabled = true;
            }
        }
        public static void UnsavedMark(TabItem tab, bool toggle)
        {
            if (tab != null)
            {
                StackPanel sp = tab.Header as StackPanel;
                TextBlock tb = sp.Children[2] as TextBlock;
                if(toggle)
                {
                    tb.Visibility = Visibility.Visible;
                }
                else
                {
                    tb.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
