using Arx.Sys.Stdio;
using System;
using System.IO;

namespace Arx.Sys.Shell.Commands.Filesystem
{
    public class ShCd : ShellCommand
    {
        public ShCd() : base("cd", "Changes directory", Sys.User.UserAccess.User) { }

        public override void Execute(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Kernel.currentDir = @"0:\";
                }
                else
                {
                    if (args[0] == "..")
                    {
                        if(Kernel.currentDir != @"0:\" || Kernel.currentDir != @"0:\\")
                        {
                            Kernel.currentDir = Directory.GetParent(Kernel.currentDir).FullName;
                        }
                    }
                    else if (args[0].StartsWith($@"0:\"))
                    {
                        if (Directory.Exists(Directory.GetParent(args[0]).FullName))
                        {
                            if (Directory.Exists(args[0]))
                            {
                                Kernel.currentDir = args[0];
                            }
                            else
                            {
                                Write.Println("Directory does not exist!", ConsoleColor.Red);
                            }
                        }
                        else
                        {
                            Write.Println("Directory does not exist!", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        if (Directory.Exists(Kernel.currentDir + '\\' + args[0]))
                        {
                            Kernel.currentDir = Kernel.currentDir + "\\" + args[0];
                        }
                        else
                        {
                            Write.Println("Directory does not exist!", ConsoleColor.Red);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Write.Println(ex.ToString(), ConsoleColor.Red);
            }
        }
    }
}
