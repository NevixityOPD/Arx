﻿using Arx.Sys.Stdio;

namespace Arx.Sys.Shell.Commands.Other
{
    public class ShHelp : ShellCommand
    {
        public ShHelp() : base("help", "Shows this list of commands", Sys.User.UserAccess.Guest) { }

        public override void Execute(string[] args)
        {
            Write.Println("List of commands.");
            foreach (var i in Kernel.ShellManager.commands)
            {
                Write.Println($"\t{i.shellName}: {i.shellDesc}");
            }
        }
    }
}