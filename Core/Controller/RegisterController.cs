using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace JAO_PI.Core.Controller
{
    public class Register
    {
        public static bool Edit(MenuItem editItem)
        {
            if (Main.EditItem == null)
            {
                Main.EditItem = editItem;
                return true;
            }
            return false;
        }

        public static bool Compile(StatusBarItem compile)
        {
            if (Main.Compile == null)
            {
                Main.Compile = compile;
                return true;
            }
            return false;
        }

        public static bool Frames(Window mainFrame, Window searchFrame)
        {
            if (Main.Frames.Count == 0)
            {
                Main.Frames.Add(mainFrame);
                Main.Frames.Add(searchFrame);
                return true;
            }
            return false;
        }
        public static bool SearchBox(TextBox searchBox)
        {
            if (Main.SearchBox == null)
            {
                Main.SearchBox = searchBox;
                return true;
            }
            return false;
        }
        public static bool TabControl(TabControl Control)
        {
            if (Main.tabControl == null)
            {
                Main.tabControl = Control;
                return true;
            }
            return false;
        }
        public static bool EmptyMessage(Grid Empty_Message_Grid)
        {
            if (Main.Empty_Message == null)
            {
                Main.Empty_Message = Empty_Message_Grid;
                return true;
            }
            return false;
        }
        public static bool SaveOptions(MenuItem Save, MenuItem SaveAs, MenuItem CloseFile)
        {
            if (Main.SaveOptions.Count == 0)
            {
                Main.SaveOptions.Add(Save);
                Main.SaveOptions.Add(SaveAs);
                Main.SaveOptions.Add(CloseFile);
                return true;
            }
            return false;
        }
    }
}
