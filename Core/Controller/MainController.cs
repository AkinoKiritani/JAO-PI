﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace JAO_PI.Core.Controller
{
    public class Main
    {
        public static TabControl tabControl = null;
        public static Grid Empty_Message = null;
        public static List<MenuItem> SaveOptions = new List<MenuItem>();
        public static List<Tab> TabControlList = new List<Tab>();
        public static string Compiler_Errors = null;
        public static int LastIndex;
        public static StatusBarItem Compile = null;
        public static MenuItem EditItem = null;

        //Frames
        public static List<Window> Frames = new List<Window>();

        //Search
        public static TextBox SearchBox = null;
        public static string CurrentSearch = null;
        public static int CurrentSearchIndex { get; set; }
    }
}
