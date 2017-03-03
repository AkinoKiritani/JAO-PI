using ICSharpCode.AvalonEdit;
using JAO_PI.Core.Utility;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JAO_PI.Core.Classes
{
    class Generator
    {
        EventsManager.TabContextMenu TabEvents = new EventsManager.TabContextMenu();
        EventsManager.Editor EditorEvents = new EventsManager.Editor();
        public TabItem TabItem(string path, string header, Stream content)
        {
            TextEditorOptions Options = new TextEditorOptions()
            {
                ConvertTabsToSpaces = true,
                AllowScrollBelowDocument = true,
                CutCopyWholeLine = true,
                HighlightCurrentLine = true
            };
            TextEditor Editor = new TextEditor()
            {
                FontSize = 13,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                FontFamily = new FontFamily("Consolas"),
                ShowLineNumbers = true,
                Margin = new Thickness(0, 0, 5, 0),
                Options = Options
            };

            if (content != null)
            {
                Editor.Load(content);
            }
            
            StringBuilder SyntaxPath = new StringBuilder();

            SyntaxPath.Append(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            SyntaxPath.Append(@"\" + Core.Resources.Folder.Languages);
            string FolderPath = SyntaxPath.ToString();

            SyntaxPath.Append(@"\PAWN.xshd");
            string syntaxPath = SyntaxPath.ToString();

            if (File.Exists(syntaxPath) == false)
            {
                Directory.CreateDirectory(FolderPath);
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
            Utility.Editor.LoadSyntax(Editor, syntaxPath);
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

            Structures.States Tabstate = 0;
            Tabstate |= Structures.States.Saved;

            Controller.Main.TabControlList.Add(new Controller.Tab()
            {
                TabItem     = tab,
                Editor      = Editor,
                HeaderPanel = tab.Header,
                Close       = tab.ContextMenu.Items[(int)Structures.ContextMenuItems.Close]         as MenuItem,
                CloseAll    = tab.ContextMenu.Items[(int)Structures.ContextMenuItems.CloseAll]      as MenuItem,
                CloseAllBut = tab.ContextMenu.Items[(int)Structures.ContextMenuItems.CloseAllBut]   as MenuItem,
                Rename      = tab.ContextMenu.Items[(int)Structures.ContextMenuItems.Rename]        as MenuItem,
                Save        = tab.ContextMenu.Items[(int)Structures.ContextMenuItems.Save]          as MenuItem,
                State       = Tabstate
            });

            Editor.Uid = path;
            Editor.Document.Changed += EditorEvents.Document_Changed;
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
            Image SaveIcon = Main.CreateImage(Icon, 22, 18, HorizontalAlignment.Left);

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

        public ListBoxItem ListItem(ushort ID, string file, string line, string description, System.Drawing.Bitmap Icon)
        {
            // Load Image
            Image ErrorIcon = Main.CreateImage(Icon, 14, 14, HorizontalAlignment.Center);

            Grid ListItem = new Grid();
            ListItem.ColumnDefinitions.Add(new ColumnDefinition() // ID
            {
                Width = new GridLength(12, GridUnitType.Pixel)
            });
            ListItem.ColumnDefinitions.Add(new ColumnDefinition() // icon
            {
                Width = new GridLength(14, GridUnitType.Pixel)
            });
            ListItem.ColumnDefinitions.Add(new ColumnDefinition() // line
            {
                Width = new GridLength(65, GridUnitType.Pixel)
            });
            ListItem.ColumnDefinitions.Add(new ColumnDefinition()); // description
            ListItem.ColumnDefinitions.Add(new ColumnDefinition() // file
            {
                Width = new GridLength(1, GridUnitType.Auto),
                MinWidth = 50
            });

            TextBlock IDColumn = new TextBlock()
            {
                Text = ID.ToString()
            };
            TextBlock lineColumn = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = Properties.Resources.Line + ": " + line
            };
            TextBlock descriptionColumn = new TextBlock()
            {
                Text = description
            };
            TextBlock fileColumn = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                Text = file
            };

            IDColumn.SetValue(Grid.ColumnProperty, 0);
            ErrorIcon.SetValue(Grid.ColumnProperty, 1);
            lineColumn.SetValue(Grid.ColumnProperty, 2);
            descriptionColumn.SetValue(Grid.ColumnProperty, 3);
            fileColumn.SetValue(Grid.ColumnProperty, 4);

            ListItem.Children.Add(IDColumn);
            ListItem.Children.Add(ErrorIcon);
            ListItem.Children.Add(lineColumn);
            ListItem.Children.Add(descriptionColumn);
            ListItem.Children.Add(fileColumn);
            
            ListBoxItem Item = new ListBoxItem()
            {
                Uid = file + "|" + line,
                Content = ListItem,
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };

            EventsManager.ListBoxItem ListBoxEvents = new EventsManager.ListBoxItem();

            Item.MouseDoubleClick += ListBoxEvents.MouseDoubleClick;
            return Item;
        }
        public ListBoxItem ListItem(string Text)
        {
            ListBoxItem Item = new ListBoxItem()
            {
                Content = Text
            };
            return Item;
        }
    }
}