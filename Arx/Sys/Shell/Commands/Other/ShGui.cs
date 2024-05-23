using System;
using System.Collections.Generic;

namespace Arx.Sys.Shell.Commands.Other
{
    public class ShGui : ShellCommand
    {
        public ShGui() : base("gui", "Starts a graphical user interface", Sys.User.UserAccess.User) { }

        public override void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Kernel.Desktop = new GUI.Desktop(false);
                Kernel.GUIMode = true;
            }
            else
            {
                if (args[0] == "debug")
                {
                    Kernel.Desktop = new GUI.Desktop(true);
                    Kernel.GUIMode = true;
                }
                else
                {
                    Kernel.Desktop = new GUI.Desktop(false);
                    Kernel.GUIMode = true;
                }
            }
        }
    }
}
