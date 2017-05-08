namespace JAO_PI.Core.Utility
{
    public static class GoTo
    {
        public static void RefreshLineOffset()
        {
            if (Controller.Goto.Line.IsChecked.Value == true)
            {
                Controller.Goto.MaxLineLabel.Content = Controller.Main.CurrentEditor.Document.LineCount.ToString();
                Controller.Goto.LineLabel.Content = Controller.Main.CurrentEditor.TextArea.Caret.Line.ToString();
            }
            else
            {
                Controller.Goto.MaxLineLabel.Content = Controller.Main.CurrentEditor.Document.TextLength.ToString();
                Controller.Goto.LineLabel.Content = Controller.Main.CurrentEditor.TextArea.Caret.Offset.ToString();
            }
        }
    }
}
