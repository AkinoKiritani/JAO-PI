using System;
using System.Windows;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    public class GoToFrame
    {
        public void GoNowhere_Click(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
        }

        public void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void GoTo_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Main.GoToBox.Text.Length > 0)
            {
                int GoToValue = Convert.ToInt32(Core.Controller.Main.GoToBox.Text);
                Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
                Core.Controller.Main.Frames[(int)Structures.Frames.MainFrame].Activate();
                if (Core.Controller.Main.Line.IsChecked.Value == true)
                {
                    if (GoToValue > Core.Controller.Main.CurrentEditor.Document.LineCount)
                    {
                        Core.Controller.Main.CurrentEditor.TextArea.Caret.Line = Core.Controller.Main.CurrentEditor.Document.LineCount;
                        Core.Controller.Main.CurrentEditor.TextArea.Caret.VisualColumn = Core.Controller.Main.CurrentEditor.Document.LineCount;
                        Core.Controller.Main.CurrentEditor.ScrollToLine(Core.Controller.Main.CurrentEditor.Document.LineCount);
                    }
                    else
                    {
                        Core.Controller.Main.CurrentEditor.TextArea.Caret.Line = GoToValue;
                        Core.Controller.Main.CurrentEditor.TextArea.Caret.VisualColumn = GoToValue;
                        Core.Controller.Main.CurrentEditor.ScrollToLine(GoToValue);
                    }
                }
                else
                {
                    if (GoToValue > Core.Controller.Main.CurrentEditor.Document.TextLength)
                    {
                        Core.Controller.Main.CurrentEditor.TextArea.Caret.Line = Core.Controller.Main.CurrentEditor.Document.TextLength;
                        Core.Controller.Main.CurrentEditor.TextArea.Caret.VisualColumn = Core.Controller.Main.CurrentEditor.Document.TextLength;
                        Core.Controller.Main.CurrentEditor.ScrollToLine(Core.Controller.Main.CurrentEditor.Document.LineCount);
                    }
                    else
                    {
                        Core.Controller.Main.CurrentEditor.TextArea.Caret.Offset = GoToValue;
                        Core.Controller.Main.CurrentEditor.TextArea.Caret.VisualColumn = GoToValue;
                        Core.Controller.Main.CurrentEditor.ScrollToLine(GoToValue);
                    }
                }
            }
        }

        public void Line_Checked(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.MaxLineLabel.Content = Core.Controller.Main.CurrentEditor.Document.LineCount.ToString();
            Core.Controller.Main.LineLabel.Content = Core.Controller.Main.CurrentEditor.TextArea.Caret.Line.ToString();
        }

        public void Offset_Checked(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.MaxLineLabel.Content = Core.Controller.Main.CurrentEditor.Document.TextLength.ToString();
            Core.Controller.Main.LineLabel.Content = Core.Controller.Main.CurrentEditor.TextArea.Caret.Offset.ToString();
        }        
    }
}
