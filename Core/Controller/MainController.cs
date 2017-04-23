using ICSharpCode.AvalonEdit;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace JAO_PI.Core.Controller
{
    public static class Main
    {
        public static Grid MainView = null;
        public static TabControl tabControl = null;
        public static TextEditor CurrentEditor = null;
        public static Label Empty_Message = null;
        public static List<MenuItem> SaveOptions = new List<MenuItem>();
        public static List<Tab> TabControlList = new List<Tab>();
        public static string Compiler_Errors = null;
        public static int LastIndex = 0;
        public static StatusBarItem Compile = null;
        public static MenuItem EditItem = null;
        public static List<StatusBarItem> StatusBarItems = new List<StatusBarItem>();
        public static MenuItem CompileMenuItem = null;
        
        public static TabItem CompiledTabItem = null;

        public static DockPanel CompilerPanel = null;
        public static RowDefinition PanelHeight = new RowDefinition()
        {
            Height = new GridLength(140),
            MinHeight = 140
        };
        public static Border PanelBorder = null;
        public static ListBox ErrorBox = null;

        //Frames
        public static List<Window> Frames = new List<Window>();
    }
}
