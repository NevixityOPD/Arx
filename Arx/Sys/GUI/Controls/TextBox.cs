using Cosmos.System;
using CosmosTTF;
using System;
using System.Drawing;
using System.Text;

namespace Arx.Sys.GUI.Controls
{
    public class TextBox : Control
    {
        //Test

        public TextBox(uint X, uint Y, ushort Width, Action EnterKeyEvent)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.EnterKeyEvent = EnterKeyEvent;

            TextEnd = (Width - 2) / Cosmos.System.Graphics.Fonts.PCScreenFont.Default.Width;
        }

        public override uint X { get; set; }
        public override uint Y { get; set; }
        public ushort Width { get; set; }
        public const ushort Height = 26;
        
        public StringBuilder text = new StringBuilder();
        public Action EnterKeyEvent;

        public override bool IsVisible { get; set; }
        private bool IsWriting = false;

        public readonly Color BackgroundColor = Color.White;
        private KeyEvent key = null!;

        public int TextStart = 0;
        public int TextEnd;

        public override void Render(string[] args)
        {
            Kernel.Desktop.Screen.DrawRectangle(Color.Black, (int)X, (int)Y, Width, Height);
            Kernel.Desktop.Screen.DrawFilledRectangle(BackgroundColor, (int)X + 1, (int)Y + 1, Width - 1, Height - 1);
            //if (text.Length >= TextEnd) { Kernel.Desktop.Screen.DrawStringTTF(Color.Black, text.Substring(TextStart, TextEnd), "calibri", 16, new Point((int)X + 4, (int)Y + 18)); }
            //else { Kernel.Desktop.Screen.DrawStringTTF(Color.Black, text, "calibri", 16, new Point((int)X + 4, (int)Y + 18)); }
            
            if(text.Length > TextEnd) { Kernel.Desktop.Screen.DrawString(text.ToString(TextStart, TextEnd), Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, (int)X + 4, (int)Y + 4); }
            else { Kernel.Desktop.Screen.DrawString(text.ToString(), Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, (int)X + 4, (int)Y + 4); }

            if (IsMouseWithin((int)X + 1, (int)Y + 1, (ushort)(Width - 1), (ushort)(Height - 1)))
            {
                if(Kernel.Desktop.mouse.cursor != Kernel.typeCursorBitmap)
                {
                    Kernel.Desktop.mouse.ChangeCursor(Kernel.typeCursorBitmap);
                }
            }
            else
            {
                Kernel.Desktop.mouse.ChangeCursor(Kernel.cursorBitmap);
            }

            if(MouseManager.MouseState == MouseState.Left && MouseManager.LastMouseState == MouseState.None && IsMouseWithin((int)X + 1, (int)Y + 1, (ushort)(Width - 1), (ushort)(Height - 1)))
            { IsWriting = true; }

            if (MouseManager.MouseState == MouseState.Left && MouseManager.LastMouseState == MouseState.None && !IsMouseWithin((int)X + 1, (int)Y + 1, (ushort)(Width - 1), (ushort)(Height - 1)))
            { IsWriting = false; }

            if(IsWriting)
            {
                if(KeyboardManager.TryReadKey(out key))
                {
                    if (key.Key == ConsoleKeyEx.Backspace)
                    {
                        if(text.Length != 0)
                        {
                            if (TextStart != 0) { TextStart--; }
                            text.Length--;

                            if(TextStart < 0) { TextStart = 0; }
                        }
                    }
                    else if(key.Key == ConsoleKeyEx.Tab)
                    {
                        text.Append("    ");
                        if (text.Length > TextEnd) { TextStart += 4; }
                    }
                    else if (key.Key == ConsoleKeyEx.Enter || key.Key == ConsoleKeyEx.NumEnter)
                    {
                        if (EnterKeyEvent != null!)
                        {
                            EnterKeyEvent();
                        }
                    }
                    else
                    {
                        text.Append(key.KeyChar);
                        if (text.Length > TextEnd) { TextStart++; }
                    }
                }
            }
        }

        private static bool IsMouseWithin(int X, int Y, ushort Width, ushort Height)
        {
            return
                MouseManager.X >= X && MouseManager.X <= X + Width &&
                MouseManager.Y >= Y && MouseManager.Y <= Y + Height;
        }
    }
}
