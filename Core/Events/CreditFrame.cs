using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    public class CreditFrame
    {
        public void OpenWebsite(object sender, MouseButtonEventArgs e)
        {
            if(sender != null)
            {
                TextBlock target = sender as TextBlock;
                System.Diagnostics.Process.Start(target.Text);
            }
        }

        public void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.CreditsFrame].Visibility = Visibility.Collapsed;
            Core.Controller.Main.Frames[(int) Structures.Frames.MainFrame].Focus();
        }

        public void Activated(object sender, EventArgs e)
        {
            Core.Controller.Credits.FrameBorder.BorderBrush = SystemParameters.WindowGlassBrush;
        }
    }
}
