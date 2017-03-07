using JAO_PI.Core.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    class ListBoxItem
    {
        internal void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListBoxItem Item = sender as System.Windows.Controls.ListBoxItem;

            string[] matches = Regex.Split(Item.Uid, @"[\|]+"); // 0 = Path | 1 = Line -> Size = 2

            if (matches.Length == 2)
            {
                Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.TabItem == Core.Controller.Main.CompiledTabItem);
                if (Index != null)
                {
                    string HeaderText = Core.Utility.Tab.GetTabHeaderText(Index.TabItem);
                    if (matches[0].Equals(HeaderText))
                    {
                        if (Regex.IsMatch(matches[1], @"^\d+$"))
                        {
                            Core.Utility.Editor.BringLineToView(Core.Controller.Main.CurrentEditor, Convert.ToInt32(matches[1]));
                            Core.Controller.Main.tabControl.SelectedItem = Core.Controller.Main.CompiledTabItem;
                        }
                    }
                    else if (File.Exists(matches[0]))
                    {
                        int slash = matches[0].LastIndexOf('\\') + 1;
                        string path = matches[0].Substring(0, slash);
                        string file = matches[0].Substring(slash, matches[0].Length - slash);

                        List<Core.Controller.Tab> TabList = Core.Controller.Main.TabControlList.FindAll(x => x.TabItem.Uid.Equals(path));
                        if (TabList != null && TabList.Count > 0)
                        {
                            string Header = null;
                            for (int i = 0; i != TabList.Count; i++)
                            {
                                Header = Core.Utility.Tab.GetTabHeaderText(TabList[i].TabItem);
                                if (matches[0].Equals(path + Header))
                                {
                                    if (Regex.IsMatch(matches[1], @"^\d+$"))
                                    {
                                        Core.Controller.Main.tabControl.SelectedItem = TabList[i].TabItem;
                                        Core.Utility.Editor.BringLineToView(Core.Controller.Main.CurrentEditor, Convert.ToInt32(matches[1]));
                                    }
                                    return;
                                }
                            }
                        }
                        // When the File isn't already open, then open the file and create a tmpTab
                        FileStream stream = new FileStream(matches[0], FileMode.Open, FileAccess.Read);
                        Generator generator = new Generator();
                        TabItem tab = generator.TmpTabItem(matches[0], file, stream);

                        Core.Controller.Main.tabControl.Items.Add(tab);
                        Core.Controller.Main.tabControl.SelectedItem = tab;
                        stream.Dispose();

                        if (Regex.IsMatch(matches[1], @"^\d+$"))
                        {
                            Core.Utility.Editor.BringLineToView(Core.Controller.Main.CurrentEditor, Convert.ToInt32(matches[1]));
                        }
                    }
                }
            }
        }
    }
}
