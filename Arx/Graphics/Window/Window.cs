using Arx.Graphics.Control;
using Cosmos.System;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Arx.Graphics.Window
{
    public class Window
    {
        public Window(int x, int y, uint width, uint height, string title, Color foregroundColor, TitlebarTheme titlebarColor)
        {
            Y = y;
            X = x;
            this.width = width;
            this.height = height;
            this.foregroundColor = foregroundColor;
            this.titlebarColor = titlebarColor;
            this.title = title;

            controls = new();
            close = new Button((int)(x + width) - 20, y, 20, titleBarHeight, "X", false, Color.Red, () => { isRendering = false; });
        }

        public uint width { get; set; }
        public uint height { get; set; }
        public int X { get; set; } = 30;
        public int Y { get; set; } = 30;
        public string title { get; set; } = "New Window";
        public bool isDragging;
        public bool isRendering = true;

        public Color foregroundColor { get; private set; }
        public TitlebarTheme titlebarColor { get; private set; }

        public List<IControl> controls;

        private Button close;
        private MouseState prevMouseState;
        private int mouseDragStartX;
        private int mouseDragStartY;
        private const ushort titleBarHeight = 20;

        public void Render()
        {
            Global.screen.DrawFilledRectangle(Color.FromArgb(36, 36, 36), X + 4, Y + 4, (int)width, (int)height);
            Global.screen.DrawFilledRectangle(foregroundColor, X, Y, (int)width, (int)height);
            Global.screen.DrawFilledRectangle(Color.FromArgb(59, 146, 209), (X), (Y), ((int)width), (titleBarHeight));
            Global.screen.DrawString(title, PCScreenFont.Default, Color.Black, (int)X + 4, (int)Y + 4);
            close.Render(); close.X = (int)(X + width) - 20; close.Y = Y;

            if (controls.Count != 0)
            {
                foreach (var i in controls)
                {
                    if (i.isRendering)
                    {
                        if (i.X + i.Width >= X + width) { i.X = (int)((X + width) - 1); }
                        if (i.Y + i.Height >= Y + height) { i.Y = (int)((Y + height) - 1); }
                        if (i.X <= X) { i.X = X + 1; }
                        if (i.X <= Y) { i.X = Y + 1; }
                    }
                }
            }

            if (IsMouseWithin(X, Y, (ushort)width, 25) && MouseManager.MouseState == MouseState.Left && prevMouseState == MouseState.None)
            {
                mouseDragStartX = (int)(MouseManager.X - X);
                mouseDragStartY = (int)(MouseManager.Y - Y);

                isDragging = true;
            }

            if(MouseManager.MouseState == MouseState.None) { isDragging = false; }

            if (isDragging)
            {
                X = (int)MouseManager.X - mouseDragStartX; close.X = (int)(X + width) - 20;
                Y = (int)MouseManager.Y - mouseDragStartY; close.Y = Y;
            }

            prevMouseState = MouseManager.MouseState;
        }

        private static bool IsMouseWithin(int X, int Y, ushort Width, ushort Height)
        {
            return
                MouseManager.X >= X && MouseManager.X <= X + Width &&
                MouseManager.Y >= Y && MouseManager.Y <= Y + Height;
        }
    }
}
