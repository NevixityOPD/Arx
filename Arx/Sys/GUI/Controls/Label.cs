using System.Drawing;

namespace Arx.Sys.GUI.Controls
{
    public class Label : Control
    {
        public Label(uint X, uint Y, string text, Color textColor)
        {
            this.X = X;
            this.Y = Y;
            this.text = text;
            this.textColor = textColor;

            Width = (ushort)((Kernel.font.Width + 2) * text.Length);
            Height = Kernel.font.Height;
        }

        public override uint X { get; set; }
        public override uint Y { get; set; }
        public override uint StaticX { get; set; }
        public override uint StaticY { get; set; }

        public override ushort Width { get; set; }
        public override ushort Height { get; set; }

        public override bool IsVisible { get; set; }

        public string text;
        public Color textColor;

        public override void Render()
        {
            Kernel.Desktop.Screen.DrawString(text, Kernel.font, textColor, (int)X, (int)Y);
        }
    }
}
