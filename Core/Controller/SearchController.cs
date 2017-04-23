﻿using System.Windows.Controls;

namespace JAO_PI.Core.Controller
{
    static class Search
    {
        // Main
        public static TabControl SearchControl = null;
        public static string CurrentSearch = null;
        public static int CurrentSearchIndex { get; set; }
        public static TextBlock SearchInfo = null;
        public static Structures.LastSearch LastSearchTyp = Structures.LastSearch.None;

        // Header
        public static Canvas Head = null;

        // Search
        public static TextBox SearchBox = null;
        public static CheckBox MatchCase = null;

        // Replace
        public static TextBox SearchBox_Replace = null;
        public static TextBox ReplaceBox = null;

        //Goto
        public static TextBox GoToBox = null;
        public static Label LineLabel = null;
        public static Label MaxLineLabel = null;
        public static RadioButton Line = null;
        public static RadioButton Offset = null;
    }
}
