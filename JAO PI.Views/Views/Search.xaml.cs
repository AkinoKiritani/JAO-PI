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

            // Register
            Core.Controller.Register.SearchBox(SearchBox);
            Core.Controller.Register.SearchInfo(SearchInfo);
            Core.Controller.Register.MatchCase(MatchCase);

            // Events
            Cancel.Click += SearchEvents.Cancel_Click;
            Do_Search.Click += SearchEvents.Search_Click;
            Do_Count.Click += SearchEvents.Count_Click;
            this.Closing += SearchEvents.Closing;
        }
    }
}