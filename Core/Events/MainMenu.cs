using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JAO_PI.Core.Events
{
    class MainMenu
    {
        Classes.Generator generator = null;
        private OpenFileDialog openFileDialog = null;

        internal void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            generator = new Classes.Generator();
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.GetLength(0) > 1)
            {
                string[] arg = arguments[1].Split('\\');
                TabItem tab = generator.TabItem(arguments[1], arg[arg.Length - 1], File.ReadAllText(arguments[1], System.Text.Encoding.Default));

                Controller.Main.tabControl.Items.Add(tab);
                Controller.Main.tabControl.SelectedItem = tab;

                Controller.Main.Empty_Message.Visibility = Visibility.Hidden;
                Controller.Main.Empty_Message.IsEnabled = false;

                Controller.Main.tabControl.Visibility = Visibility.Visible;
                if (Controller.Main.tabControl.Items.Count == 1)
                {
                    Controller.Main.ToggleSaveOptions(true);
                }
            }

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PAWN Files (*.inc, *.pwn)|*.inc;*.pwn|Include Files (*.inc)|*.inc|Only Pawn Files (*.pwn)|*.pwn|All files (*.*)|*.*";
            openFileDialog.Title = "Open PAWN File...";
            //Worker = new Controller.Worker();
        }

        internal void Restore_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Restore clicked");
        }

        internal void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.Main.tabControl.Items.Count > 0 && Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                Controller.Main.SaveTab(Controller.Main.tabControl.Items[Controller.Main.tabControl.SelectedIndex] as TabItem);
                MessageBox.Show("kek");
            }
        }

        internal void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.Main.tabControl.Items.Count > 0 && Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.Filter = "Only Pawn File (*.pwn)|*.pwn|Include File (*.inc)|*.inc|All files (*.*)|*.*";
                saveFileDialog.Title = "Save PAWN File...";
                if (saveFileDialog.ShowDialog() == true)
                {
                    TabItem Tab = Controller.Main.tabControl.Items[Controller.Main.tabControl.SelectedIndex] as TabItem;
                    Controller.Main.SaveTab(Tab, saveFileDialog);
                    Tab.Header = saveFileDialog.SafeFileName;
                }
            }
        }

        internal void Close_File_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.Main.tabControl.Items.Count == 0)
            {
                Controller.Main.ToggleSaveOptions(false);

                Controller.Main.Empty_Message.IsEnabled = true;
                Controller.Main.Empty_Message.Visibility = Visibility.Visible;
            }
        }

        internal void Cut_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Cut clicked");
        }

        internal void Copy_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Copy clicked");
        }

        internal void Paste_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Paste clicked");
        }

        internal void Compile_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process Compiler = new Process();
            ProcessStartInfo StartInfo = new ProcessStartInfo();

            TabItem itemToCompile = Controller.Main.tabControl.Items[Controller.Main.tabControl.SelectedIndex] as TabItem;
            Compiler.StartInfo = new ProcessStartInfo()
            {
                FileName = "G:/Desk/PrP/pawno/pawncc.exe",
                WorkingDirectory = itemToCompile.Uid,
                Arguments = itemToCompile.Header.ToString(),
                CreateNoWindow = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };
            Compiler.Start();
            Compiler.WaitForExit();

            string errors = Compiler.StandardError.ReadToEnd();
            if (errors.Length == 0)
            {
                MessageBox.Show("Erfolg ! :)");
            }
            else
            {
                MessageBox.Show(errors);
            }
        }


        internal void Create_File_Click(object sender, RoutedEventArgs e)
        {
            TabItem tab = generator.TabItem(Environment.CurrentDirectory, "new.pwn", null);

            Controller.Main.tabControl.Items.Add(tab);
            Controller.Main.tabControl.SelectedItem = tab;

            Controller.Main.Empty_Message.Visibility = Visibility.Hidden;
            Controller.Main.Empty_Message.IsEnabled = false;

            Controller.Main.tabControl.Visibility = Visibility.Visible;

            if (Controller.Main.tabControl.Items.Count == 1)
            {
                Controller.Main.ToggleSaveOptions(true);
            }
        }


        internal void Open_File_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                TabItem tab = generator.TabItem(openFileDialog.FileName, openFileDialog.SafeFileName, File.ReadAllText(openFileDialog.FileName, System.Text.Encoding.Default));
                Controller.Main.tabControl.Items.Add(tab);
                Controller.Main.tabControl.SelectedItem = tab;

                Controller.Main.Empty_Message.Visibility = Visibility.Hidden;
                Controller.Main.Empty_Message.IsEnabled = false;

                Controller.Main.tabControl.Visibility = Visibility.Visible;

                if (Controller.Main.tabControl.Items.Count == 1)
                {
                    Controller.Main.ToggleSaveOptions(true);
                }
            }
        }
    }
}
