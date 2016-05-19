using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace JAO_PI.Core.Classes
{
    class MainController
    {
        public static TabControl tabControl = null;
        public static Grid Empty_Message = null;

        public static List<MenuItem> SaveOptions = new List<MenuItem>();
        public static void RegisterTabControl(TabControl Control)
        {
            if(tabControl == null) tabControl = Control;
        }
        public static void RegisterEmptyMessage(Grid Empty_Message_Grid)
        {
            if (Empty_Message == null) Empty_Message = Empty_Message_Grid;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcefghijklmnopqrstuvw";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void RegisterSaveOptions(MenuItem Save, MenuItem SaveAs, MenuItem CloseFile)
        {
            if(SaveOptions.Count == 0)
            {
                SaveOptions.Add(Save);
                SaveOptions.Add(SaveAs);
                SaveOptions.Add(CloseFile);
            }
        }
        public static void ToggleSaveOptions(bool toggle)
        {
            foreach (MenuItem item in SaveOptions)
            {
                item.IsEnabled = toggle;
            }
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
