using System;
using System.Diagnostics;
using CocosSharp;
using Run;

namespace Run_CocosSharp
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

