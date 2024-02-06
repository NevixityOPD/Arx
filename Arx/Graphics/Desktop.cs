using Arx.Graphics.Control;
using Cosmos.Core.Memory;
using Cosmos.System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Arx.Graphics
{
    public class Desktop
    {
        public Desktop(int width, int height, Color backgroundColor)
        {
            this.width = width;
            this.height = height;
            this.backgroundColor = backgroundColor;

            mouse = new(((uint)width), ((uint)height));
            windows = new();
            startButton = new Button(10, height - 34, 60, 20, "Start", true, Color.FromArgb(195, 195, 195), () =>
            {
                if (MouseManager.MouseState == MouseState.Left && MouseManager.LastMouseState == MouseState.None)
                {
                    if (startMenu) { startMenu = false; }
                    else { startMenu = true; }
                }
                else { }
            });
        }

        public int width { get; private set; }
        public int height { get; private set; }
        public Color backgroundColor { get; set; }
        public Cursor mouse;
        public Button startButton;

        private static int fps = 0;
        private static int frames;
        private static int osRuntime = 0;
        private static int gc;
        private bool startMenu = false;

        Timer T = new((_) => { fps = frames; frames = 0; osRuntime++; }, null, 1000, 0);

        public List<Window.Window> windows;

        public void Render()
        {
            Global.screen.Clear(backgroundColor);
            Global.screen.DrawString($"FPS : {fps} | OS Runtime : {osRuntime}s", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, 25, 25);

            Global.screen.DrawFilledRectangle(Color.FromArgb(129, 129, 129), 0, height - 28, width, 28);
            Global.screen.DrawFilledRectangle(Color.White, 80, height - 34, 5, 20);
            startButton.Render();

            if (startMenu)
            {
                Global.screen.DrawFilledRectangle(Color.FromArgb(129, 129, 129), 10, height - (44 + 180), 100, 180);
            }

            if (windows.Count != 0)
            {
                foreach (var i in windows)
                {
                    if (i.isRendering)
                    {
                        if (i.X + i.width >= width) { i.isDragging = false; i.X = i.X - 1; }
                        if (i.Y + i.height >= height) { i.isDragging = false; i.Y = i.Y - 1; }

                        if (i.X <= 0) { i.isDragging = false; i.X = i.X + 1; }
                        if (i.Y <= 0) { i.isDragging = false; i.Y = i.Y + 1; }

                        i.Render();
                    }
                }
            }

            mouse.Update();
            Global.screen.Display(); 
            if(gc == 19) { Heap.Collect(); gc = 0; } else { gc++; } frames++;
        }
    }
}
