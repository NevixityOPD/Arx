using Arx.Graphics.Control;
using Cosmos.System.ExtendedASCII;
using Cosmos.System.Graphics;
using System;
using System.Drawing;
using Sys = Cosmos.System;

namespace Arx
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.OutputEncoding = CosmosEncodingProvider.Instance.GetEncoding(437);
            Global.cmdmgr = new();
            Global.screen = FullScreenCanvas.GetFullScreenCanvas(new Mode(800, 600, ColorDepth.ColorDepth32));
            Global.desktop = new(800, 600, Color.FromArgb(0, 128, 128));

            Global.desktop.windows.Add(new Graphics.Window.Window(30, 30, 400, 400, "Test Window", Color.Honeydew, Graphics.TitlebarTheme.White));
        }

        protected override void Run()
        {
            Global.desktop.Render();
        }
    }
}
