using ICSharpCode.AvalonEdit.Document;
using System;
using System.Windows;

namespace JAO_PI.EventsManager
{
    class Editor
    {
        internal void Document_Changed(object sender, DocumentChangeEventArgs e)
        {
            TextDocument Document = sender as TextDocument;
            if (Document.FileName.Contains(".JAOsaved"))
            {
                Document.FileName = Document.FileName.Replace(".JAOsaved", ".JAOnotsaved");
                Document.Changed -= Document_Changed;
            }
        }

        internal void Editor_Unloaded(object sender, RoutedEventArgs e)
        {
            GC.ReRegisterForFinalize(sender);
            GC.Collect();
        }

        internal void TextInput(object sender, EventArgs e)
        {
            Core.Controller.Main.StatusBarItems[(int)Core.Utility.Structures.StatusBar.Line].Content = Core.Properties.Resources.Line + ": " + Core.Controller.Main.CurrentEditor.TextArea.Caret.Line;
            Core.Controller.Main.StatusBarItems[(int)Core.Utility.Structures.StatusBar.Column].Content = Core.Properties.Resources.Column + ": " + Core.Controller.Main.CurrentEditor.TextArea.Caret.Column;
        }
    }
}
