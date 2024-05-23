using Arx.Sys.GUI;
using Arx.Sys.Shell;
using Arx.Sys.Stdio;
using Arx.Sys.User;
using Cosmos.System.ExtendedASCII;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
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

        [ManifestResourceStream(ResourceName = "Arx.Assets.Bitmap.Cursor.Cursor.bmp")]
        private static byte[] cursorByte;
        public static Bitmap cursorBitmap = new Bitmap(cursorByte);

        [ManifestResourceStream(ResourceName = "Arx.Assets.Bitmap.Cursor.Type.bmp")]
        private static byte[] typeCursorByte;
        public static Bitmap typeCursorBitmap = new Bitmap(typeCursorByte);

        [ManifestResourceStream(ResourceName = "Arx.Assets.Bitmap.Cursor.Point.bmp")]
        private static byte[] pointCursorByte;
        public static Bitmap pointCursorBitmap = new Bitmap(pointCursorByte);

        public static PCScreenFont font = PCScreenFont.Default;

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

            Write.Println("Finding disk and partition");

            if (FileSystem.Disks.Count > 0) { Write.Println("Disk found!", ConsoleColor.Green); }

            for(int i = 0; i < FileSystem.Disks.Count; i++)
            {
                Write.Println($"Disk {i} => {FileSystem.Disks[i].Size} Bytes");
                for(int e = 0; e < FileSystem.Disks[i].Partitions.Count; e++)
                {
                    Write.Println($"\tPartition {e} => {FileSystem.Disks[i].Partitions[e].RootPath}");
                }
            }

            ReCheck:
            Write.Println("Running check on system directory");
            if(!Directory.Exists(@"0:\System\"))
            {
                Write.Println("Missing system directory", ConsoleColor.Red);

                Write.Println("Creating missing system directory", ConsoleColor.Green);
                Directory.CreateDirectory(@"0:\System\");

                Directory.CreateDirectory(@"0:\System\Users");
                Directory.CreateDirectory(@"0:\System\Logs");
                Directory.CreateDirectory(@"0:\System\Variables");
                if (!UserManager.CheckUserExistance("root"))
                {
                    string password;
                    Write.Println("Root user was missing!");
                    password = Read.Prompt("Setup", ConsoleColor.Green, "Enter root password (Optional)");
                    UserManager.CreateUser(new User("root", password, UserAccess.Root));
                }
                goto ReCheck;
            }
            else { Write.Println("System directory found!", ConsoleColor.Green); }

            Write.Print("Welcome to "); Write.Println("Arx", ConsoleColor.Cyan);
        }

        protected override void Run()
        {
            if(GUIMode)
            {
                try
                {
                    Desktop.Render();
                }
                catch(Exception ex)
                {
                    GUIMode = false;
                    Desktop.Screen.Disable();

                    Write.Println($"Unexpected exception occured while Graphic user interface is running!. {ex.ToString()}", ConsoleColor.Red);
                }
            }
            else
            {
                ShellManager.Call();
            }
        }
    }
}
