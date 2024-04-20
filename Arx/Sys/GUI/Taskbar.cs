using Arx.Sys.GUI.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Arx.Sys.GUI
{
    public class Taskbar
    {
        public Taskbar(Color TaskbarColor)
        {
            this.TaskbarColor = TaskbarColor;

            MenuButton = new Button(12, (Desktop.Height - 30) - 10, 60, 20, "Menu", Color.DarkGray, () => { });
        }

        public Color TaskbarColor { get; set; } = Color.Gray;
        public bool ShowMenuButton { get; set; } = true;
        public bool ShowDivider { get; set; } = true;
        public bool ShowAvailableWindow { get; set; } = true;

        public Button MenuButton { get; set; }
        public List<Button> WindowButton { get; set; }
    
        public void Render()
        {
            Kernel.Desktop.Screen.DrawFilledRectangle(TaskbarColor, 0, Desktop.Height - 30, Desktop.Width, 30);
            Kernel.Desktop.Screen.DrawFilledRectangle(Color.White, 82, (Desktop.Height - 30) - 10, 10, 20);
            MenuButton.Render();
        }
    }
}
