using ICSharpCode.AvalonEdit;
using System.Windows.Controls;

namespace JAO_PI.Core.Controller
{
    public class Tab
    {
        public TabItem      TabItem     { get; set; }
        public TextEditor   Editor      { get; set; }
        public MenuItem     Close       { get; set; }
        public MenuItem     CloseAll    { get; set; }
        public MenuItem     Rename      { get; set; }
        public MenuItem     Save        { get; set; }
    }
}