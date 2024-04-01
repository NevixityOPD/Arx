using System;
using System.Collections.Generic;
using System.IO;

namespace Arx.Sys.Shell.Commands
{
    public class ShDir : ShellCommand
    {
        public ShDir() : base("dir", "Directory acessing command") { }

        public override void Execute(string[] args)
        {
            if(args.Length == 0)
            {
                Stdio.Write.Println($"Argument can't be 0!", ConsoleColor.Red);
            }
            else
            {
                if (args[0] == "-c" || args[0] == "-C" || args[0].ToLower() == "--create")
                {
                    try
                    {
                        if (!args[1].Contains(Kernel.currentDir))
                            Directory.CreateDirectory(Kernel.currentDir + args[1]);
                        else if (!args[1].Contains(@"0:\"))
                            Directory.CreateDirectory(@"0:\" + args[1]);
                        else
                            Directory.CreateDirectory(args[1]);
                    }
                    catch(Exception ex)
                    {
                        Stdio.Write.Println($"Exception: {ex.ToString()}");
                    }
                }
                else if (args[0] == "-r" || args[0] == "-rm" || args[0].ToLower() == "--remove")
                {
                    try
                    {
                        if (!args[1].Contains(Kernel.currentDir))
                            Directory.Delete(Kernel.currentDir + args[1].Replace("\"", ""), true);
                        else if (!args[1].Contains(@"0:\"))
                            Directory.Delete(@"0:\" + args[1].Replace("\"", ""), true);
                        else
                            Directory.Delete(args[1].Replace("\"", ""), true);
                    }
                    catch (Exception ex)
                    {
                        Stdio.Write.Println($"Exception: {ex.ToString()}", ConsoleColor.Red);
                    }
                }
            }
        }
    }
}
