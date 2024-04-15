using Arx.Sys.GUI.Controls;
using Cosmos.Core.Memory;
using Cosmos.System.Graphics;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Arx.Sys.GUI
{
    public class Desktop
    {
        public Canvas Screen;

        public const ushort Width = 800;
        public const ushort Height = 600;

        public int FPS = 0;
        public int FrameRendered = 0;
        public int Runtime = 0;
        public Taskbar taskbar;

        public Color desktopBackground;
        public TextList DebuStat;

        public TextBox testBox;

        public List<Window.Window> windows;

        public Mouse mouse;

        private Timer T;

        public bool EnableDebugStat = false;
        public bool TextBoxBlink = false;

        public Desktop(bool enableDebugStat)
        {
            T = new((_) => { FPS = FrameRendered; FrameRendered = 0; Runtime++; }, null, 1000, 0);
            EnableDebugStat = enableDebugStat;

            desktopBackground = Color.Teal;
            Screen = FullScreenCanvas.GetFullScreenCanvas(new Mode(Width, Height, ColorDepth.ColorDepth32));

            windows = new List<Window.Window>();
            DebuStat = new TextList(25, 25, Color.Black , 14, 16);
            mouse = new Mouse(Kernel.cursorBitmap, Width, Height);

            testBox = new TextBox(100, 200, 200, null!);

            windows.Add(new Window.Window(25, 25, 300, 300, "Balls"));

            taskbar = new Taskbar(Color.Gray);
        }

        public void Render()
        {
            Screen.Clear(desktopBackground);

            taskbar.Render();

            if (EnableDebugStat)
            {
                //Screen.DrawString($"FPS : {FPS}", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, 25, 25);
                DebuStat.Render(new string[]
                {
                    $"FPS : {FPS}",
                    $"GUI Runtime : {Runtime}s",
                    $"Ram total : {Kernel.totalRam} MB",
                    $"Text render start : {testBox.TextStart}"
                });
            }

            testBox.Render(new string[1]);

            foreach(var i in windows)
            {
                if(i.IsVisible)
                {
                    i.Render();
                }
            }

            mouse.Render();

            Screen.Display();
            FrameRendered++;

            Heap.Collect();
        }
    }
}
