using Cosmos.System;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Drawing;

namespace Arx.Graphics.Control
{
    public class Button : IControl
    {
        public Button(int x, int y, int width, int height, string buttonText, bool castShadow, Color buttonColor, Action buttonPressedAction)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            this.buttonText = buttonText;
            this.buttonColor = buttonColor;
            this.buttonPressedAction = buttonPressedAction;
            this.castShadow = castShadow;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool isRendering { get; set; }
        public string buttonText { get; set; }
        public Color buttonColor { get; set; }
        public Action buttonPressedAction { get; private set; }
        public bool isPressed;

        private bool castShadow;

        public void Render()
        {
            if(castShadow) { Global.screen.DrawFilledRectangle(Color.FromArgb(36, 36, 36), X + 2, Y + 2, Width, Height); }
            Global.screen.DrawFilledRectangle(buttonColor, X, Y, Width, Height);
            Global.screen.DrawString(buttonText, PCScreenFont.Default, Color.Black, ((Width / 2) / 2) + X, ((Height / 2) / 2) + Y);
        
            if (IsMouseWithin(X, Y, (ushort)Width, (ushort)Height) && MouseManager.MouseState == MouseState.Left && MouseManager.LastMouseState == MouseState.None)
            {
                isPressed = true;
            }

            if (MouseManager.MouseState == MouseState.None)
            {
                isPressed = false;
            }

            if (isPressed)
            {
                buttonPressedAction();
            }
        }

        private bool IsMouseWithin(int X, int Y, ushort Width, ushort Height)
        {
            return
                MouseManager.X >= X && MouseManager.X <= X + Width &&
                MouseManager.Y >= Y && MouseManager.Y <= Y + Height;
        }

        private bool IsDarker(Color col)
        {
            if (col.R * 0.2126 + col.G * 0.7152 + col.B * 0.0722 < 255 / 2)
            {
                return true;
            }

            return false;
        }
    }
}
