using Arx.Sys.Stdio;
using System;
using System.IO;

namespace Arx.Sys.Shell.Commands.Filesystem
{
    public class ShFile : ShellCommand
    {
        public ShFile() : base("file", "File acessing command", Sys.User.UserAccess.User) { }

        public override void Execute(string[] args)
        {
            if(args.Length == 0)
            {
                Stdio.Write.Println("Argument can't be 0!", ConsoleColor.Red);
            }
            else
            {
                if (args[0] == "-c" || args[0] == "--create")
                {
                    try
                    {
                        if (args[1].StartsWith($@"0:\"))
                        {
                            if (Directory.Exists(Directory.GetParent(args[1]).FullName))
                            {
                                if (!File.Exists(args[1]))
                                {
                                    File.Create(args[1]);
                                }
                                else
                                {
                                    Write.Println("File already existed!", ConsoleColor.Red);
                                }
                            }
                            else
                            {
                                Write.Println("Directory does not exist!", ConsoleColor.Red);
                            }
                        }
                        else
                        {
                            if (!File.Exists(Kernel.currentDir + '\\' + args[1]))
                            {
                                File.Create(Kernel.currentDir + '\\' + args[1]);
                            }
                            else
                            {
                                Write.Println("File already existed!", ConsoleColor.Red);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Stdio.Write.Println("Exception: " + ex.ToString(), ConsoleColor.Red);
                    }
                }
                else if (args[0] == "-rm" || args[0] == "--remove")
                {
                    try
                    {
                        if (args[1].StartsWith($@"0:\"))
                        {
                            if (Directory.Exists(Directory.GetParent(args[1]).FullName))
                            {
                                if (!File.Exists(args[1]))
                                {
                                    Write.Println("File does not exist!", ConsoleColor.Red);
                                }
                                else
                                {
                                    File.Delete(args[1]);
                                }
                            }
                            else
                            {
                                Write.Println("Directory does not exist!", ConsoleColor.Red);
                            }
                        }
                        else
                        {
                            if (!File.Exists(Kernel.currentDir + '\\' + args[1]))
                            {
                                Write.Println("File does not exist!", ConsoleColor.Red);
                            }
                            else
                            {
                                File.Delete(Kernel.currentDir + '\\' + args[1]);
                            }
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
