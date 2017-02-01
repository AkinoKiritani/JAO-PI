using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

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
            Stream ImageStream = new MemoryStream();
            Properties.Resources.pawn.Save(ImageStream, ImageFormat.Png);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = ImageStream;
            bitmap.EndInit();

            Icon.Source = bitmap;

            SaveIconByWebsite.MouseLeftButtonUp += CreditsFrameEvents.OpenWebsite;
            IconByWebsite.MouseLeftButtonUp += CreditsFrameEvents.OpenWebsite;
            AvalonEditByWebsite.MouseLeftButtonDown += CreditsFrameEvents.OpenWebsite;

            CloseButton.Click += CreditsFrameEvents.CloseButton_Click;
            this.Activated += CreditsFrameEvents.Activated;
        }
    }
}
