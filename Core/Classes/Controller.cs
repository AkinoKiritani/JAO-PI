using ICSharpCode.AvalonEdit;
using System;
using System.Linq;
using System.Windows.Controls;

namespace JAO_PI.Core.Classes
{
    class MainController
    {
        public static TabControl tabControl = null;
        public static void RegisterTabControl(TabControl Control)
        {
            if(tabControl == null) tabControl = Control;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
    public class TabController
    {
        public MenuItem Close { get; set; }
        public MenuItem Rename { get; set; }
        public TabItem TabItem { get; set; }
        public TextEditor Editor { get; set; }
    }
}
