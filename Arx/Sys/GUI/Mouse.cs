using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Arx.Sys.GUI
{
    public class Mouse
    {
        public Bitmap cursor { get; }

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
