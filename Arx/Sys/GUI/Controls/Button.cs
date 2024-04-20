using Cosmos.System;
using System;
using System.Drawing;

namespace Arx.Sys.GUI.Controls
{
    public class Button : Control
    {
        public Button(uint X, uint Y, ushort Width, ushort Height, string ButtonText, Color ButtonColor, Action ButtonPressedEvent)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.ButtonText = ButtonText;
            this.ButtonPressedEvent = ButtonPressedEvent;
            this.ButtonColor = ButtonColor;

            StaticX = X;
            StaticY = Y;
        }

        public override uint X { get; set; }
        public override uint Y { get; set; }
        public override uint StaticX { get; set; }
        public override uint StaticY { get; set; }
        public override ushort Width { get; set; }
        public override ushort Height { get; set; }

        public string ButtonText { get; set; }

        public Color ButtonColor { get; set; } = Color.White;
        public Action ButtonPressedEvent { get; set; }

        public override bool IsVisible { get; set; }
        public bool ShowShadow { get; set; } = false;

        public bool IsClicked = false;

        public override void Render()
        {
            if(ShowShadow) { Kernel.Desktop.Screen.DrawFilledRectangle(Color.FromArgb(17, 18, 17), (int)X + 4, (int)Y + 4, Width, Height); }
            Kernel.Desktop.Screen.DrawFilledRectangle(ButtonColor, (int)X, (int)Y, Width, Height);

            if (!string.IsNullOrEmpty(ButtonText))
            {
                int textX;
                int textY = (Height / 2 - Kernel.font.Height / 2);

                int textWidth = ButtonText.Length * Kernel.font.Width;
                textX = (Width / 2) - (textWidth / 2);

                Kernel.Desktop.Screen.DrawString(ButtonText, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, (int)(textX + X), (int)(textY + Y));
            }

            if (IsMouseWithin((int)X, (int)Y, Width, Height))
            {
                if (Kernel.Desktop.mouse.cursor != Kernel.pointCursorBitmap)
                {
                    Kernel.Desktop.mouse.ChangeCursor(Kernel.pointCursorBitmap);
                }
            }

            if (MouseManager.MouseState == MouseState.Left && MouseManager.LastMouseState == MouseState.None && IsMouseWithin((int)X, (int)Y, Width, Height))
            {
                IsClicked = true;
            }

            if(MouseManager.MouseState == MouseState.None && !IsMouseWithin((int)X, (int)Y, Width, Height))
            {
                IsClicked = false;
            }
            
            if(IsClicked)
            {
                ButtonPressedEvent();
            }
        }

        private static bool IsMouseWithin(int X, int Y, ushort Width, ushort Height)
        {
            return
                MouseManager.X >= X && MouseManager.X <= X + Width &&
                MouseManager.Y >= Y && MouseManager.Y <= Y + Height;
        }

        private Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }
    }
}
