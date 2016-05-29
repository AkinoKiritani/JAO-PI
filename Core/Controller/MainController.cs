using ICSharpCode.AvalonEdit;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace JAO_PI.Core.Controller
{
    public class Main
    {
        public static TabControl tabControl = null;
        public static Grid Empty_Message = null;
        public static List<MenuItem> SaveOptions = new List<MenuItem>();
        public static List<Tab> TabControlList = new List<Tab>();
        public static string Compiler_Errors = null;
        public static int LastIndex;
        public static StatusBarItem Compile = null;
        public static MenuItem EditItem = null;

        //Frames
        public static List<Window> Frames = new List<Window>();

        //Search
        public static TextBox SearchBox = null;
        public static string CurrentSearch = null;
        public static int CurrentSearchIndex { get; set; }

        public static bool RegisterEdit(MenuItem editItem)
        {
            if (EditItem == null)
            {
                EditItem = editItem;
                return true;
            }
            return false;
        }

        public static bool RegisterCompile(StatusBarItem compile)
        {
            if (Compile == null)
            {
                Compile = compile;
                return true;
            }
            return false;
        }

        public static bool RegisterFrames(Window mainFrame, Window searchFrame)
        {
            if (Frames.Count == 0)
            {
                Frames.Add(mainFrame);
                Frames.Add(searchFrame);
                return true;
            }
            return false;
        }
        public static bool RegisterSearchBox(TextBox searchBox)
        {
            if (SearchBox == null)
            {
                SearchBox = searchBox;
                return true;
            }
            return false;
        }
        public static bool RegisterTabControl(TabControl Control)
        {
            if (tabControl == null)
            {
                tabControl = Control;
                return true;
            }
            return false;
        }
        public static bool RegisterEmptyMessage(Grid Empty_Message_Grid)
        {
            if (Empty_Message == null)
            {
                Empty_Message = Empty_Message_Grid;
                return true;
            }
            return false;
        }
        public static bool RegisterSaveOptions(MenuItem Save, MenuItem SaveAs, MenuItem CloseFile)
        {
            if (SaveOptions.Count == 0)
            {
                SaveOptions.Add(Save);
                SaveOptions.Add(SaveAs);
                SaveOptions.Add(CloseFile);
                return true;
            }
            return false;
        }
        
        public static void ToggleSaveOptions(bool toggle)
        {
            foreach (MenuItem item in SaveOptions)
            {
                item.IsEnabled = toggle;
            }
        }

        public static void SaveTab(TabItem SaveTab)
        {
            if (tabControl.Visibility == Visibility.Visible && tabControl.Items.Contains(SaveTab) == true)
            { 
                Grid SaveGrid = SaveTab.Content as Grid;
                TextEditor SaveEditor = SaveGrid.Children[0] as TextEditor;

                EventsManager.Editor EditorEvents = new EventsManager.Editor();
                SaveEditor.Document.Changed += EditorEvents.Document_Changed;

                System.Text.StringBuilder FileToSave = new System.Text.StringBuilder();
                FileToSave.Append(SaveTab.Uid);
                FileToSave.Append(SaveTab.Header);

                SaveEditor.Save(FileToSave.ToString());
                SaveEditor = null;
                SaveGrid = null;
                SaveTab = null;
                FileToSave = null;
            }
        }
        public static void SaveTab(TabItem SaveTab, Microsoft.Win32.SaveFileDialog saveFileDialog)
        {
            if(tabControl.Visibility == Visibility.Visible && tabControl.Items.Contains(SaveTab) == true)
            { 
                Grid SaveGrid = SaveTab.Content as Grid;
                TextEditor SaveEditor = SaveGrid.Children[0] as TextEditor;

                EventsManager.Editor EditorEvents = new EventsManager.Editor();
                SaveEditor.Document.Changed += EditorEvents.Document_Changed;

                SaveEditor.Save(saveFileDialog.FileName);
                SaveEditor = null;
                SaveGrid = null;
                SaveTab = null;
            }
        }
    }
}
