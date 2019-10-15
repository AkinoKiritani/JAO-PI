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
            Core.Controller.Register.SearchComponents(SearchTabGrid);

            //Register Replace
            Core.Controller.Register.ReplaceComponents(ReplaceTabGrid);

            // Register GoTo
            Core.Controller.Register.GoToComponents(GoToGrid);

            // Events Search
            Cancel.Click    += SearchEvents.CancelClick;
            Do_Search.Click += SearchEvents.SearchClick;
            Do_Count.Click  += SearchEvents.CountClick;

            // Events Replace
            Do_Search_Replace.Click += SearchEvents.DoSearchReplaceClick;
            Do_Replace.Click        += SearchEvents.DoReplace;
            Do_Replace_All.Click    += SearchEvents.DoReplaceAll;
            Cancel_Replace.Click    += SearchEvents.CancelClick;

            MatchCase.Checked += SearchEvents.MatchCaseChecked;
            MatchCase.Unchecked += SearchEvents.MatchCaseUnchecked;
            WrapAround.Checked += SearchEvents.WrapAroundChecked;
            WrapAround.Unchecked += SearchEvents.WrapAroundUnchecked;

            MatchCase_Replace.Checked += SearchEvents.MatchCaseChecked;
            MatchCase_Replace.Unchecked += SearchEvents.MatchCaseUnchecked;
            WrapAround_Replace.Checked += SearchEvents.WrapAroundChecked;
            WrapAround_Replace.Unchecked += SearchEvents.WrapAroundUnchecked;

            // Events GoTo
            Line.Checked += GoToEvents.LineChecked;
            Offset.Checked += GoToEvents.OffsetChecked;
            GoToBox.PreviewTextInput += GoToEvents.PreviewTextInput;
            Button_ToNowhere.Click += GoToEvents.GoNowhereClick;
            Button_ToGo.Click += GoToEvents.GoToClick;

            // Events Frame
            SearchControl.Loaded += SearchEvents.Loaded;
            this.Closing += SearchEvents.Closing;

            // Event for transparency
            this.Activated += SearchEvents.Activated;

            // Events for the rebuild Header
            CloseBox.MouseEnter += SearchEvents.CloseMouseEnter;
            CloseBox.MouseLeave += SearchEvents.CloseMouseLeave;
            CloseBox.MouseLeftButtonDown += SearchEvents.CloseMouseLeftButtonDown;

            Head.MouseLeftButtonDown += SearchEvents.HeadMouseLeftButtonDown;

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