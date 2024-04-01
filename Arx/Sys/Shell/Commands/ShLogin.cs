using System;
using System.Collections.Generic;

namespace Arx.Sys.Shell.Commands
{
    public class ShLogin : ShellCommand
    {
        public ShLogin() : base("login", "Log into an account") { }

        public override void Execute(string[] args)
        {
            Kernel.UserManager.Authenticate();
        }
    }
}
