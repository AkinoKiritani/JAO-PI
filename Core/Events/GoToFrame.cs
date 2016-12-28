using JAO_PI.Core.Controller;
using JAO_PI.Core.Utility;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    public class GoToFrame
    {
        Functions ut = new Functions();
        public void Closing(object sender, CancelEventArgs e)
        {
            Main.Frames[(int)Structures.Frames.GoToFrame].Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }
        public void GoNowhere_Click(object sender, RoutedEventArgs e)
        {
            Main.Frames[(int)Structures.Frames.GoToFrame].Visibility = Visibility.Collapsed;
        }

        public void Activated(object sender, EventArgs e)
        {
            if (Main.Line.IsChecked.Value == true)
            {
                Main.MaxLineLabel.Content = Main.CurrentEditor.Document.LineCount.ToString();
                Main.LineLabel.Content = Main.CurrentEditor.TextArea.Caret.Line.ToString();
            }
            else
            {
                Main.MaxLineLabel.Content = Main.CurrentEditor.Document.TextLength.ToString();
                Main.LineLabel.Content = Main.CurrentEditor.TextArea.Caret.Offset.ToString();
            }
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Controls.TextBox GoToBox = sender as System.Windows.Controls.TextBox;
            if (System.Text.RegularExpressions.Regex.IsMatch(GoToBox.Text, "\\d+") == false)
            {
                e.Handled = true;
            }
        }

        public void GoTo_Click(object sender, RoutedEventArgs e)
        {
            if (Main.GoToBox.Text.Length > 0)
            {
                int GoToValue = Convert.ToInt32(Main.GoToBox.Text);
                Main.Frames[(int)Structures.Frames.GoToFrame].Visibility = Visibility.Collapsed;
                Main.Frames[(int)Structures.Frames.MainFrame].Activate();
                if (Main.Line.IsChecked.Value == true)
                {
                    if (GoToValue > Main.CurrentEditor.Document.LineCount)
                    {
                        Main.CurrentEditor.TextArea.Caret.Line = Main.CurrentEditor.Document.LineCount;
                        Main.CurrentEditor.TextArea.Caret.VisualColumn = Main.CurrentEditor.Document.LineCount;
                        Main.CurrentEditor.ScrollToLine(Main.CurrentEditor.Document.LineCount);
                    }
                    else
                    {
                        Main.CurrentEditor.TextArea.Caret.Line = GoToValue;
                        Main.CurrentEditor.TextArea.Caret.VisualColumn = GoToValue;
                        Main.CurrentEditor.ScrollToLine(GoToValue);
                    }
                }
                else
                {
                    if (GoToValue > Main.CurrentEditor.Document.TextLength)
                    {
                        Main.CurrentEditor.TextArea.Caret.Line = Main.CurrentEditor.Document.TextLength;
                        Main.CurrentEditor.TextArea.Caret.VisualColumn = Main.CurrentEditor.Document.TextLength;
                        Main.CurrentEditor.ScrollToLine(Main.CurrentEditor.Document.LineCount);
                    }
                    else
                    {
                        Main.CurrentEditor.TextArea.Caret.Offset = GoToValue;
                        Main.CurrentEditor.TextArea.Caret.VisualColumn = GoToValue;
                        Main.CurrentEditor.ScrollToLine(GoToValue);
                    }
                }
            }
        }

        public void Line_Checked(object sender, RoutedEventArgs e)
        {
            Main.MaxLineLabel.Content = Main.CurrentEditor.Document.LineCount.ToString();
            Main.LineLabel.Content = Main.CurrentEditor.TextArea.Caret.Line.ToString();
        }

        public void Offset_Checked(object sender, RoutedEventArgs e)
        {
            Main.MaxLineLabel.Content = Main.CurrentEditor.Document.TextLength.ToString();
            Main.LineLabel.Content = Main.CurrentEditor.TextArea.Caret.Offset.ToString();
        }        
    }
}
