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

        public static bool Frames(Window[] Frame)
        {
            if (Main.Frames.Count == 0)
            {
                foreach (Window frame in Frame)
                {
                    Main.Frames.Add(frame);
                }
                return true;
            }
            return false;
        }

        public static void SetFrameAsOwner(Window main)
        {
            for(int i = 1; i != Main.Frames.Count; i++)
            {
                Main.Frames[i].Owner = main;
            }
        }
        public static bool GoToComponents(TextBox GoToBox, Label lineLabel, Label MaxLineLabel, RadioButton Line, RadioButton Offset)
        {
            Main.GoToBox = GoToBox;
            Main.LineLabel = lineLabel;
            Main.MaxLineLabel = MaxLineLabel;
            Main.Line = Line;
            Main.Offset = Offset;
            return true;
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
