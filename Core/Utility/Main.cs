using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace JAO_PI.Core.Utility
{
    static class Main
    {
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcefghijklmnopqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }       

        public static void SelectAndOpenSearchTab(Structures.SearchControl Index)
        {
            if (Controller.Main.tabControl.Items.Count > 0 && Controller.Main.tabControl.Visibility == Visibility.Visible)
            {
                if (Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility == Visibility.Collapsed)
                {
                    Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Visibility = Visibility.Visible;
                }
                Controller.Search.SearchControl.SelectedIndex = (int)Index;
                Controller.Main.Frames[(int)Structures.Frames.SearchFrame].Focus();
            }
        }
        
        public static System.Windows.Controls.Image CreateImage(System.Drawing.Bitmap Image, double Width, double Height, HorizontalAlignment Alignment)
        {
            Stream ImageStream = new MemoryStream();
            Image.Save(ImageStream, ImageFormat.Png);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = ImageStream;
            bitmap.EndInit();

            System.Windows.Controls.Image SaveIcon = new System.Windows.Controls.Image()
            {
                Source = bitmap,
                Width = Width,
                Height = Height,
                HorizontalAlignment = Alignment
            };
            return SaveIcon;
        }
    }
}
