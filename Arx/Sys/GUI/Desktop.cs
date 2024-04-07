using Arx.Sys.GUI.Controls;
using Cosmos.Core.Memory;
using Cosmos.System.Graphics;
using System.Drawing;
using System.Threading;

namespace Arx.Sys.GUI
{
    public class Desktop
    {
        public Canvas Screen;

        public const ushort Width = 800;
        public const ushort Height = 600;

        public static int FPS = 0;
        public static int FrameRendered = 0;
        public static int Runtime = 0;
        public static int HeapReset = 0;

        public Color desktopBackground;
        public TextList DebuStat;

        public Mouse mouse;

        private Timer T = new((_) => { FPS = FrameRendered; FrameRendered = 0; Runtime++; }, null, 1000, 0);

        public bool EnableDebugStat = false;

        public Desktop(bool enableDebugStat)
        {
            EnableDebugStat = enableDebugStat;

            desktopBackground = Color.Teal;
            Screen = FullScreenCanvas.GetFullScreenCanvas(new Mode(Width, Height, ColorDepth.ColorDepth32));

            DebuStat = new TextList(25, 25, Color.Black , 14, 16);

            mouse = new Mouse(Kernel.cursorBitmap, Width, Height);
        }

        public void Render()
        {
            Screen.Clear(desktopBackground);

            if (EnableDebugStat)
            {
                //Screen.DrawString($"FPS : {FPS}", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, 25, 25);
                DebuStat.Render(new string[]
                {
                    $"FPS : {FPS}",
                    $"GUI Runtime : {Runtime}s",
                    $"Ram total : {Kernel.totalRam} mb",
                });
            }

            mouse.Render();

            Screen.Display();
            FrameRendered++;

            if(HeapReset == 19) { Heap.Collect(); HeapReset = 0; }
            else { HeapReset++; }
        }
    }
}
