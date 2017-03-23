using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JAO_PI.Core.Events
{
    internal class TabItem
    {
        internal void PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var tabItem = e.Source as System.Windows.Controls.TabItem;
            if (tabItem == null)
            {
                return;
            }

            if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                if (Controller.Main.tabControl.Items.Count > 1)
                {
                    DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.All);
                }
            }
        }

        internal void Drop(object sender, DragEventArgs e)
        {
            System.Windows.Controls.TabItem tabItemTarget = null;
            if (e.Source is System.Windows.Controls.TabItem)
            {
                tabItemTarget = e.Source as System.Windows.Controls.TabItem;
            }
            else if (e.Source is TextBlock)
            {
                TextBlock k = e.Source as TextBlock;
                StackPanel Target = k.Parent as StackPanel;
                tabItemTarget = Target.Parent as System.Windows.Controls.TabItem;
            }
            else if (e.Source is Image)
            {
                Image k = e.Source as Image;
                StackPanel Target = k.Parent as StackPanel;
                tabItemTarget = Target.Parent as System.Windows.Controls.TabItem;
            }
            else return;
            var tabItemSource = e.Data.GetData(typeof(System.Windows.Controls.TabItem)) as System.Windows.Controls.TabItem;

            if (!tabItemTarget.Equals(tabItemSource))
            {
                var tabControl = tabItemTarget.Parent as TabControl;
                int sourceIndex = tabControl.Items.IndexOf(tabItemSource);
                int targetIndex = tabControl.Items.IndexOf(tabItemTarget);

                tabControl.Items.Remove(tabItemSource);
                tabControl.Items.Insert(targetIndex, tabItemSource);

                tabControl.Items.Remove(tabItemTarget);
                tabControl.Items.Insert(sourceIndex, tabItemTarget);
            }
        }


    }
}
