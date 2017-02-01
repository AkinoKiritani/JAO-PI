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
            TextBlock target = sender as TextBlock;
            System.Diagnostics.Process.Start(target.Text);
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.CreditsFrame].Visibility = Visibility.Collapsed;
        }

        public void Activated(object sender, EventArgs e)
        {
            Core.Controller.Credits.FrameBorder.BorderBrush = SystemParameters.WindowGlassBrush;
        }
    }
}
