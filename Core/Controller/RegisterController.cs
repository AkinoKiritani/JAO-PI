using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace JAO_PI.Core.Controller
{
    public static class Register
    {
        public static bool MainView(Grid MainView)
        {
            if(Main.MainView == null)
            {
                Main.MainView = MainView;
                return true;
            }
            return false;
        }

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

        public static bool CompilePanel(DockPanel compilerPanel)
        {
            if(Main.CompilerPanel == null)
            {
                Main.CompilerPanel = compilerPanel;
                Main.PanelBorder = compilerPanel.Children[(int)Structures.CompilerPanel.PanelBorder] as Border;
                Main.ErrorBox = compilerPanel.Children[(int)Structures.CompilerPanel.ErrorBox] as ListBox;
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

        public static bool SearchComponents(Grid SearchTabGrid)
        {
            if (Search.SearchBox == null)
            {
                Search.SearchBox = SearchTabGrid.Children[(int)Structures.SearchTab.SearchBox] as TextBox;
                Search.MatchCase = SearchTabGrid.Children[(int)Structures.SearchTab.MatchCase] as CheckBox;
                return true;
            }
            return false;
        }

        public static bool ReplaceComponents(Grid ReplaceTabGrid)
        {
            if (Search.SearchBox_Replace == null)
            {
                Search.SearchBox_Replace = ReplaceTabGrid.Children[(int)Structures.ReplaceTab.SearchReplaceBox] as TextBox;
                Search.ReplaceBox = ReplaceTabGrid.Children[(int)Structures.ReplaceTab.ReplaceBox] as TextBox;
                return true;
            }
            return false;
        }

        public static bool GoToComponents(Grid GoToTabGrid)
        {
            if(Search.Line == null)
            {
                Search.Line = GoToTabGrid.Children[(int)Structures.GoToTab.Line] as RadioButton;
                Search.Offset = GoToTabGrid.Children[(int)Structures.GoToTab.Offset] as RadioButton;
                Search.LineLabel = GoToTabGrid.Children[(int)Structures.GoToTab.Position] as Label;
                Search.GoToBox = GoToTabGrid.Children[(int)Structures.GoToTab.GoToBox] as TextBox;
                Search.MaxLineLabel = GoToTabGrid.Children[(int)Structures.GoToTab.Max_Position] as Label;
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
        public static bool EmptyMessage(Label Empty_Message)
        {
            if (Main.Empty_Message == null)
            {
                Main.Empty_Message = Empty_Message;
                return true;
            }
            return false;
        }
        public static bool SaveOptions(MenuItem MainMenu)
        {
            if (Main.SaveOptions.Count == 0)
            {
                Main.SaveOptions.Add(MainMenu.Items[(int)Structures.SaveOptions.CloseFile] as MenuItem);
                Main.SaveOptions.Add(MainMenu.Items[(int)Structures.SaveOptions.CloseAll] as MenuItem);
                Main.SaveOptions.Add(MainMenu.Items[(int)Structures.SaveOptions.Save] as MenuItem);
                Main.SaveOptions.Add(MainMenu.Items[(int)Structures.SaveOptions.SaveAs] as MenuItem);
                
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

        public static bool CreditsFrameBorder(Border FrameBorder)
        {
            if (Credits.FrameBorder == null)
            {
                Credits.FrameBorder = FrameBorder;
                return true;
            }
            return false;
        }
    }
}
