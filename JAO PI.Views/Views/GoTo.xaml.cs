using System.Windows;

namespace JAO_PI.Views
{
    /// <summary>
    /// Interaktionslogik für GoTo.xaml
    /// </summary>
    public partial class GoTo : Window
    {
        private EventsManager.GoToFrame GoToEvents = null;
        public GoTo()
        {
            GoToEvents = new EventsManager.GoToFrame();
            InitializeComponent();
            
            Core.Controller.Register.GoToComponents(GoToBox, Position, Max_Position, Line, Offset);
            this.Closing += GoToEvents.Closing;
            this.Activated += GoToEvents.Activated;

            Line.Checked += GoToEvents.Line_Checked;
            Offset.Checked += GoToEvents.Offset_Checked;

            GoToBox.KeyDown += GoToEvents.KeyDown;

            Button_ToNowhere.Click += GoToEvents.GoNowhere_Click;
            Button_ToGo.Click += GoToEvents.GoTo_Click;
        }
    }
}