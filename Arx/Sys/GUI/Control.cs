using System;
using System.Collections.Generic;

namespace Arx.Sys.GUI
{
    public abstract class Control
    {
        public abstract uint X { get; set; }
        public abstract uint Y { get; set; }

        public abstract bool IsVisible { get; set; }

        public abstract void Render(string[] args);
    }
}
