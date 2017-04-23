namespace JAO_PI.Core.Utility
{
    public static class GoTo
    {
        public static void RefreshLineOffset()
        {
            if (Controller.Search.Line.IsChecked.Value == true)
            {
                Controller.Search.MaxLineLabel.Content = Controller.Main.CurrentEditor.Document.LineCount.ToString();
                Controller.Search.LineLabel.Content = Controller.Main.CurrentEditor.TextArea.Caret.Line.ToString();
            }
            else
            {
                Controller.Search.MaxLineLabel.Content = Controller.Main.CurrentEditor.Document.TextLength.ToString();
                Controller.Search.LineLabel.Content = Controller.Main.CurrentEditor.TextArea.Caret.Offset.ToString();
            }
        }
    }
}
