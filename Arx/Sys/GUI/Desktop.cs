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

        public Color desktopBackground;

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
            mouse = new Mouse(Kernel.cursorBitmap, Width, Height);
        }

        public void Render()
        {
            Screen.Clear(desktopBackground);

            if (EnableDebugStat)
            {
                Screen.DrawString($"FPS : {FPS}", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, 25, 25);
            }

            if(windows.Count != 0)
            {
                foreach (var i in windows)
                {
                    if (i.IsVisible)
                    {
                        if (i.IsRemoved)
                        {
                            windows.Remove(i);
                        }
                        else { i.Render(); }
                    }
                }
            }

            mouse.Render();
            mouse.ChangeCursor(Kernel.cursorBitmap);

            Screen.Display();
            FrameRendered++;

            Heap.Collect();
        }
    }
}
