namespace JAO_PI.Core.Utility
{
    public static class GoTo
    {
        public static void RefreshLineOffset()
        {
            if (Controller.Main.Line.IsChecked.Value == true)
            {
                Controller.Main.MaxLineLabel.Content = Controller.Main.CurrentEditor.Document.LineCount.ToString();
                Controller.Main.LineLabel.Content = Controller.Main.CurrentEditor.TextArea.Caret.Line.ToString();
            }
            else
            {
                Controller.Main.MaxLineLabel.Content = Controller.Main.CurrentEditor.Document.TextLength.ToString();
                Controller.Main.LineLabel.Content = Controller.Main.CurrentEditor.TextArea.Caret.Offset.ToString();
            }
        }
    }
}
