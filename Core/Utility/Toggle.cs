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
    }
}
