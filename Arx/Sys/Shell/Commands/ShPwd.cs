using System;
using System.Collections.Generic;

namespace Arx.Sys.Shell.Commands
{
    public class ShPwd : ShellCommand
    {
        public ShPwd() : base("pwd", "Output current directory") { }

        public override void Execute(string[] args)
        {
            Stdio.Write.Println(Kernel.currentDir);
        }
    }
}
