using Arx.Sys.GUI;
using Arx.Sys.Shell;
using Arx.Sys.User;
using Cosmos.System.ExtendedASCII;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.IO;
using System.Text;
using CosmosSys = Cosmos.System;

namespace Arx
{
    public class Kernel : CosmosSys.Kernel
    {
        public static CosmosVFS FileSystem;
        public static ShellManager ShellManager;
        public static UserManager UserManager;
        public static Desktop Desktop;

        public static bool GUIMode = false;

        public static string currentDir = @"0:\";
        public static uint totalRam = Cosmos.Core.CPU.GetAmountOfRAM();

        public static ConsoleColor normalBackground = ConsoleColor.Black;
        public static ConsoleColor normalForeground = ConsoleColor.White;

        public static void SetForegroundNormal() => Console.ForegroundColor = normalForeground;
        public static void SetBackgroundNormal() => Console.BackgroundColor = normalBackground;

        [ManifestResourceStream(ResourceName = "Arx.Assets.Font.Calibri.ttf")]
        private static byte[] calibriByteData;

        [ManifestResourceStream(ResourceName = "Arx.Assets.Bitmap.Cursor.bmp")]
        private static byte[] cursorByte;
        public static Bitmap cursorBitmap = new Bitmap(cursorByte);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        protected override void BeforeRun()
        {
            Console.Clear();
            Console.SetWindowSize(90, 30);
            Encoding.RegisterProvider(CosmosEncodingProvider.Instance);
            Console.OutputEncoding = CosmosEncodingProvider.Instance.GetEncoding(437);

            CosmosTTF.TTFManager.RegisterFont("calibri", calibriByteData);

            FileSystem = new CosmosVFS();
            UserManager = new UserManager();
            VFSManager.RegisterVFS(FileSystem);
            ShellManager = new ShellManager();

            Sys.Stdio.Write.Println("Finding disk and partition");

            if (FileSystem.Disks.Count > 0) { Sys.Stdio.Write.Println("Disk found!", ConsoleColor.Green); }

            for(int i = 0; i < FileSystem.Disks.Count; i++)
            {
                Sys.Stdio.Write.Println($"Disk {i} => {FileSystem.Disks[i].Size} Bytes");
                for(int e = 0; e < FileSystem.Disks[i].Partitions.Count; e++)
                {
                    Sys.Stdio.Write.Println($"\tPartition {e} => {FileSystem.Disks[i].Partitions[e].RootPath}");
                }
            }

            ReCheck:
            Sys.Stdio.Write.Println("Running check on system directory");
            if(!Directory.Exists(@"0:\System\"))
            {
                Sys.Stdio.Write.Println("System directory hasn't been made", ConsoleColor.Red);
                Directory.CreateDirectory(@"0:\System\");

                Directory.CreateDirectory(@"0:\System\Users");
                Directory.CreateDirectory(@"0:\System\Log");
                Directory.CreateDirectory(@"0:\System\Registry");
                goto ReCheck;
            }
            else { Sys.Stdio.Write.Println("System directory found!", ConsoleColor.Green); }

            Sys.Stdio.Write.Print("Welcome to "); Sys.Stdio.Write.Println("Arx", ConsoleColor.Cyan);
        }

        protected override void Run()
        {
            if(GUIMode)
            {
                Desktop.Render();
            }
            else
            {
                ShellManager.Call();
            }
        }
    }
}
