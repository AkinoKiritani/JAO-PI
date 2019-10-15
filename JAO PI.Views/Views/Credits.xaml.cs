using System.Drawing.Imaging;
using System.Windows;

namespace JAO_PI.Views
{
    /// <summary>
    /// Interaktionslogik für Credits.xaml
    /// </summary>
    public partial class Credits : Window
    {
        private EventsManager.CreditFrame CreditsFrameEvents;
        public Credits()
        {
            InitializeComponent();

            CreditsFrameEvents = new EventsManager.CreditFrame();

            Core.Controller.Register.CreditsFrameBorder(CreditsFrameBorder);

            // Load Image from Ressources
            Core.Utility.Main.SetResourceImage(Properties.Resources.pawn, ImageFormat.Png, IconPic);

            SaveIconByWebsite.MouseLeftButtonUp += CreditsFrameEvents.OpenWebsite;
            IconByWebsite.MouseLeftButtonUp += CreditsFrameEvents.OpenWebsite;
            AvalonEditByWebsite.MouseLeftButtonDown += CreditsFrameEvents.OpenWebsite;

            CloseButton.Click += CreditsFrameEvents.CloseButtonClick;
            this.Activated += CreditsFrameEvents.Activated;
        }
    }
}
