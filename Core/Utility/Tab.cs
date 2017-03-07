using ICSharpCode.AvalonEdit;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace JAO_PI.Core.Utility
{
    static class Tab
    {
        public static void SaveTab(TabItem SaveTab)
        {
            if (Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                Controller.Tab Index = Controller.Main.TabControlList.Find(x => x.TabItem == SaveTab);
                if (Index != null)
                {
                    if (!Index.State.HasFlag(Structures.States.Saved))
                    {
                        Index.State |= Structures.States.Saved;
                    }
                    Index.State &= ~Structures.States.Changed;

                    StringBuilder FileToSave = new StringBuilder();
                    FileToSave.Append(SaveTab.Uid);
                    FileToSave.Append(GetTabHeaderText(SaveTab));

                    if (File.Exists(FileToSave.ToString()))
                    {
                        Index.Editor.Save(FileToSave.ToString());
                        FileToSave = null;
                    }
                    else
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog()
                        {
                            OverwritePrompt = true,
                            Filter = Properties.Resources.FileFilter,
                            Title = Properties.Resources.SaveFile
                        };
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            Tab.SaveTab(SaveTab, saveFileDialog);
                            UpdateTabHeaderText(SaveTab, saveFileDialog.SafeFileName);
                        }
                    }
                    Toggle.UnsavedMark(SaveTab, false);
                }
            }
        }
        public static void SaveTab(TabItem SaveTab, SaveFileDialog saveFileDialog)
        {
            if (Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                Controller.Tab Index = Controller.Main.TabControlList.Find(x => x.TabItem == SaveTab);
                if (Index != null)
                {
                    Index.Editor.Save(saveFileDialog.FileName);
                    Toggle.UnsavedMark(SaveTab, false);
                }                
            }
        }
        public static bool UpdateTabHeaderText(TabItem tab, string newHeaderText)
        {
            if (tab != null)
            {
                StackPanel sp = tab.Header as StackPanel;
                TextBlock tb = sp.Children[1] as TextBlock;
                tb.Text = newHeaderText;
                return true;
            }
            return false;
        }
        public static string GetTabHeaderText(TabItem tab)
        {
            if (tab != null)
            {
                StackPanel sp = tab.Header as StackPanel;
                TextBlock tb = sp.Children[1] as TextBlock;
                return tb.Text;
            }
            return null;
        }
        public static TextEditor GetTextEditor(int index)
        {
            TabItem item = Controller.Main.tabControl.Items[index] as TabItem;
            Grid EditorGrid = item.Content as Grid;
            return EditorGrid.Children[0] as TextEditor;
        }

        public static void RemoveTempTabs()
        {
            List<Controller.Tab> tmpTabs = Controller.Main.TabControlList.FindAll(x => x.Tmp == true);
            if (tmpTabs.Count > 0)
            {
                Controller.Tab Index = null;
                Grid grid = null;
                for (int i = 0; i != tmpTabs.Count; i++)
                {
                    Index = tmpTabs[i];

                    Index.Editor.Clear();
                    grid = Index.TabItem.Content as Grid;
                    Index.Editor = null;
                    grid.Children.Remove(Index.Editor);
                    grid = null;
                    Controller.Main.TabControlList.Remove(Index);

                    Index.TabItem.ContextMenu.Items.Clear();
                    Controller.Main.tabControl.Items.Remove(Index.TabItem);
                }
            }
        }
    }
}
