using System.Globalization;

namespace JAO_PI.Core.Utility
{
    public static class GoTo
    {
        public static void RefreshLineOffset()
        {
            if (Controller.Goto.Line.IsChecked.Value == true)
            {
                Controller.Goto.MaxLineLabel.Content = Controller.Main.CurrentEditor.Document.LineCount.ToString(CultureInfo.InvariantCulture.NumberFormat);
                Controller.Goto.LineLabel.Content = Controller.Main.CurrentEditor.TextArea.Caret.Line.ToString(CultureInfo.InvariantCulture.NumberFormat);
            }
            else
            {
                Controller.Goto.MaxLineLabel.Content = Controller.Main.CurrentEditor.Document.TextLength.ToString(CultureInfo.InvariantCulture.NumberFormat);
                Controller.Goto.LineLabel.Content = Controller.Main.CurrentEditor.TextArea.Caret.Offset.ToString(CultureInfo.InvariantCulture.NumberFormat);
            }
        }
    }
}
