using System.Drawing.Imaging;
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

            Core.Utility.Main.SetResourceImage(Properties.Resources.box_full, ImageFormat.Png, DataMenuItem);
            Core.Utility.Main.SetResourceImage(Properties.Resources.open_arrow, ImageFormat.Png, OpenFileMenuItem);
            Core.Utility.Main.SetResourceImage(Properties.Resources.save_text, ImageFormat.Png, SaveMenuItem);
            Core.Utility.Main.SetResourceImage(Properties.Resources.save_text, ImageFormat.Png, SaveAsMenuItem);

            Core.Controller.Register.MainView(this.MainView);
            Core.Controller.Register.TabControl(this.tabControl);
            Core.Controller.Register.EmptyMessage(this.Message_Label);
            Core.Controller.Register.SaveOptions(this.MainMenu);
            Core.Controller.Register.Edit(this.Edit);
            Core.Controller.Register.StatusBar(this.Compiling, this.Line, this.Column);
            Core.Controller.Register.CompilePanel(this.CompilerPanel);

            // MainFrame
            this.Loaded         += FrameEvents.MainFrameLoaded;
            this.Closed         += FrameEvents.MainFrameClosed;
            this.Closing        += FrameEvents.MainFrameClosing;
            // Event for transparency of the SearchFrame
            this.Activated      += FrameEvents.MainFrameActivated;
            this.Drop           += FrameEvents.MainFrameDrop;

            tabControl.SelectionChanged += TabControllEvents.SelectionChanged;

            // Data
            Create_File.Click   += MenuEvents.CreateFileClick;
            Open_File.Click     += MenuEvents.OpenFileClick;
            Close_File.Click    += MenuEvents.CloseFileClick;
            Close_All.Click     += MenuEvents.CloseAllClick;
            Save.Click          += MenuEvents.SaveClick;
            SaveAs.Click        += MenuEvents.SaveAsClick;
            Exit.Click          += MenuEvents.ExitClick;

            RoutedCommand SaveCmd = new RoutedCommand();
            SaveCmd.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(SaveCmd, MenuEvents.SaveClick));

            RoutedCommand NewFileCmd = new RoutedCommand();
            NewFileCmd.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(NewFileCmd, MenuEvents.CreateFileClick));

            // Edit
            Undo.Click          += MenuEvents.UndoClick;
            Cut.Click           += MenuEvents.CutClick;
            Copy.Click          += MenuEvents.CopyClick;
            Paste.Click         += MenuEvents.PasteClick;

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

            Find.Click += MenuEvents.FindClick;
            Go_To.Click += MenuEvents.GoToClick;

            // Compiler
            RoutedCommand CompileCmd = new RoutedCommand();
            CompileCmd.InputGestures.Add(new KeyGesture(Key.F5));
            CommandBindings.Add(new CommandBinding(CompileCmd, MenuEvents.Compile));

            Core.Controller.Register.CompileMenuItem(this.Compile);

            Compile.Click       += MenuEvents.CompileClick;
            Compiler_Path.Click += MenuEvents.CompilerPathClick;
            
            CompilerCloseBox.MouseLeftButtonDown    += MenuEvents.CompilerCloseClick;
            CompilerCloseBox.MouseEnter             += SearchEvents.CloseMouseEnter;
            CompilerCloseBox.MouseLeave             += SearchEvents.CloseMouseLeave;

            // About
            RoutedCommand AboutCmd = new RoutedCommand();
            AboutCmd.InputGestures.Add(new KeyGesture(Key.F1));
            CommandBindings.Add(new CommandBinding(AboutCmd, MenuEvents.AboutClick));

            About.Click += MenuEvents.AboutClick;

            Analysis.Click += MenuEvents.AnalyseClick;
        }        
    }
}
