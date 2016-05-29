using ICSharpCode.AvalonEdit.Document;
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
                Core.Controller.Tab result = Core.Controller.Main.TabControlList.Find(x => x.Editor.Document.FileName.Equals(Document.FileName));
                Document.FileName = Document.FileName.Replace(".JAOsaved", ".JAOnotsaved");
                Document.Changed -= Document_Changed;
            }
        }

        internal void Editor_Unloaded(object sender, RoutedEventArgs e)
        {
            System.GC.ReRegisterForFinalize(sender);
        }
    }
}
