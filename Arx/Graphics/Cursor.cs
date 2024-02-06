using Cosmos.System;
using System;
using System.Collections.Generic;

namespace Arx.Graphics
{
    public class Cursor
    {
        public Cursor(uint width, uint height)
        {
            MouseManager.ScreenWidth = width;
            MouseManager.ScreenHeight = height;

            MouseManager.MouseSensitivity = 0.5f;
        }

        public void Update()
        {
            Global.screen.DrawImageAlpha(Global.cursor, (int)MouseManager.X, (int)MouseManager.Y);
        }
    }
}
