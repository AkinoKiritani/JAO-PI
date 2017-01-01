using ICSharpCode.AvalonEdit;
using JAO_PI.Core.Classes;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace JAO_PI.Core.Utility
{
    class Functions
    {
        public TextEditor GetTextEditor(int index)
        {
            TabItem item = Controller.Main.tabControl.Items[index] as TabItem;
            Grid EditorGrid = item.Content as Grid;
            return EditorGrid.Children[0] as TextEditor;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcefghijklmnopqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Find FindString(TextEditor Editor, string SearchQuery, int lastIndex, bool IgnoreCase)
        {
            Find find = new Find();
            CompareInfo myComp = CultureInfo.InvariantCulture.CompareInfo;

            string Text = Editor.Text;
            if (SearchQuery.Length > 0 && Text.Length > 0)
            {
                 find.Index = myComp.IndexOf(Text, SearchQuery, lastIndex, (IgnoreCase == true) ? CompareOptions.IgnoreCase : CompareOptions.None);
                 find.Line = (find.Index == -1) ? -1 : Editor.TextArea.Document.GetLineByOffset(find.Index).LineNumber;
                 return find;
            }
            return find = null;
        }
        public void LoadSyntax(TextEditor Editor, string Path)
        {
            try
            {
                using (Stream s = File.OpenRead(Path))
                {
                    using (XmlTextReader reader = new XmlTextReader(s))
                    {
                        Editor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load
                                                    (reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Core.Properties.Resources.SyntaxReadingError);
            }
        }

        public void SaveTab(TabItem SaveTab)
        {
            if (Controller.Main.tabControl.Visibility == Visibility.Visible && Controller.Main.tabControl.Items.Contains(SaveTab) == true)
            {
                Grid SaveGrid = SaveTab.Content as Grid;
                TextEditor SaveEditor = SaveGrid.Children[0] as TextEditor;

                if (SaveEditor.Document.FileName.Contains(".JAOnotsaved"))
                {
                    SaveEditor.Document.FileName = SaveEditor.Document.FileName.Replace(".JAOnotsaved", ".JAOsaved");
                }

                EventsManager.Editor EditorEvents = new EventsManager.Editor();
                SaveEditor.Document.Changed += EditorEvents.Document_Changed;

                StringBuilder FileToSave = new System.Text.StringBuilder();
                FileToSave.Append(SaveTab.Uid);
                FileToSave.Append(SaveTab.Header);

                if (File.Exists(FileToSave.ToString()))
                {
                    SaveEditor.Save(FileToSave.ToString());

                    SaveEditor = null;
                    SaveGrid = null;
                    SaveTab = null;
                    FileToSave = null;
                }
                else
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.OverwritePrompt = true;
                    saveFileDialog.Filter = Core.Properties.Resources.FileFilter;
                    saveFileDialog.Title = Core.Properties.Resources.SaveFile;
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        TabItem Tab = Controller.Main.tabControl.Items[Controller.Main.tabControl.SelectedIndex] as TabItem;
                        this.SaveTab(Tab, saveFileDialog);
                        Tab.Header = saveFileDialog.SafeFileName;
                    }
                }
            }
        }
        public void SaveTab(TabItem SaveTab, SaveFileDialog saveFileDialog)
        {
            if (Controller.Main.tabControl.Visibility == Visibility.Visible && Controller.Main.tabControl.Items.Contains(SaveTab) == true)
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
        public bool SetSearchInfo(string text)
        {
            if (Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility == Visibility.Visible)
            {
                if (Controller.Main.SearchInfo.Visibility == Visibility.Collapsed)
                {
                    Controller.Main.SearchInfo.Visibility = Visibility.Visible;
                }
                Controller.Main.SearchInfo.Text = text;
                return true;
            }
            return false;
        }
        public bool SetSearchInfo(StringBuilder text)
        {
            if (Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility == Visibility.Visible)
            {
                if (Controller.Main.SearchInfo.Visibility == Visibility.Collapsed)
                {
                    Controller.Main.SearchInfo.Visibility = Visibility.Visible;
                }
                Controller.Main.SearchInfo.Text = text.ToString();
                return true;
            }
            return false;
        }
    }
}
