using System.Windows;
using System.Windows.Input;

namespace JAO_PI.Views
{
    /// <summary>
    /// Interaktionslogik für Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            Core.Controller.Register.Frames(new Window[] { this, new Search(), new Credits() });
            EventsManager.MainFrame FrameEvents = new EventsManager.MainFrame();
            EventsManager.TabControl TabControllEvents = new EventsManager.TabControl();
            EventsManager.MainMenu MenuEvents = new EventsManager.MainMenu();
            EventsManager.SearchFrame SearchEvents = new EventsManager.SearchFrame();

            InitializeComponent();

            Core.Controller.Register.MainView(this.MainView);
            Core.Controller.Register.TabControl(this.tabControl);
            Core.Controller.Register.EmptyMessage(this.Message_Label);
            Core.Controller.Register.SaveOptions(this.MainMenu);
            Core.Controller.Register.Edit(this.Edit);
            Core.Controller.Register.StatusBar(this.Compiling, this.Line, this.Column);
            Core.Controller.Register.CompilePanel(this.CompilerPanel);

            // MainFrame
            this.Loaded         += FrameEvents.MainFrame_Loaded;
            this.Closed         += FrameEvents.MainFrame_Closed;
            this.Closing        += FrameEvents.MainFrame_Closing;
            // Event for transparency of the SearchFrame
            this.Activated      += FrameEvents.MainFrame_Activated;
            this.Drop           += FrameEvents.MainFrame_Drop;

            tabControl.SelectionChanged += TabControllEvents.SelectionChanged;

            // Data
            Create_File.Click   += MenuEvents.Create_File_Click;
            Open_File.Click     += MenuEvents.Open_File_Click;
            Close_File.Click    += MenuEvents.Close_File_Click;
            Close_All.Click     += MenuEvents.Close_All_Click;
            Save.Click          += MenuEvents.Save_Click;
            SaveAs.Click        += MenuEvents.SaveAs_Click;
            Exit.Click          += MenuEvents.Exit_Click;

            RoutedCommand SaveCmd = new RoutedCommand();
            SaveCmd.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(SaveCmd, MenuEvents.Save_Click));

            RoutedCommand NewFileCmd = new RoutedCommand();
            NewFileCmd.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(NewFileCmd, MenuEvents.Create_File_Click));

            // Edit
            Undo.Click          += MenuEvents.Undo_Click;
            Cut.Click           += MenuEvents.Cut_Click;
            Copy.Click          += MenuEvents.Copy_Click;
            Paste.Click         += MenuEvents.Paste_Click;

            // Find
            RoutedCommand FindCmd = new RoutedCommand();
            FindCmd.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(FindCmd, MenuEvents.Search));

            // Find Next
            RoutedCommand FindNextCmd = new RoutedCommand();
            FindNextCmd.InputGestures.Add(new KeyGesture(Key.F3));
            CommandBindings.Add(new CommandBinding(FindNextCmd, MenuEvents.FindNext));

            // Replace
            RoutedCommand ReplaceCmd = new RoutedCommand();
            ReplaceCmd.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(ReplaceCmd, MenuEvents.Replace));

            // GoTo
            RoutedCommand GoToCmd = new RoutedCommand();
            GoToCmd.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(GoToCmd, MenuEvents.GoTo));

            Find.Click += MenuEvents.Find_Click;
            Go_To.Click += MenuEvents.GoTo_Click;

            // Compiler
            RoutedCommand CompileCmd = new RoutedCommand();
            CompileCmd.InputGestures.Add(new KeyGesture(Key.F5));
            CommandBindings.Add(new CommandBinding(CompileCmd, MenuEvents.Compile));

            Core.Controller.Register.CompileMenuItem(this.Compile);

            Compile.Click       += MenuEvents.Compile_Click;
            Compiler_Path.Click += MenuEvents.Compiler_Path_Click;
            
            CompilerCloseBox.MouseLeftButtonDown    += MenuEvents.Compiler_Close_Click;
            CompilerCloseBox.MouseEnter             += SearchEvents.Close_MouseEnter;
            CompilerCloseBox.MouseLeave             += SearchEvents.Close_MouseLeave;

            // About
            RoutedCommand AboutCmd = new RoutedCommand();
            AboutCmd.InputGestures.Add(new KeyGesture(Key.F1));
            CommandBindings.Add(new CommandBinding(AboutCmd, MenuEvents.About_Click));

            About.Click += MenuEvents.About_Click;

            Analysis.Click += MenuEvents.Analyse_Click;
        }        
    }
}
