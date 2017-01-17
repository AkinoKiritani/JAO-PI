using ICSharpCode.AvalonEdit;
using JAO_PI.Core.Utility;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JAO_PI.Core.Classes
{
    class Generator
    {
        EventsManager.TabContextMenu TabEvents = new EventsManager.TabContextMenu();
        EventsManager.Editor EditorEvents = new EventsManager.Editor();
        public TabItem TabItem(string path, string header, Stream content)
        {
            TextEditor Editor = new TextEditor()
            {
                FontSize = 13,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                FontFamily = new FontFamily("Consolas"),
                ShowLineNumbers = true,
                Margin = new Thickness(0, 0, 5, 0)
            };

            if (content != null)
            {
                Editor.Load(content);
            }
            
            StringBuilder SyntaxPath = new StringBuilder();
            SyntaxPath.Append(System.Environment.CurrentDirectory);
            SyntaxPath.Append(@"\" + Core.Resources.Folder.Languages+ @"\PAWN.xshd");

            string syntaxPath = SyntaxPath.ToString();
            if (File.Exists(syntaxPath) == false)
            {
                Directory.CreateDirectory(Core.Resources.Folder.Languages);
                using (FileStream fs = File.Create(syntaxPath))
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var resourceName = "JAO_PI.Core.Resources.PAWN.xshd";

                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            byte[] info = new UTF8Encoding(true).GetBytes(reader.ReadToEnd());
                            fs.Write(info, 0, info.Length);
                            fs.Close();
                        }
                    }
                }
            }
            Main.LoadSyntax(Editor, syntaxPath);
            Grid grid = new Grid();
            grid.Children.Add(Editor);

            TabItem tab = new TabItem()
            {
                Header = GenerateTabHeader(header, Properties.Resources.save_text),
                Content = grid
            };
            
            
            if (path.Contains(header) == true)
            {
                path = path.Remove(path.Length - header.Length, header.Length);
            }
            tab.Uid = path;

            tab.ContextMenu = GenerateContextMenu();

            Controller.Main.TabControlList.Add(new Controller.Tab()
            {
                TabItem     = tab,
                Editor      = Editor,
                HeaderPanel = tab.Header,
                Close       = tab.ContextMenu.Items[(int)Structures.ContextMenuItems.Close]         as MenuItem,
                CloseAll    = tab.ContextMenu.Items[(int)Structures.ContextMenuItems.CloseAll]      as MenuItem,
                CloseAllBut = tab.ContextMenu.Items[(int)Structures.ContextMenuItems.CloseAllBut]   as MenuItem,
                Rename      = tab.ContextMenu.Items[(int)Structures.ContextMenuItems.Rename]        as MenuItem,
                Save        = tab.ContextMenu.Items[(int)Structures.ContextMenuItems.Save]          as MenuItem
            });
            Editor.Uid = path;
            Editor.Document.Changed += EditorEvents.Document_Changed;
            Editor.Document.FileName = header + ".JAOsaved";
            Editor.Unloaded += EditorEvents.Editor_Unloaded;
            Editor.TextArea.Caret.PositionChanged += EditorEvents.Caret_PositionChanged;
            Controller.Main.EditItem.IsEnabled = true;
            return tab;
        }

        private ContextMenu GenerateContextMenu()
        {
            ContextMenu menu = new ContextMenu();

            MenuItem CloseItem = new MenuItem()
            {
                Header = Resources.ContextMenu.CloseItem,
                Uid = Main.RandomString(10)
            };
            CloseItem.PreviewMouseLeftButtonUp += TabEvents.CloseItem_PreviewMouseLeftButtonUp;
            menu.Items.Add(CloseItem);

            MenuItem CloseAllItem = new MenuItem()
            {
                Header = Resources.ContextMenu.CloseAllItem,
                Uid = Main.RandomString(10)
            };
            CloseAllItem.PreviewMouseLeftButtonUp += TabEvents.CloseAllItem_PreviewMouseLeftButtonUp;
            menu.Items.Add(CloseAllItem);

            MenuItem CloseAllButItem = new MenuItem()
            {
                Header = Resources.ContextMenu.CloseAllButItem,
                Uid = Main.RandomString(10)
            };
            CloseAllButItem.PreviewMouseLeftButtonUp += TabEvents.CloseAllButItem_PreviewMouseLeftButtonUp;
            menu.Items.Add(CloseAllButItem);

            MenuItem RenameItem = new MenuItem()
            {
                Header = Resources.ContextMenu.RenameItem,
                Uid = Main.RandomString(10)
            };
            RenameItem.PreviewMouseLeftButtonUp += TabEvents.RenameItem_PreviewMouseLeftButtonUp;
            menu.Items.Add(RenameItem);

            MenuItem SaveItem = new MenuItem()
            {
                Header = Resources.ContextMenu.SaveItem,
                Uid = Main.RandomString(10)
            };
            SaveItem.PreviewMouseLeftButtonUp += TabEvents.SaveItem_PreviewMouseLeftButtonUp;
            menu.Items.Add(SaveItem);

            return menu;
        }

        private StackPanel GenerateTabHeader(string Header, System.Drawing.Bitmap Icon)
        {
            // Load Image 
            Stream ImageStream = new MemoryStream();
            Icon.Save(ImageStream, ImageFormat.Png);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = ImageStream;
            bitmap.EndInit();

            Image SaveIcon = new Image()
            {
                Source = bitmap,
                Width = 22,
                Height = 18,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            StackPanel stack = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            // Add Image and Textblock to the StackPanel
            stack.Children.Add(SaveIcon);
            stack.Children.Add(new TextBlock()
            {
                Text = Header
            });
            // Add "unsaved mark" to the StackPanel
            stack.Children.Add(new TextBlock()
            {
                Text = " *",
                Visibility = Visibility.Collapsed
            });
            return stack;
        }
    }
}