using Arx.Sys.Language;
using System;
using System.IO;

namespace Arx.Sys.Shell.Commands.Other
{
    public class ShAsm : ShellCommand
    {
        public ShAsm() : base("asm", "Arx assembly language", Sys.User.UserAccess.User) { asm = new ArxAssembly(); }

        private ArxAssembly asm;

        public override void Execute(string[] args)
        {
            if(args.Length == 0)
            {
                Stdio.Write.Println("Argument can't be 0!", ConsoleColor.Red);
            }
            else
            {
                try
                {
                    if (!args[0].Contains(Kernel.currentDir))
                    {
                        asm.interpretCode(File.ReadAllLines(Kernel.currentDir + args[0]));
                    }
                    else
                    {
                        asm.interpretCode(File.ReadAllLines(args[0]));
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
