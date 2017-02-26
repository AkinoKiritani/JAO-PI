using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    class ListBoxItem
    {
        internal void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListBoxItem Item = sender as System.Windows.Controls.ListBoxItem;
            if (Regex.IsMatch(Item.Uid, @"^\d+$"))
            {
                Core.Utility.Main.BringLineToView(Core.Controller.Main.CurrentEditor, Convert.ToInt32(Item.Uid));
            }
        }
    }
}
