using System;
using System.Collections.Generic;

namespace Arx.Sys.Shell.Commands.User
{
    public class ShLogin : ShellCommand
    {
        public ShLogin() : base("login", "Log into an account", Sys.User.UserAccess.Guest) { }

        public override void Execute(string[] args)
        {
            Kernel.UserManager.Authenticate();
        }
    }
}
