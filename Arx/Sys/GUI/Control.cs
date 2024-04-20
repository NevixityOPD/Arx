using System;
using System.Collections.Generic;

namespace Arx.Sys.GUI
{
    public abstract class Control
    {
        public abstract uint X { get; set; }
        public abstract uint Y { get; set; }
        public abstract ushort Width { get; set; }
        public abstract ushort Height { get; set; }

        public abstract uint StaticX { get; set; }
        public abstract uint StaticY { get; set; }

        public abstract bool IsVisible { get; set; }

        public abstract void Render();
    }
}
