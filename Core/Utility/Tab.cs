﻿using ICSharpCode.AvalonEdit;
using Microsoft.Win32;
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
            if (Controller.Main.tabControl.Visibility == Visibility.Visible && Controller.Main.tabControl.Items.Contains(SaveTab) == true)
            {
                Grid SaveGrid = SaveTab.Content as Grid;
                TextEditor SaveEditor = SaveGrid.Children[0] as TextEditor;

                if (SaveEditor.Document.FileName.Contains(".JAOnotsaved"))
                {
                    SaveEditor.Document.FileName = SaveEditor.Document.FileName.Replace(".JAOnotsaved", ".JAOsaved");
                }
                
                StringBuilder FileToSave = new StringBuilder();
                FileToSave.Append(SaveTab.Uid);
                FileToSave.Append(GetTabHeaderText(SaveTab));

                if (File.Exists(FileToSave.ToString()))
                {
                    SaveEditor.Save(FileToSave.ToString());

                    SaveEditor = null;
                    SaveGrid = null;
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
        public static void SaveTab(TabItem SaveTab, SaveFileDialog saveFileDialog)
        {
            if (Controller.Main.tabControl.Visibility == Visibility.Visible && Controller.Main.tabControl.Items.Contains(SaveTab) == true)
            {
                Grid SaveGrid = SaveTab.Content as Grid;
                TextEditor SaveEditor = SaveGrid.Children[0] as TextEditor;

                SaveEditor.Save(saveFileDialog.FileName);
                SaveEditor = null;
                SaveGrid = null;

                Toggle.UnsavedMark(SaveTab, false);
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
    }
}
