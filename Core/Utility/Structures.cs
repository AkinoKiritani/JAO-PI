namespace JAO_PI.Core.Utility
{
    class Structures
    {
        public enum Frames
        {
            MainFrame,
            SearchFrame,
            GoToFrame,
            CreditsFrame
        }
        public enum ContextMenuItems
        {
            Close,
            Rename,
            Save,
            CloseAll,
            CloseAllBut
        }
        public enum StatusBar
        {
            Line,
            Column
        }

        [System.Flags]
        public enum States
        {
            Saved = 0,
            Changed = 1,
            Searching = 2
        }
    }
}
