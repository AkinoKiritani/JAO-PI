using ICSharpCode.AvalonEdit;
using System;
using System.IO;
using System.Xml;

namespace JAO_PI.Core.Utility
{
    class Editor
    {
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
                System.Windows.MessageBox.Show(Properties.Resources.SyntaxReadingError);
            }
        }

        public static void SelectAndBringToView(TextEditor Editor, int offset, int lenght)
        {
            Editor.Select(offset, lenght);
            Editor.TextArea.Caret.Offset = offset;
            Editor.TextArea.Caret.BringCaretToView();
            Editor.TextArea.Caret.Show();
        }

        public static void BringLineToView(TextEditor Editor, int Line)
        {
            Editor.TextArea.Caret.Line = Line;
            Editor.TextArea.Caret.Column = 0;
            Editor.TextArea.Caret.BringCaretToView();
            Editor.TextArea.Caret.Show();
        }
    }
}
