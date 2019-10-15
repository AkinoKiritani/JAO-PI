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
        //private Data.Connector Data = null;
        public void MainFrameLoaded(object sender, RoutedEventArgs e)
        {
            Core.Controller.Register.SetFrameAsOwner(Core.Controller.Main.Frames[(int)Structures.Frames.MainFrame]);
            Core.Controller.Worker Worker = new Core.Controller.Worker();

            /*Data = new Data.Connector(Core.Properties.Resources.IncludesDataBase);
            int res = Data.Open().Result;
            if(res != null)
            {
                // do stuff
            }*/
            if (Core.Properties.Settings.Default.CompilerPath.Length == 0 || File.Exists(Core.Properties.Settings.Default.CompilerPath) == false)
            {
                MessageBoxResult result = MessageBox.Show(Core.Properties.Resources.NoCompilerPath, Core.Properties.Resources.ProgName, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    OpenFileDialog CompilerPathDialog = new OpenFileDialog()
                    {
                        Filter = Core.Properties.Resources.PathFilter,
                        Title = Core.Properties.Resources.SetPath,
                        InitialDirectory = Environment.CurrentDirectory
                    };
                    if (CompilerPathDialog.ShowDialog() == true)
                    {
                        Core.Properties.Settings.Default.CompilerPath = CompilerPathDialog.FileName;
                        MessageBox.Show(Core.Properties.Resources.PathSet, Core.Properties.Resources.ProgName, MessageBoxButton.OK, MessageBoxImage.Information);
                        Core.Properties.Settings.Default.Save();
                    }
                    GC.ReRegisterForFinalize(CompilerPathDialog);
                }
            }

            Generator generator = new Generator();
            string[] arguments = Environment.GetCommandLineArgs();
            TabItem tab;
            FileStream stream = null;
            if (arguments.GetLength(0) > 1)
            {
                string[] arg = arguments[1].Split('\\');
                tab = generator.TabItem(arguments[1], arg[arg.Length - 1], stream);
                stream = new FileStream(arguments[1], FileMode.Open, FileAccess.Read);
                stream.Close();
            }
            else tab = generator.TabItem(Environment.CurrentDirectory, Core.Properties.Resources.NewFileName, null);

            Core.Controller.Main.tabControl.Items.Add(tab);
            Core.Controller.Main.tabControl.SelectedItem = tab;

            Toggle.TabControl(true);
            if (Core.Controller.Main.tabControl.Items.Count == 1)
            {
                Toggle.SaveOptions(true);
            }
            Core.Controller.Main.CompileMenuItem.IsEnabled = true;
        }

        public void MainFrameDrop(object sender, DragEventArgs e)
        {
            Main.LoadDropData(e);
        }

        public void MainFrameActivated(object sender, EventArgs e)
        {
            if (Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].IsVisible)
            {
                Core.Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Opacity = 0.4;

                Border FrameBorder = Core.Controller.Search.Head.Children[(int)Structures.SearchHeader.FrameBorder] as Border;
                FrameBorder.BorderBrush = System.Windows.Media.Brushes.Transparent;
            }
        }

        public void MainFrameClosed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public void MainFrameClosing(object sender, CancelEventArgs e)
        {
            //Data.Close();

            List<Core.Controller.Tab> notSavedList = Core.Controller.Main.TabControlList.FindAll(x => !x.State.HasFlag(Structures.States.Saved));
            if (notSavedList.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show(Core.Properties.Resources.NotSaved, Core.Properties.Resources.ProgName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    StringBuilder FileToSave = new StringBuilder();

                    SaveFileDialog saveFileDialog = new SaveFileDialog()
                    {
                        OverwritePrompt = true,
                        Filter = Core.Properties.Resources.FileFilter,
                        Title = Core.Properties.Resources.SaveFile
                    };
                    string HeaderText = null;
                    for (int i = 0; i != notSavedList.Count; i++)
                    {
                        HeaderText = Tab.GetTabHeaderText(notSavedList[i].TabItem);
                        FileToSave.Clear();
                        FileToSave.Append(notSavedList[i].TabItem.Uid);
                        FileToSave.Append(HeaderText);
                        string[] arg = FileToSave.ToString().Split('\\');
                        saveFileDialog.InitialDirectory = notSavedList[i].TabItem.Uid;
                        saveFileDialog.FileName = HeaderText;

                        if (HeaderText.Equals(arg[arg.Length - 1], StringComparison.CurrentCulture))
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
                                    Tab.SaveTab(notSavedList[i].TabItem);
                                }
                                else
                                {
                                    if (saveFileDialog.ShowDialog() == true)
                                    {
                                        Tab.SaveTab(notSavedList[i].TabItem, saveFileDialog);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (saveFileDialog.ShowDialog() == true)
                            {
                                Tab.SaveTab(notSavedList[i].TabItem, saveFileDialog);
                            }
                        }
                    }
                }
            }
        }
    }
}
