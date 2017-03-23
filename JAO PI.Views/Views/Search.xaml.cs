using System.Windows;
using System.Windows.Input;

namespace JAO_PI.Views
{
    /// <summary>
    /// Interaktionslogik für Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        private EventsManager.SearchFrame SearchEvents = null;
        private EventsManager.GoToFrame GoToEvents = null;
        private EventsManager.MainMenu MenuEvents = null;
        public Search()
        {
            MenuEvents = new EventsManager.MainMenu();
            SearchEvents = new EventsManager.SearchFrame();
            GoToEvents = new EventsManager.GoToFrame();

            InitializeComponent();

            // Register Main
            Core.Controller.Register.SearchControl(SearchControl);
            Core.Controller.Register.SearchInfo(SearchInfo);

            // Register Header
            Core.Controller.Register.MoveHeader(Head);

            // Register Search
            Core.Controller.Register.SearchBox(SearchBox);
            Core.Controller.Register.MatchCase(MatchCase);

            //Register Replace
            Core.Controller.Register.SearchBox_Replace(SearchBox_Replace);
            Core.Controller.Register.ReplaceBox(ReplaceBox);

            // Register GoTo
            Core.Controller.Register.GoToComponents(GoToGrid);

            // Events Search
            Cancel.Click    += SearchEvents.Cancel_Click;
            Do_Search.Click += SearchEvents.Search_Click;
            Do_Count.Click  += SearchEvents.Count_Click;

            // Events Replace
            Do_Search_Replace.Click += SearchEvents.Do_Search_Replace_Click;
            Do_Replace.Click        += SearchEvents.Do_Replace;
            Do_Replace_All.Click    += SearchEvents.Do_Replace_All;
            Cancel_Replace.Click    += SearchEvents.Cancel_Click;

            // Events GoTo
            Line.Checked += GoToEvents.Line_Checked;
            Offset.Checked += GoToEvents.Offset_Checked;
            GoToBox.PreviewTextInput += GoToEvents.PreviewTextInput;
            Button_ToNowhere.Click += GoToEvents.GoNowhere_Click;
            Button_ToGo.Click += GoToEvents.GoTo_Click;

            // Events Frame
            SearchControl.Loaded += SearchEvents.Loaded;
            this.Closing += SearchEvents.Closing;

            // Event for transparency
            this.Activated += SearchEvents.Activated;

            // Events for the rebuild Header
            CloseBox.MouseEnter += SearchEvents.Close_MouseEnter;
            CloseBox.MouseLeave += SearchEvents.Close_MouseLeave;
            CloseBox.MouseLeftButtonDown += SearchEvents.Close_MouseLeftButtonDown;

            Head.MouseLeftButtonDown += SearchEvents.Head_MouseLeftButtonDown;

            // Ctrl + G will switch to GoTo
            RoutedCommand GoToCmd = new RoutedCommand();
            GoToCmd.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(GoToCmd, MenuEvents.GoTo));

            // Ctrl + G will switch to Search
            RoutedCommand FindCmd = new RoutedCommand();
            FindCmd.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(FindCmd, MenuEvents.Search));

            // Ctrl + H will switch to Replace
            RoutedCommand ReplaceCmd = new RoutedCommand();
            ReplaceCmd.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(ReplaceCmd, MenuEvents.Replace));
        }
    }
}