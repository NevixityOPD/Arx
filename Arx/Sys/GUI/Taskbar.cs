using Arx.Sys.GUI.Controls;
using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Drawing;

namespace Arx.Sys.GUI
{
    public class Taskbar
    {
        public Taskbar(Color TaskbarColor)
        {
            this.TaskbarColor = TaskbarColor;
        }

        public Color TaskbarColor { get; set; } = Color.Gray;
        public bool ShowMenuButton { get; set; } = true;
        public bool ShowDivider { get; set; } = true;
        public bool ShowAvailableWindow { get; set; } = true;
        public bool ShowMenu { get; set; } = false;

        private KeyEvent key;

        public List<Button> WindowButton { get; set; }
    
        public void Render()
        {
            Kernel.Desktop.Screen.DrawFilledRectangle(TaskbarColor, 0, Desktop.Height - 30, Desktop.Width, 30);
            Kernel.Desktop.Screen.DrawFilledRectangle(Color.White, 82, (Desktop.Height - 30) - 10, 10, 20);
            Kernel.Desktop.Screen.DrawFilledRectangle(Color.Gray, 12, (Desktop.Height - 30) - 10, 60, 20);
            if (ShowMenu)
            {
                Kernel.Desktop.Screen.DrawFilledRectangle(Color.FromArgb(17, 18, 17), 12, Desktop.Height - (40 + 100), 500, 100);
            }
        }
    }
}
