using System;
using System.Collections.Generic;

namespace Arx.Sys.Shell.Commands
{
    public class ShPower : ShellCommand
    {
        public ShPower() : base("power", "System power control") { }

        public override void Execute(string[] args)
        {
            if(args.Length == 0)
            {
                Stdio.Write.Println("Argument can't be 0!", ConsoleColor.Red);
            }
            else
            {
                if (args[0] == "shutdown")
                {
                    Cosmos.System.Power.Shutdown();
                }
                else if (args[0] == "reboot")
                {
                    Cosmos.System.Power.Reboot();
                }
            }
        }
    }
}
