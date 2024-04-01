using System;
using System.IO;

namespace Arx.Sys.Shell.Commands
{
    public class ShCd : ShellCommand
    {
        public ShCd() : base("cd", "Changes directory") { }

        public override void Execute(string[] args)
        {
            if(args.Length == 0)
            {
                Stdio.Write.Println("Argument can't be 0!", ConsoleColor.Red);
            }
            else
            {
                if (args[0] == "..")
                {
                    string[] dir = Kernel.currentDir.Split(@"\");
                    Kernel.currentDir = "";
                    for(int i = 0; i < dir.Length; i++) { if (dir[i] != dir[dir.Length - 1]) { Kernel.currentDir += dir[i] + @"\"; } }
                }
                else
                {
                    try
                    {
                        if (Directory.Exists(Kernel.currentDir + @"\" + args[0]))
                        {
                            Kernel.currentDir += args[0];
                        }
                        else
                        {
                            if (Directory.Exists(args[0]))
                            {
                                Kernel.currentDir = args[0];
                            }
                            else
                            {
                                Stdio.Write.Println("Directory not found!", ConsoleColor.Red);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Stdio.Write.Println("Exception: " + ex, ConsoleColor.Red);
                    }
                }
            }
        }
    }
}
