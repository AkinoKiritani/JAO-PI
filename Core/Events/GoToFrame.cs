using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    public class GoToFrame
    {
        public void GoNowhereClick(object sender, RoutedEventArgs e)
        {
            Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
        }

        public void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e != null)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        public void GoToClick(object sender, RoutedEventArgs e)
        {
            if (Core.Controller.Goto.GoToBox.Text.Length > 0)
            {
                int GoToValue = Convert.ToInt32(Core.Controller.Goto.GoToBox.Text, CultureInfo.InvariantCulture.NumberFormat);
                Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Collapsed;
                Core.Controller.Main.Frames[(int)Structures.Frames.MainFrame].Activate();
                if (Core.Controller.Goto.Line.IsChecked.Value == true)
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

        public void LineChecked(object sender, RoutedEventArgs e)
        {
            Core.Controller.Goto.MaxLineLabel.Content = Core.Controller.Main.CurrentEditor.Document.LineCount.ToString(CultureInfo.InvariantCulture.NumberFormat);
            Core.Controller.Goto.LineLabel.Content = Core.Controller.Main.CurrentEditor.TextArea.Caret.Line.ToString(CultureInfo.InvariantCulture.NumberFormat);
        }

        public void OffsetChecked(object sender, RoutedEventArgs e)
        {
            Core.Controller.Goto.MaxLineLabel.Content = Core.Controller.Main.CurrentEditor.Document.TextLength.ToString(CultureInfo.InvariantCulture.NumberFormat);
            Core.Controller.Goto.LineLabel.Content = Core.Controller.Main.CurrentEditor.TextArea.Caret.Offset.ToString(CultureInfo.InvariantCulture.NumberFormat);
        }        
    }
}
