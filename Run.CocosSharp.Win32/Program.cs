using System;
using System.Diagnostics;
using CocosSharp;
using Run;
using Run.CocosSharp.Shared;

namespace Run.CocosSharp.Win32
{
    static class Program
    {
        static void Main(string[] args)
        {
            CCApplication application = new CCApplication(false, new CCSize(Config.WindowWidth, Config.WindowHeight));
            application.ApplicationDelegate = new AppDelegate();
            application.StartGame();
        }
    }


}

