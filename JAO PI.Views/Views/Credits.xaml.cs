using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

            // Load Image from Ressources
            Stream ImageStream = new MemoryStream();
            Properties.Resources.pawn.Save(ImageStream, ImageFormat.Png);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = ImageStream;
            bitmap.EndInit();

            kek.Source = bitmap;

            SaveIconByWebsite.MouseLeftButtonUp += CreditsFrameEvents.SaveIconByWebsite_MouseLeftButtonUp;
            IconByWebsite.MouseLeftButtonUp += CreditsFrameEvents.IconByWebsite_MouseLeftButtonUp;
            CloseButton.Click += CreditsFrameEvents.CloseButton_Click;
        }
    }
}
