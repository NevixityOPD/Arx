using System;

namespace Arx.Sys.Shell.Commands.Basic
{
    public class ShClear : ShellCommand
    {
        public ShClear() : base("clear", "Clears the screen", Sys.User.UserAccess.Guest) { }

        public override void Execute(string[] args)
        {
            Console.Clear();
        }
    }
}
