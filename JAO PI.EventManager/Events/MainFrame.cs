using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace JAO_PI.EventsManager.MainFrame
{
    public class MainFrame
    {
        JAO_PI.Core.Classes.Generator generator = null;

        public void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            if(JAO_PI.Properties.Settings.Default.CompilerPath.Length == 0)
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
            generator = new Core.Classes.Generator();
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.GetLength(0) > 1)
            {
                string[] arg = arguments[1].Split('\\');
                TabItem tab = generator.TabItem(arguments[1], arg[arg.Length - 1], File.ReadAllText(arguments[1], System.Text.Encoding.Default));

                Core.Controller.Main.tabControl.Items.Add(tab);
                Core.Controller.Main.tabControl.SelectedItem = tab;

                Core.Controller.Main.Empty_Message.Visibility = Visibility.Hidden;
                Core.Controller.Main.Empty_Message.IsEnabled = false;

                Core.Controller.Main.tabControl.Visibility = Visibility.Visible;
                if (Core.Controller.Main.tabControl.Items.Count == 1)
                {
                    Core.Controller.Main.ToggleSaveOptions(true);
                }
            }
        }
    }
}
