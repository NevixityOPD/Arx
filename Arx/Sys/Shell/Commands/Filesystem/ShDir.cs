using Arx.Sys.Stdio;
using System;
using System.Collections.Generic;
using System.IO;

namespace Arx.Sys.Shell.Commands.Filesystem
{
    public class ShDir : ShellCommand
    {
        public ShDir() : base("dir", "Directory acessing command", Sys.User.UserAccess.User) { }

        public override void Execute(string[] args)
        {
            if(args.Length == 0)
            {
                Write.Println("Argument cannot be 0!", ConsoleColor.Red);
            }
            else
            {
                if (args[0] == "-c" || args[0] == "--create")
                {
                    if (args[1].StartsWith($@"0:\"))
                    {
                        if (Directory.Exists(Directory.GetParent(args[1]).FullName))
                        {
                            if (!Directory.Exists(args[1]))
                            {
                                Directory.CreateDirectory(args[1]);
                            }
                            else
                            {
                                Write.Println("Directory already existed!", ConsoleColor.Red);
                            }
                        }
                        else
                        {
                            Write.Println("Directory does not exist!", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(Kernel.currentDir + '\\' + args[1]))
                        {
                            Directory.CreateDirectory(Kernel.currentDir + '\\' + args[1]);
                        }
                        else
                        {
                            Write.Println("Directory already existed!", ConsoleColor.Red);
                        }
                    }
                }
                else if (args[0] == "-rm" || args[0] == "--remove")
                {
                    if (args[1].StartsWith($@"0:\"))
                    {
                        if (Directory.Exists(Directory.GetParent(args[1]).FullName))
                        {
                            if (!Directory.Exists(args[1]))
                            {
                                Write.Println("Directory does not exist!", ConsoleColor.Red);
                            }
                            else
                            {
                                Directory.Delete(args[1]);
                            }
                        }
                        else
                        {
                            Write.Println("Directory does not exist!", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(Kernel.currentDir + '\\' + args[1]))
                        {
                            Write.Println("Directory does not exist!", ConsoleColor.Red);
                        }
                        else
                        {
                            Directory.Delete(Kernel.currentDir + '\\' + args[1]);
                        }
                    }
                }
            }
        }
    }
}
