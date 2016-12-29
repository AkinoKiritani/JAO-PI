using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Windows;
using System.Windows.Input;

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

        internal void Caret_PositionChanged(object sender, EventArgs e)
        {
            Caret Area = sender as Caret;
            Core.Controller.Main.StatusBarItems[(int)Core.Utility.Structures.StatusBar.Line].Content = Core.Properties.Resources.Line + ": " + Area.Line;
            Core.Controller.Main.StatusBarItems[(int)Core.Utility.Structures.StatusBar.Column].Content = Core.Properties.Resources.Column + ": " + Area.Column;
        }
    }
}
