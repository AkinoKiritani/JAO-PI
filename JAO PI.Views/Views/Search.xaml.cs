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

            Core.Controller.Register.SearchBox(SearchBox);
            Cancel.Click += SearchEvents.Cancel_Click;
            Do_Search.Click += SearchEvents.Search_Click;
            this.Closing += SearchEvents.Closing;
        }
    }
}