using System;
using System.Collections.Generic;

namespace Arx.Graphics.Control
{
    public interface IControl
    {
        int X { get; set; }
        int Y { get; set; }
        int Width { get; }
        int Height { get; }
        bool isRendering { get; set; }

        void Render();
    }
}
