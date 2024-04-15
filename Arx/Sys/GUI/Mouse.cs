using Cosmos.System;
using Cosmos.System.Graphics;

namespace Arx.Sys.GUI
{
    public class Mouse
    {
        public Bitmap cursor { get; private set; }

        public Mouse(Bitmap cursor, uint screenWidth, uint screenHeight) 
        {
            MouseManager.ScreenWidth = screenWidth;
            MouseManager.ScreenHeight = screenHeight;

            this.cursor = cursor; 
        }

        public void Render()
        {
            Kernel.Desktop.Screen.DrawImageAlpha(cursor, (int)MouseManager.X, (int)MouseManager.Y);
        }

        public void ChangeCursor(Bitmap cursor)
        {
            this.cursor = cursor;
        }

        public MouseState GetLastClickEvent()
        {
            return MouseManager.LastMouseState;
        }

        public MouseState GetCurrentMouseStat()
        {
            return MouseManager.MouseState;
        }
    }
}
