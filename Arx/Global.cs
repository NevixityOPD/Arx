using Arx.Graphics;
using Arx.Internal.Command;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Drawing;

namespace Arx
{
    public static class Global
    {
        [ManifestResourceStream(ResourceName = "Arx.Assets.Cursor.bmp")]
        private static byte[] cursorByte;
        public static Bitmap cursor = new Bitmap(cursorByte);

        [ManifestResourceStream(ResourceName = "Arx.Assets.Arrow_LR.bmp")]
        private static byte[] arrowLRbyte;
        public static Bitmap arrowlr = new Bitmap(arrowLRbyte);

        [ManifestResourceStream(ResourceName = "Arx.Assets.Arrow_UD.bmp")]
        private static byte[] arrowUDbyte;
        public static Bitmap arrowud = new Bitmap(arrowUDbyte);

        [ManifestResourceStream(ResourceName = "Arx.Assets.Type.bmp")]
        private static byte[] typeByte;
        public static Bitmap type = new Bitmap(typeByte);

        public static Canvas screen;
        public static Desktop desktop;
        public static bool GUIMode = false;

        public static Color white = Color.FromArgb(219, 219, 219);
        public static Color black = Color.FromArgb(33, 33, 33);

        public static CommandManager cmdmgr;
    }
}
