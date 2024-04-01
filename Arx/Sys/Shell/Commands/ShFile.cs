using System;
using System.IO;

namespace Arx.Sys.Shell.Commands
{
    public class ShFile : ShellCommand
    {
        public ShFile() : base("file", "File acessing command") { }

        public override void Execute(string[] args)
        {
            if(args.Length == 0)
            {
                Stdio.Write.Println("Argument can't be 0!", ConsoleColor.Red);
            }
            else
            {
                if (args[0] == "-c" || args[0] == "-C" || args[0].ToLower() == "--create")
                {
                    try
                    {
                        if (!args[1].Contains(Kernel.currentDir))
                        {
                            File.Create(Kernel.currentDir + args[1]);
                        }
                        else
                        {
                            File.Create(args[1]);
                        }
                    }
                    catch (Exception ex)
                    {
                        Stdio.Write.Println("Exception: " + ex.ToString(), ConsoleColor.Red);
                    }
                }
                else if (args[0] == "-r" || args[0] == "-R" || args[0].ToLower() == "-rm" || args[0].ToLower() == "--remove")
                {
                    try
                    {
                        if (!args[1].Contains(Kernel.currentDir))
                        {
                            File.Delete(Kernel.currentDir + args[1]);
                        }
                        else
                        {
                            File.Delete(args[1]);
                        }
                    }
                    catch (Exception ex)
                    {
                        Stdio.Write.Println("Exception: " + ex.Message, ConsoleColor.Red);
                    }
                }
                else if (args[0].ToLower() == "-fi" || args[0].ToLower() == "--find")
                {
                    try
                    {
                        if (File.Exists(args[1]))
                        {
                            Stdio.Write.Print("File found!: ", ConsoleColor.Green);
                            Stdio.Write.Println(args[1]);
                        }
                        else
                        {
                            Stdio.Write.Println("File not found!", ConsoleColor.Red);
                        }
                    }
                    catch (Exception ex)
                    {
                        Stdio.Write.Println("Exception: " + ex.Message, ConsoleColor.Red);
                    }
                }
            }
        }
    }
}
