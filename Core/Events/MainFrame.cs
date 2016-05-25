using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace JAO_PI.Core.Events
{
    class MainFrame
    {
        Classes.Generator generator = null;

        internal void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.CompilerPath.Length == 0)
            {
                MessageBoxResult result = MessageBox.Show("There is no Compiler path set. Do you want to set it now?", "JAO PI", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    OpenFileDialog CompilerPathDialog = new OpenFileDialog();
                    CompilerPathDialog = new OpenFileDialog();
                    CompilerPathDialog.Filter = "PAWN Compiler (pawncc.exe)|pawncc.exe";
                    CompilerPathDialog.Title = "Set Compiler Path ...";
                    CompilerPathDialog.InitialDirectory = Environment.CurrentDirectory;
                    if (CompilerPathDialog.ShowDialog() == true)
                    {
                        Properties.Settings.Default.CompilerPath = CompilerPathDialog.FileName;
                        MessageBox.Show("Path set", "JAO PI");
                        Properties.Settings.Default.Save();
                    }
                }
            }
            else
            {
                MessageBox.Show(Properties.Settings.Default.CompilerPath);
            }
            MainMenu main = new MainMenu();
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
        }
    }
}
