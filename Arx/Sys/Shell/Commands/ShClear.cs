using System;

namespace Arx.Sys.Shell.Commands
{
    public class ShClear : ShellCommand
    {
        public ShClear() : base("clear", "Clears the screen") { }

        public override void Execute(string[] args)
        {
            Console.Clear();
        }
    }
}
