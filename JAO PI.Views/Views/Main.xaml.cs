using System.Windows;
using System.Windows.Input;

namespace JAO_PI.Views
{
    /// <summary>
    /// Interaktionslogik für Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private EventsManager.MainFrame FrameEvents;
        private EventsManager.MainMenu MenuEvents;
        private Core.Controller.Worker Worker = null;

        public Main()
        {

            Core.Controller.Register.Frames(new Window[] { this, new Search(), new GoTo() });
            Worker = new Core.Controller.Worker();
            FrameEvents = new EventsManager.MainFrame();
            MenuEvents = new EventsManager.MainMenu();

            InitializeComponent();
            
            Core.Controller.Register.TabControl(this.tabControl);
            Core.Controller.Register.EmptyMessage(this.Empty_Message);
            Core.Controller.Register.SaveOptions(this.Save, this.SaveAs, this.Close_File);
            Core.Controller.Register.Compile(this.Compiling);
            Core.Controller.Register.Edit(this.Edit);

            RoutedCommand CompileCmd = new RoutedCommand();
            CompileCmd.InputGestures.Add(new KeyGesture(Key.F5));
            CommandBindings.Add(new CommandBinding(CompileCmd, MenuEvents.Compile));

            //MainFrame
            this.Loaded         += FrameEvents.MainFrame_Loaded;
            this.Closed         += FrameEvents.MainFrame_Closed;
            this.Closing        += FrameEvents.MainFrame_Closing;

            //Data
            Create_File.Click   += MenuEvents.Create_File_Click;
            Open_File.Click     += MenuEvents.Open_File_Click;
            Close_File.Click    += MenuEvents.Close_File_Click;
            Save.Click          += MenuEvents.Save_Click;
            SaveAs.Click        += MenuEvents.SaveAs_Click;
            Exit.Click          += MenuEvents.Exit_Click;

            RoutedCommand SaveCmd = new RoutedCommand();
            SaveCmd.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(SaveCmd, MenuEvents.Save_Click));

            //Edit
            Undo.Click          += MenuEvents.Undo_Click;
            Cut.Click           += MenuEvents.Cut_Click;
            Copy.Click          += MenuEvents.Copy_Click;
            Paste.Click         += MenuEvents.Paste_Click;
            Find.Click          += MenuEvents.Find_Click;
            Go_To.Click         += MenuEvents.GoTo_Click;

            RoutedCommand FindCmd = new RoutedCommand();
            FindCmd.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(FindCmd, MenuEvents.Search));

            RoutedCommand FindNextCmd = new RoutedCommand();
            FindNextCmd.InputGestures.Add(new KeyGesture(Key.F3));
            CommandBindings.Add(new CommandBinding(FindNextCmd, MenuEvents.FindNext));

            RoutedCommand GoToCmd = new RoutedCommand();
            GoToCmd.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(GoToCmd, MenuEvents.GoTo));

            //Compiler
            Compile.Click       += MenuEvents.Compile_Click;
            Compiler_Path.Click += MenuEvents.Compiler_Path_Click;
        }

    }
}
