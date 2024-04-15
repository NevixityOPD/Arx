using Arx.Sys.GUI.Controls;
using Cosmos.System;
using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Arx.Sys.GUI.Window
{
    public class Window
    {
        public Window(uint X, uint Y, ushort Width, ushort Height, string WindowTitle)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.WindowTitle = WindowTitle;

            exitButton = new Button(X + Width - 25, Y, 25, 25, "X", Color.Red, () => { IsVisible = false; });
        }

        public uint X { get; set; } = 25;
        public uint Y { get; set; } = 25;
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public string WindowTitle { get; set; } = "New Window";

        public const byte TitleBarHeight = 25;

        public bool IsVisible = true;
        public bool TaskBarVisible = true;
        public Color BackgroundColor = Color.White;
        public Color TitleBarColor = Color.SkyBlue;

        private bool IsDragging = false;
        private int DragStartX;
        private int DragStartY;
        private Button exitButton;

        public void Render()
        {
            Kernel.Desktop.Screen.DrawFilledRectangle(Color.FromArgb(17, 18, 17), (int)(X + 4), (int)(Y + 4), (int)Width, (int)Height);
            Kernel.Desktop.Screen.DrawFilledRectangle(BackgroundColor, (int)(X), (int)(Y), (int)Width, (int)Height);
            Kernel.Desktop.Screen.DrawFilledRectangle(TitleBarColor, (int)X, (int)Y, Width, TitleBarHeight);
            //Kernel.Desktop.Screen.DrawStringTTF(Color.Black, WindowTitle, "calibri", 16, new Point((int)X + 17, (int)Y + 17));
            Kernel.Desktop.Screen.DrawString(WindowTitle, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, (int)X + 8, (int)Y + 8);
            
            exitButton.Render(new string[1]);
            if(Kernel.Desktop.mouse.GetCurrentMouseStat() == MouseState.Left && Kernel.Desktop.mouse.GetLastClickEvent() == MouseState.None && IsMouseWithin((int)X, (int)Y, Width, TitleBarHeight))
            {
                DragStartX = (int)(MouseManager.X - X);
                DragStartY = (int)(MouseManager.Y - Y);

                IsDragging = true;
            }

            if(Kernel.Desktop.mouse.GetCurrentMouseStat() == MouseState.None) { IsDragging = false; }

            if(IsDragging)
            {
                X = (uint)(MouseManager.X - DragStartX);
                Y = (uint)(MouseManager.Y - DragStartY);

                exitButton.X = X + Width - 25;
                exitButton.Y = Y;
            }
        }

        private static bool IsMouseWithin(int X, int Y, ushort Width, ushort Height)
        {
            return
                MouseManager.X >= X && MouseManager.X <= X + Width &&
                MouseManager.Y >= Y && MouseManager.Y <= Y + Height;
        }
    }
}
