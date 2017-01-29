using ICSharpCode.AvalonEdit;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;

namespace JAO_PI.Core.Utility
{
    static class Main
    {
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcefghijklmnopqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        public static void LoadSyntax(TextEditor Editor, string Path)
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
                MessageBox.Show(Properties.Resources.SyntaxReadingError);
            }
        }

        public static void SelectAndOpenSearchTab(Structures.SearchControl Index)
        {
            if (Controller.Main.tabControl.Items.Count > 0 && Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                if (Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility == Visibility.Collapsed)
                {
                    Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Visible;
                }
                Controller.Search.SearchControl.SelectedIndex = (int)Index;
                Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Focus();
            }
        }
    }
}
