using ICSharpCode.AvalonEdit;
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
            Utility utility = new Utility();
            TextEditor Editor = new TextEditor();

            Editor.FontSize = 13;
            Editor.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            Editor.FontFamily = new FontFamily("Consolas");
            Editor.ShowLineNumbers = true;            

            if (content != null)
            {
                Editor.Load(content);
            }

            Editor.Margin = new Thickness(0, 0, 5, 0); 

            StringBuilder SyntaxPath = new StringBuilder();
            SyntaxPath.Append(System.Environment.CurrentDirectory);
            SyntaxPath.Append(@"\Language\PAWN.xshd");

            string syntaxPath = SyntaxPath.ToString();
            if (File.Exists(syntaxPath) == false)
            {
                Directory.CreateDirectory("Language");
                using (FileStream fs = File.Create(syntaxPath))
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var resourceName = "JAO_PI.Core.Resources.PAWN.xshd";

                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(reader.ReadToEnd());
                        fs.Write(info, 0, info.Length);
                    }
                }
            }
            utility.LoadSyntax(Editor, syntaxPath);
            Grid grid = new Grid();
            grid.Children.Add(Editor);
            
            TabItem tab = new TabItem();
            tab.Header = header;
            tab.Content = grid;
            
            if (path.Contains(header) == true)
            {
                path = path.Remove(path.Length - header.Length, header.Length);
            }
            tab.Uid = path;

            tab.ContextMenu = GenerateContextMenu();

            Controller.Main.TabControlList.Add(new Controller.Tab()
            {
                TabItem = tab,
                Editor = Editor,
                Close = tab.ContextMenu.Items[(int)Utility.ContextMenuItems.Close] as MenuItem,
                Rename = tab.ContextMenu.Items[(int)Utility.ContextMenuItems.Rename] as MenuItem,
                Save = tab.ContextMenu.Items[(int)Utility.ContextMenuItems.Save] as MenuItem
            });
            Editor.Uid = path;
            Editor.Document.Changed += EditorEvents.Document_Changed;
            Editor.Document.FileName = header + ".JAOsaved";
            Editor.Unloaded += EditorEvents.Editor_Unloaded;
            Controller.Main.EditItem.IsEnabled = true;
            return tab;
        }

        private ContextMenu GenerateContextMenu()
        {
            ContextMenu menu = new ContextMenu();

            MenuItem CloseItem = new MenuItem();
            CloseItem.Header = "Close";
            CloseItem.Uid = Utility.RandomString(10);
            CloseItem.PreviewMouseLeftButtonUp += TabEvents.CloseItem_PreviewMouseLeftButtonUp;
            menu.Items.Add(CloseItem);

            MenuItem RenameItem = new MenuItem();
            RenameItem.Header = "Rename";
            RenameItem.Uid = Utility.RandomString(10);
            RenameItem.PreviewMouseLeftButtonUp += TabEvents.RenameItem_PreviewMouseLeftButtonUp;
            menu.Items.Add(RenameItem);

            MenuItem SaveItem = new MenuItem();
            SaveItem.Header = "Save";
            SaveItem.Uid = Utility.RandomString(10);
            SaveItem.PreviewMouseLeftButtonUp += TabEvents.SaveItem_PreviewMouseLeftButtonUp;
            menu.Items.Add(SaveItem);

            return menu;
        }
    }
}