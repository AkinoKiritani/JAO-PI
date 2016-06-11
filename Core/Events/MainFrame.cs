using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Generic;
using JAO_PI.Core.Classes;
using System.Text;

namespace JAO_PI.EventsManager
{
    public class MainFrame
    {
        Generator generator = null;
        Utility utility = null;
        public void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            utility = new Utility();
            if (Core.Properties.Settings.Default.CompilerPath.Length == 0 || File.Exists(Core.Properties.Settings.Default.CompilerPath) == false)
            {
                MessageBoxResult result = MessageBox.Show("There is no Compiler path set. Do you want to set it now?", "JAO PI", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    OpenFileDialog CompilerPathDialog = new OpenFileDialog();
                    CompilerPathDialog = new OpenFileDialog();
                    CompilerPathDialog.Filter = "PAWN Compiler (pawncc.exe)|pawncc.exe";
                    CompilerPathDialog.Title = "Set Compiler Path ...";
                    CompilerPathDialog.InitialDirectory = Environment.CurrentDirectory;
                    if (CompilerPathDialog.ShowDialog() == true)
                    {
                        Core.Properties.Settings.Default.CompilerPath = CompilerPathDialog.FileName;
                        MessageBox.Show("Path set", "JAO PI", MessageBoxButton.OK, MessageBoxImage.Information);
                        Core.Properties.Settings.Default.Save();
                    }
                    GC.ReRegisterForFinalize(CompilerPathDialog);
                }
            }

            MainMenu main = new MainMenu();
            generator = new Generator();
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.GetLength(0) > 1)
            {
                string[] arg = arguments[1].Split('\\');

                FileStream stream = new FileStream(arguments[1], FileMode.Open, FileAccess.Read);
                TabItem tab = generator.TabItem(arguments[1], arg[arg.Length - 1], stream);

                Core.Controller.Main.tabControl.Items.Add(tab);
                Core.Controller.Main.tabControl.SelectedItem = tab;

                Core.Controller.Main.Empty_Message.Visibility = Visibility.Hidden;
                Core.Controller.Main.Empty_Message.IsEnabled = false;

                Core.Controller.Main.tabControl.Visibility = Visibility.Visible;
                if (Core.Controller.Main.tabControl.Items.Count == 1)
                {
                    utility.ToggleSaveOptions(true);
                }
            }
        }

        public void MainFrame_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public void MainFrame_Closing(object sender, CancelEventArgs e)
        {
            List<Core.Controller.Tab> notSavedList = Core.Controller.Main.TabControlList.FindAll(x => x.Editor.Document.FileName.Contains(".JAOnotsaved"));
            if(notSavedList.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Some files are not saved. Do you want to save them now ?", "JAO PI", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    StringBuilder FileToSave = new StringBuilder();
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.OverwritePrompt = true;
                    saveFileDialog.Filter = "Only Pawn File (*.pwn)|*.pwn|Include File (*.inc)|*.inc|All files (*.*)|*.*";
                    saveFileDialog.Title = "Save PAWN File...";
                    for (int i = 0; i != notSavedList.Count; i++)
                    {
                        FileToSave.Clear();
                        FileToSave.Append(notSavedList[i].TabItem.Uid);
                        FileToSave.Append(notSavedList[i].TabItem.Header);
                        string[] arg = FileToSave.ToString().Split('\\');
                        saveFileDialog.InitialDirectory = notSavedList[i].TabItem.Uid;
                        saveFileDialog.FileName = notSavedList[i].TabItem.Header.ToString();
                        if (notSavedList[i].TabItem.Header.Equals(arg[arg.Length - 1]))
                        {
                            if (File.Exists(FileToSave.ToString()))
                            {
                                result = MessageBox.Show("Do you want to overwrite " + notSavedList[i].TabItem.Header.ToString() +" ?", "JAO PI", MessageBoxButton.YesNo, MessageBoxImage.Stop);
                                if (result == MessageBoxResult.Yes)
                                {
                                    utility.SaveTab(notSavedList[i].TabItem);
                                }
                                else
                                {
                                    if (saveFileDialog.ShowDialog() == true)
                                    {
                                        utility.SaveTab(notSavedList[i].TabItem, saveFileDialog);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (saveFileDialog.ShowDialog() == true)
                            {
                                utility.SaveTab(notSavedList[i].TabItem, saveFileDialog);
                            }
                        }
                    }   
                }
            }
        }
    }
}
