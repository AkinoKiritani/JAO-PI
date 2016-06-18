using ICSharpCode.AvalonEdit;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace JAO_PI.EventsManager
{
    public class Worker
    {
        internal static void Compiler_DoWork(object sender, DoWorkEventArgs e)
        {
            Core.Controller.Main.Compile.Dispatcher.Invoke(new Action(() =>
            {
                Core.Controller.Main.Compile.Visibility = Visibility.Visible;
            }));
            
            if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                try
                {
                    Process Compiler = new Process();
                    ProcessStartInfo StartInfo = new ProcessStartInfo();

                    TabItem itemToCompile = null;
                    string Header = null;
                    string uID = null;

                    Core.Controller.Main.tabControl.Items.Dispatcher.Invoke(new Action(() =>
                    {
                        itemToCompile = Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem;
                        Header = itemToCompile.Header.ToString();
                        uID = itemToCompile.Uid;
                    }));

                    Compiler.StartInfo = new ProcessStartInfo()
                    {
                        FileName = Core.Properties.Settings.Default.CompilerPath,
                        WorkingDirectory = uID,
                        Arguments = Header,
                        CreateNoWindow = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    };

                    Compiler.Start();
                    Compiler.WaitForExit();

                    Core.Controller.Main.Compiler_Errors = Compiler.StandardError.ReadToEnd();
                    Compiler.Dispose();
                    
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.ToString(), "JAO PI", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else MessageBox.Show("Nothing to compile", "JAO PI", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        internal static void Compiler_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                if(Core.Controller.Main.Compiler_Errors != null)
                {
                    if (Core.Controller.Main.Compiler_Errors.Length == 0)
                    {
                        MessageBox.Show("No Errors ! :)", "JAO PI", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show(Core.Controller.Main.Compiler_Errors, "JAO PI", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                Core.Controller.Main.Compile.Visibility = Visibility.Collapsed;
            }
        }

        internal static void Save_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {                    
                if (Core.Controller.Main.tabControl.Items.Count > 0 && Core.Controller.Main.tabControl.Visibility == Visibility.Visible)
                {
                    TabItem itemToSave = null;
                    Grid SaveGrid = null;
                    TextEditor SaveEditor = null;
                    string Header = null;
                    string uID = null;
                    Core.Controller.Main.tabControl.Items.Dispatcher.Invoke(new Action(() =>
                    {
                        itemToSave = Core.Controller.Main.tabControl.Items[Core.Controller.Main.tabControl.SelectedIndex] as TabItem;
                        SaveGrid = itemToSave.Content as Grid;
                        SaveEditor = SaveGrid.Children[0] as TextEditor;
                        Header = itemToSave.Header.ToString();
                        uID = itemToSave.Uid;
                    }));

                    System.Text.StringBuilder FileToSave = new System.Text.StringBuilder();
                    FileToSave.Append(uID);
                    FileToSave.Append(Header);

                    SaveEditor.Dispatcher.Invoke(new Action(() => SaveEditor.Save(FileToSave.ToString())));
                    
                    SaveEditor = null;
                    SaveGrid = null;
                    itemToSave = null;
                    FileToSave = null;                    
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString(), "JAO PI", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal static void Save_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Core.Controller.Worker.CompileWorker.RunWorkerAsync();
        }
    }
}