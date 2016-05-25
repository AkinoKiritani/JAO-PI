using ICSharpCode.AvalonEdit;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace JAO_PI.Core.Events
{
    class Worker
    {
        internal static void Compiler_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Controller.Main.tabControl.Items.Count > 0 && Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                try
                { 
                    Process Compiler = new Process();
                    ProcessStartInfo StartInfo = new ProcessStartInfo();

                    TabItem itemToCompile = null;
                    string Header = null;
                    string uID = null;

                    Controller.Main.tabControl.Items.Dispatcher.Invoke(new Action(() =>
                    {
                        itemToCompile = Controller.Main.tabControl.Items[Controller.Main.tabControl.SelectedIndex] as TabItem;
                        Header = itemToCompile.Header.ToString();
                        uID = itemToCompile.Uid;
                    }));

                    Compiler.StartInfo = new ProcessStartInfo()
                    {
                        FileName = Properties.Settings.Default.CompilerPath,
                        WorkingDirectory = uID,
                        Arguments = Header,
                        CreateNoWindow = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    };

                    Compiler.Start();
                    Compiler.WaitForExit();

                    Controller.Main.Compiler_Errors = Compiler.StandardError.ReadToEnd();
                    Compiler.Dispose();
                    
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
            }
            else MessageBox.Show("Nothing to compile");
        }

        internal static void Compiler_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Controller.Main.tabControl.Items.Count > 0 && Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                if(Controller.Main.Compiler_Errors != null)
                {
                    if (Controller.Main.Compiler_Errors.Length == 0)
                    {
                        MessageBox.Show("No Errors ! :)");
                    }
                    else
                    {
                        MessageBox.Show(Controller.Main.Compiler_Errors);
                    }
                }
            }
        }

        internal static void Save_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {                    
                if (Controller.Main.tabControl.Items.Count > 0 && Controller.Main.tabControl.Visibility == Visibility.Visible)
                {
                    TabItem itemToSave = null;
                    Grid SaveGrid = null;
                    TextEditor SaveEditor = null;
                    string Header = null;
                    string uID = null;
                    Controller.Main.tabControl.Items.Dispatcher.Invoke(new Action(() =>
                    {
                        itemToSave = Controller.Main.tabControl.Items[Controller.Main.tabControl.SelectedIndex] as TabItem;
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
                MessageBox.Show(ee.ToString());
            }
        }

        internal static void Save_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Controller.Worker.CompileWorker.RunWorkerAsync();
        }
    }
}