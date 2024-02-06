using System;
using System.Collections.Generic;

namespace Arx.Graphics.Window.Control
{
    public interface IControl
    {
        int X { get; set; }
        int Y { get; set; }
        int Width { get; }
        int Height { get; }
        Window parent { get; }
        bool isRendering { get; set; }

        void Render();
    }
}
