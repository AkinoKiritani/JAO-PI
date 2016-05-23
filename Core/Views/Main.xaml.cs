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
        private Events.MainMenu Events;
        
        public Main()
        {
            Events = new Events.MainMenu();
            InitializeComponent();

            Controller.Main.RegisterTabControl(this.tabControl);
            Controller.Main.RegisterEmptyMessage(this.Empty_Message);
            Controller.Main.RegisterSaveOptions(this.Save, this.SaveAs, this.Close_File);

            RoutedCommand SaveCmd = new RoutedCommand();
            SaveCmd.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(SaveCmd, Events.Save_Click));

            MainFrame.Loaded += Events.MainFrame_Loaded;

            Restore.PreviewMouseLeftButtonUp += Events.Restore_PreviewMouseLeftButtonUp;
            Cut.PreviewMouseLeftButtonUp += Events.Cut_PreviewMouseLeftButtonUp;
            Copy.PreviewMouseLeftButtonUp += Events.Copy_PreviewMouseLeftButtonUp;
            Paste.PreviewMouseLeftButtonUp += Events.Paste_PreviewMouseLeftButtonUp;
            Compile.PreviewMouseLeftButtonUp += Events.Compile_PreviewMouseLeftButtonUp;

            Create_File.Click += Events.Create_File_Click;
            Open_File.Click += Events.Open_File_Click;
            Close_File.Click += Events.Close_File_Click;
            Save.Click += Events.Save_Click;
            SaveAs.Click += Events.SaveAs_Click;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
