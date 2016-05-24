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
                        FileName = "G:/Desk/PrP/pawno/pawncc.exe",
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

        internal static void Save_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal static void Save_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}