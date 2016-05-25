using ICSharpCode.AvalonEdit;
using System.Windows.Controls;

namespace JAO_PI.Core.Classes
{
    class Utility
    {
        public TextEditor GetTextEditor(int index)
        {
            TabItem item = Controller.Main.tabControl.Items[index] as TabItem;
            Grid EditorGrid = item.Content as Grid;
            return EditorGrid.Children[0] as TextEditor;
        }
    }
}
