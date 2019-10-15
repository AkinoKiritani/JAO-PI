using JAO_PI.Core.Classes;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace JAO_PI.Core.Utility
{
    public static class Main
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
        
        public static Image CreateImage(System.Drawing.Bitmap Image, double Width, double Height, HorizontalAlignment Alignment)
        {
            if(Image != null)
            {
                Stream ImageStream = new MemoryStream();
                Image.Save(ImageStream, ImageFormat.Png);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ImageStream;
                bitmap.EndInit();

                Image SaveIcon = new Image()
                {
                    Source = bitmap,
                    Width = Width,
                    Height = Height,
                    HorizontalAlignment = Alignment
                };
                return SaveIcon;
            }
            return null;
        }

        public static void LoadDropData(DragEventArgs e)
        {
            if (e != null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                    Generator generator = new Generator();
                    string[] arg;
                    TabItem Index;
                    bool found = false;
                    for (int i = 0; i < files.Length; i++)
                    {
                        for (int j = 0; j < Controller.Main.tabControl.Items.Count; j++)
                        {
                            Index = Controller.Main.tabControl.Items[j] as TabItem;
                            if (Index != null)
                            {
                                if (files[i].Equals(Index.Uid + Tab.GetTabHeaderText(Index), StringComparison.CurrentCultureIgnoreCase))
                                {
                                    found = true;
                                    break;
                                }
                            }
                        }
                        if (found == false)
                        {
                            arg = files[i].Split('\\');

                            FileStream stream = new FileStream(files[i], FileMode.Open, FileAccess.Read);
                            TabItem tab = generator.TabItem(files[i], arg[arg.Length - 1], stream);
                            stream.Close();

                            Controller.Main.tabControl.Items.Add(tab);
                            Controller.Main.tabControl.SelectedItem = tab;

                            Toggle.TabControl(true);
                            if (Controller.Main.tabControl.Items.Count == 1)
                            {
                                Toggle.SaveOptions(true);
                            }
                            break;
                        }
                    }
                }
            }
        }
        public static void SetResourceImage(System.Drawing.Bitmap img, ImageFormat format, Image control)
        {
            if(img != null && control != null)
            {
                Stream ImageStream = new MemoryStream();
                BitmapImage bitmap = new BitmapImage();

                img.Save(ImageStream, format);

                bitmap.BeginInit();
                bitmap.StreamSource = ImageStream;
                bitmap.EndInit();
                control.Source = bitmap;
            }            
        }
    }
}
