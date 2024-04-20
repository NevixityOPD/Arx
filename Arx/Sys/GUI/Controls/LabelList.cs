using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Arx.Sys.GUI.Controls
{
    public class LabelList : Control
    {
        public LabelList(uint X, uint Y, ushort textSpacing, string[] textList, Color textColor)
        {
            this.X = X;
            this.Y = Y;
            this.textList = textList;
            this.textColor = textColor;
            this.textSpacing = textSpacing;

            Width = (ushort)((Kernel.font.Width + 2) * textList.Max(x => x.Length).ToString().Length);
            if(textSpacing == 0) { Height = (ushort)(Kernel.font.Height * textList.Length); }
            else { Height = (ushort)(Kernel.font.Height * textList.Length + textSpacing * textList.Length); }
        }

        public override uint X { get; set; }
        public override uint Y { get; set; }
        public override uint StaticY { get; set; }
        public override uint StaticX { get; set; }

        public override ushort Width { get; set; }
        public override ushort Height { get; set; }

        public override bool IsVisible { get; set; }

        public string[] textList;
        public Color textColor;
        public ushort textSpacing;

        public override void Render()
        {
            for(int i = 0; i < textList.Length; i++)
            {
                Kernel.Desktop.Screen.DrawString(textList[i], Kernel.font, textColor, (int)X, (int)(Y + 16) + (textSpacing * i));
            }
        }
    }
}
