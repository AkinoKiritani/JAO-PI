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
        public static bool StatusBar(StatusBarItem Line, StatusBarItem Column)
        {
            if (Main.StatusBarItems.Count == 0)
            {
                Main.StatusBarItems.Add(Line);
                Main.StatusBarItems.Add(Column);
                return true;
            }
            return false;
        }

        public static bool ReplaceBox(TextBox replaceBox)
        {
            if (Search.ReplaceBox == null)
            {
                Search.ReplaceBox = replaceBox;
                return true;
            }
            return false;
        }

        public static bool SearchBox_Replace(TextBox searchBox_Replace)
        {
            if (Search.SearchBox_Replace == null)
            {
                Search.SearchBox_Replace = searchBox_Replace;
                return true;
            }
            return false;
        }

        public static bool MoveHeader(Canvas Head)
        {
            if (Search.Head == null)
            {
                Search.Head = Head;
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
            if (Search.SearchBox == null)
            {
                Search.SearchBox = searchBox;
                return true;
            }
            return false;
        }
        public static bool SearchInfo(TextBlock searchInfo)
        {
            if (Search.SearchInfo == null)
            {
                Search.SearchInfo = searchInfo;
                return true;
            }
            return false;
        }
        public static bool SearchControl(TabControl searchControl)
        {
            if (Search.SearchControl == null)
            {
                Search.SearchControl = searchControl;
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
        public static bool SaveOptions(MenuItem Save, MenuItem SaveAs, MenuItem CloseFile, MenuItem CloseAll)
        {
            if (Main.SaveOptions.Count == 0)
            {
                Main.SaveOptions.Add(Save);
                Main.SaveOptions.Add(SaveAs);
                Main.SaveOptions.Add(CloseFile);
                Main.SaveOptions.Add(CloseAll);
                return true;
            }
            return false;
        }
        public static bool CompileMenuItem(MenuItem compileMenuItem)
        {
            if (Main.CompileMenuItem == null)
            {
                Main.CompileMenuItem = compileMenuItem;
                return true;
            }
            return false;
        }

        public static bool MatchCase(CheckBox matchCase)
        {
            if (Search.MatchCase == null)
            {
                Search.MatchCase = matchCase;
                return true;
            }
            return false;
        }
    }
}
