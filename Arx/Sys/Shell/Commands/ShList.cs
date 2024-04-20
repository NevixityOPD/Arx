using System;
using System.IO;

namespace Arx.Sys.Shell.Commands
{
    public class ShList : ShellCommand
    {
        public ShList() : base("ls", "List all files and directory on current directory") { }

        public override void Execute(string[] args)
        {
            if(args.Length == 0)
            {
                string[] Directories = Directory.GetDirectories(Kernel.currentDir);
                string[] Files = Directory.GetFiles(Kernel.currentDir);

                Stdio.Write.Println($"Listing file and directory from {Kernel.currentDir}");

                for(int i = 0; i < Directories.Length; i++) { Stdio.Write.Println($"\tDirectory: {Directories[i]}", ConsoleColor.Green); }
                for(int i = 0; i < Files.Length; i++) { Stdio.Write.Println($"\tFile: {Files[i]}"); }
                Stdio.Write.Println($"Theres total of :");
                Stdio.Write.Print($"    {Directories.Length}"); Stdio.Write.Println($" Directory(s)", ConsoleColor.Green);
                Stdio.Write.Print($"    {Files.Length}"); Stdio.Write.Println($" File(s)");
            }
            else
            {
                try
                {
                    if (Directory.Exists(args[0]))
                    {
                        string[] Directories = Directory.GetDirectories(args[0]);
                        string[] Files = Directory.GetFiles(args[0]);

                        Stdio.Write.Println($"Listing file and directory from {args[0]}");

                        for (int i = 0; i < Directories.Length; i++) { Stdio.Write.Println($"\tDirectory: {Directories[i]}", ConsoleColor.Green); }
                        for (int i = 0; i < Files.Length; i++) { Stdio.Write.Println($"\tFile: {Files[i]}"); }
                        Stdio.Write.Println($"Theres total of :");
                        Stdio.Write.Print($"    {Directories.Length}"); Stdio.Write.Println($" Directory(s)", ConsoleColor.Green);
                        Stdio.Write.Print($"    {Files.Length}"); Stdio.Write.Println($" File(s)");
                    }
                }
                catch (Exception ex)
                {
                    Stdio.Write.Println("Exception: " + ex.ToString(), ConsoleColor.Red);
                }
            }
        }
    }
}
