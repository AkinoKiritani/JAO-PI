using ICSharpCode.AvalonEdit;
using JAO_PI.Core.Controller;
using System.ComponentModel;
using System.Windows;
using JAO_PI.Core.Classes;
using System;
using System.Windows.Input;

namespace JAO_PI.EventsManager
{
    public class GoToFrame
    {
        Utility ut = new Utility();
        public void Closing(object sender, CancelEventArgs e)
        {
            Main.Frames[(int)Utility.Frames.GoToFrame].Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }
        public void GoNowhere_Click(object sender, RoutedEventArgs e)
        {
            Main.Frames[(int)Utility.Frames.GoToFrame].Visibility = Visibility.Collapsed;
        }

        public void Activated(object sender, EventArgs e)
        {
            TextEditor ed = ut.GetTextEditor(Main.tabControl.SelectedIndex);
            if (Main.Line.IsChecked.Value == true)
            {
                Main.MaxLineLabel.Content = ed.Document.LineCount.ToString();
                Main.LineLabel.Content = ed.TextArea.Caret.Line.ToString();
            }
            else
            {
                Main.MaxLineLabel.Content = ed.Document.TextLength.ToString();
                Main.LineLabel.Content = ed.TextArea.Caret.Offset.ToString();
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
                TextEditor ed = ut.GetTextEditor(Main.tabControl.SelectedIndex);
                int GoToValue = Convert.ToInt32(Main.GoToBox.Text);
                Main.Frames[(int)Utility.Frames.GoToFrame].Visibility = Visibility.Collapsed;
                Main.Frames[(int)Utility.Frames.MainFrame].Activate();
                if (Main.Line.IsChecked.Value == true)
                {
                    if (GoToValue > ed.Document.LineCount)
                    {
                        ed.TextArea.Caret.Line = ed.Document.LineCount;
                        ed.TextArea.Caret.VisualColumn = ed.Document.LineCount;
                        ed.ScrollToLine(ed.Document.LineCount);
                    }
                    else
                    {
                        ed.TextArea.Caret.Line = GoToValue;
                        ed.TextArea.Caret.VisualColumn = GoToValue;
                        ed.ScrollToLine(GoToValue);
                    }
                }
                else
                {
                    if (GoToValue > ed.Document.TextLength)
                    {
                        ed.TextArea.Caret.Line = ed.Document.TextLength;
                        ed.TextArea.Caret.VisualColumn = ed.Document.TextLength;
                        ed.ScrollToLine(ed.Document.LineCount);
                    }
                    else
                    {
                        ed.TextArea.Caret.Offset = GoToValue;
                        ed.TextArea.Caret.VisualColumn = GoToValue;
                        ed.ScrollToLine(GoToValue);
                    }
                }
            }
        }

        public void Line_Checked(object sender, RoutedEventArgs e)
        {
            TextEditor ed = ut.GetTextEditor(Main.tabControl.SelectedIndex);
            Main.MaxLineLabel.Content = ed.Document.LineCount.ToString();
            Main.LineLabel.Content = ed.TextArea.Caret.Line.ToString();
        }

        public void Offset_Checked(object sender, RoutedEventArgs e)
        {
            TextEditor ed = ut.GetTextEditor(Main.tabControl.SelectedIndex);
            Main.MaxLineLabel.Content = ed.Document.TextLength.ToString();
            Main.LineLabel.Content = ed.TextArea.Caret.Offset.ToString();
        }        
    }
}
