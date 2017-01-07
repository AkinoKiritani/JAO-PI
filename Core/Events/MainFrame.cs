using JAO_PI.Core.Classes;
using JAO_PI.Core.Utility;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace JAO_PI.EventsManager
{
    public class MainFrame
    {
        public void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            Functions utility = new Functions();
            Core.Controller.Register.SetFrameAsOwner(Core.Controller.Main.Frames[(int)Structures.Frames.MainFrame]);

            if (Core.Properties.Settings.Default.CompilerPath.Length == 0 || File.Exists(Core.Properties.Settings.Default.CompilerPath) == false)
            {
                
                MessageBoxResult result = MessageBox.Show(Core.Properties.Resources.NoCompilerPath, Core.Properties.Resources.ProgName, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    OpenFileDialog CompilerPathDialog = new OpenFileDialog();
                    CompilerPathDialog = new OpenFileDialog();
                    CompilerPathDialog.Filter = Core.Properties.Resources.PathFilter;
                    CompilerPathDialog.Title = Core.Properties.Resources.SetPath;
                    CompilerPathDialog.InitialDirectory = Environment.CurrentDirectory;
                    if (CompilerPathDialog.ShowDialog() == true)
                    {
                        Core.Properties.Settings.Default.CompilerPath = CompilerPathDialog.FileName;
                        MessageBox.Show(Core.Properties.Resources.PathSet, Core.Properties.Resources.ProgName, MessageBoxButton.OK, MessageBoxImage.Information);
                        Core.Properties.Settings.Default.Save();
                    }
                    GC.ReRegisterForFinalize(CompilerPathDialog);
                }
            }

            MainMenu main = new MainMenu();
            Generator generator = new Generator();
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.GetLength(0) > 1)
            {
                string[] arg = arguments[1].Split('\\');

                FileStream stream = new FileStream(arguments[1], FileMode.Open, FileAccess.Read);
                TabItem tab = generator.TabItem(arguments[1], arg[arg.Length - 1], stream);

                Core.Controller.Main.tabControl.Items.Add(tab);
                Core.Controller.Main.tabControl.SelectedItem = tab;

                Toggle.TabControl(true);
                if (Core.Controller.Main.tabControl.Items.Count == 1)
                {
                    Toggle.SaveOptions(true);
                }
                Core.Controller.Main.CompileMenuItem.IsEnabled = true;
            }
        }

        public void MainFrame_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public void MainFrame_Closing(object sender, CancelEventArgs e)
        {
            List<Core.Controller.Tab> notSavedList = Core.Controller.Main.TabControlList.FindAll(x => x.Editor.Document.FileName.Contains(".JAOnotsaved"));
            if (notSavedList.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show(Core.Properties.Resources.NotSaved, Core.Properties.Resources.ProgName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    StringBuilder FileToSave = new StringBuilder();

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.OverwritePrompt = true;
                    saveFileDialog.Filter = Core.Properties.Resources.FileFilter;
                    saveFileDialog.Title = Core.Properties.Resources.SaveFile;
                    Functions utility = new Functions();
                    string HeaderText = null;
                    for (int i = 0; i != notSavedList.Count; i++)
                    {
                        HeaderText = utility.GetTabHeaderText(notSavedList[i].TabItem);
                        FileToSave.Clear();
                        FileToSave.Append(notSavedList[i].TabItem.Uid);
                        FileToSave.Append(HeaderText);
                        string[] arg = FileToSave.ToString().Split('\\');
                        saveFileDialog.InitialDirectory = notSavedList[i].TabItem.Uid;
                        saveFileDialog.FileName = HeaderText;

                        if (HeaderText.Equals(arg[arg.Length - 1]))
                        {
                            if (File.Exists(FileToSave.ToString()))
                            {
                                StringBuilder OverwriteMessage = new StringBuilder();
                                OverwriteMessage.Append(Core.Properties.Resources.OverwriteSave);
                                OverwriteMessage.Append(saveFileDialog.FileName);
                                OverwriteMessage.Append(Core.Properties.Resources.OverwriteSaveEnd);

                                result = MessageBox.Show(OverwriteMessage.ToString(), Core.Properties.Resources.ProgName, MessageBoxButton.YesNo, MessageBoxImage.Stop);
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
