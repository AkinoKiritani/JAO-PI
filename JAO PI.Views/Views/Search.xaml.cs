using System.Windows;

namespace JAO_PI.Views
{
    /// <summary>
    /// Interaktionslogik für Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        private EventsManager.SearchFrame SearchEvents = null;
        public Search()
        {
            SearchEvents = new EventsManager.SearchFrame();
            InitializeComponent();

            // Register Main
            Core.Controller.Register.SearchControl(SearchControl);
            Core.Controller.Register.SearchInfo(SearchInfo);

            // Register Search
            Core.Controller.Register.SearchBox(SearchBox);
            Core.Controller.Register.MatchCase(MatchCase);

            //Register Replace
            Core.Controller.Register.SearchBox_Replace(SearchBox_Replace);
            Core.Controller.Register.ReplaceBox(ReplaceBox);

            // Events
            Cancel.Click    += SearchEvents.Cancel_Click;
            Do_Search.Click += SearchEvents.Search_Click;
            Do_Count.Click  += SearchEvents.Count_Click;

            Do_Search_Replace.Click += SearchEvents.Do_Search_Replace_Click;
            Do_Replace.Click        += SearchEvents.Do_Replace;
            Do_Replace_All.Click    += SearchEvents.Do_Replace_All;
            Cancel_Replace.Click    += SearchEvents.Cancel_Click;

            SearchControl.Loaded += SearchEvents.Loaded;
            this.Closing += SearchEvents.Closing;
            
        }
    }
}