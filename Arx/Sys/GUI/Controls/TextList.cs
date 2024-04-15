using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Arx.Sys.GUI.Controls
{
    public class TextList : Control
    {
        public override uint X { get; set; }
        public override uint Y { get; set; }
        public override bool IsVisible { get; set; }

        public ushort TextSpacing { get; set; } = 4;
        public ushort FontSize { get; set; } = 10;
        public Color TextColor { get; set; }

        public TextList(uint x, uint y, Color textColor, ushort fontSize = 10, ushort textSpacing = 4)
        {
            X = x;
            Y = y;

            TextColor = textColor;
            TextSpacing = textSpacing;
            FontSize = fontSize;
        }

        public override void Render(string[] args)
        {
            for(int i = 0; i < args.Length; i++)
            {
                //Kernel.Desktop.Screen.DrawStringTTF(TextColor, args[i], "calibri", FontSize, new Point((int)X, (int)(Y + 16) + (TextSpacing * i)));
                Kernel.Desktop.Screen.DrawString(args[i], Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Color.Black, (int)X, (int)(Y + 16) + (TextSpacing * i));
            }
        }
    }
}
