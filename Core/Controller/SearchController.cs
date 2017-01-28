using System.Windows.Controls;

namespace JAO_PI.Core.Controller
{
    static class Search
    {
        // Main
        public static TabControl SearchControl = null;
        public static string CurrentSearch = null;
        public static int CurrentSearchIndex { get; set; }
        public static TextBlock SearchInfo = null;

        // Header
        public static Canvas Head = null;

        // Search
        public static TextBox SearchBox = null;
        public static CheckBox MatchCase = null;

        // Replace
        public static TextBox SearchBox_Replace = null;
        public static TextBox ReplaceBox = null;
    }
}
