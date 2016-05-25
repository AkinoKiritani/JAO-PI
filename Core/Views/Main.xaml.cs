using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JAO_PI.Core.Views
{
    /// <summary>
    /// Interaktionslogik für Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private Events.MainFrame FrameEvents;
        private Events.MainMenu MenuEvents;
        private Controller.Worker Worker = null;

        public Main()
        {
            Worker = new Controller.Worker();
            FrameEvents = new Events.MainFrame();
            MenuEvents = new Events.MainMenu();
            InitializeComponent();

            Controller.Main.RegisterTabControl(this.tabControl);
            Controller.Main.RegisterEmptyMessage(this.Empty_Message);
            Controller.Main.RegisterSaveOptions(this.Save, this.SaveAs, this.Close_File);

            RoutedCommand SaveCmd = new RoutedCommand();
            SaveCmd.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(SaveCmd, MenuEvents.Save_Click));

            RoutedCommand CompileCmd = new RoutedCommand();
            CompileCmd.InputGestures.Add(new KeyGesture(Key.F5));
            CommandBindings.Add(new CommandBinding(CompileCmd, MenuEvents.Compile));

            MainFrame.Loaded += FrameEvents.MainFrame_Loaded;

            //Data
            Create_File.Click += MenuEvents.Create_File_Click;
            Open_File.Click += MenuEvents.Open_File_Click;
            Close_File.Click += MenuEvents.Close_File_Click;
            Save.Click += MenuEvents.Save_Click;
            SaveAs.Click += MenuEvents.SaveAs_Click;

            //Edit
            Restore.PreviewMouseLeftButtonUp += MenuEvents.Restore_PreviewMouseLeftButtonUp;
            Cut.PreviewMouseLeftButtonUp += MenuEvents.Cut_PreviewMouseLeftButtonUp;
            Copy.PreviewMouseLeftButtonUp += MenuEvents.Copy_PreviewMouseLeftButtonUp;
            Paste.PreviewMouseLeftButtonUp += MenuEvents.Paste_PreviewMouseLeftButtonUp;

            //Compier
            Compile.PreviewMouseLeftButtonUp += MenuEvents.Compile_PreviewMouseLeftButtonUp;
            Compiler_Path.PreviewMouseLeftButtonUp += MenuEvents.Compiler_Path_PreviewMouseLeftButtonUp;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
