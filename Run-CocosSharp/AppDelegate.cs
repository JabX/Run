﻿using System.Reflection;
using Microsoft.Xna.Framework;
using CocosDenshion;
using CocosSharp;

namespace Run_CocosSharp
{
    public class AppDelegate : CCApplicationDelegate
    {
        public static CCWindow SharedWindow { get; set; }
        public static CCSize DefaultResolution;

        public override void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            DefaultResolution = new CCSize (
                        application.MainWindow.WindowSizeInPixels.Width,
                        application.MainWindow.WindowSizeInPixels.Height);

            application.ContentRootDirectory = "Content";
            application.ContentSearchPaths.Add("SD");

            CCScene scene = new CCScene(mainWindow);
            CCLayer layer = new View(DefaultResolution);

            scene.AddChild(layer);

            mainWindow.RunWithScene(scene);
        }
    }
}
