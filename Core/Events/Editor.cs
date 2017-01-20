using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using JAO_PI.Core.Utility;
using System;
using System.Windows;

namespace JAO_PI.EventsManager
{
    class Editor
    {
        internal void Document_Changed(object sender, DocumentChangeEventArgs e)
        {
            Core.Controller.Tab Index = Core.Controller.Main.TabControlList.Find(x => x.State.HasFlag(Structures.States.Saved) &&
                                                                                      x.Editor.Document == (sender as TextDocument));
            if (Index != null)
            {
                if (!Index.State.HasFlag(Structures.States.Changed))
                {
                    Index.State &= ~Structures.States.Saved;
                    Toggle.UnsavedMark(Index.TabItem, true);
                }
                Index.State |= Structures.States.Changed;
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
            Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Line].Content = Core.Properties.Resources.Line + ": " + Area.Line;
            Core.Controller.Main.StatusBarItems[(int)Structures.StatusBar.Column].Content = Core.Properties.Resources.Column + ": " + Area.Column;
        }
    }
}
