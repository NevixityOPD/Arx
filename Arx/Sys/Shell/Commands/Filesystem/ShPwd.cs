using System;
using System.Collections.Generic;

namespace Arx.Sys.Shell.Commands.Filesystem
{
    public class ShPwd : ShellCommand
    {
        public ShPwd() : base("pwd", "Output current directory", Sys.User.UserAccess.Guest) { }

        public override void Execute(string[] args)
        {
            Stdio.Write.Println(Kernel.currentDir);
        }
    }
}
