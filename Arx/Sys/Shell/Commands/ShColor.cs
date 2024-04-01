using System;
using System.Collections.Generic;

namespace Arx.Sys.Shell.Commands
{
    public class ShColor : ShellCommand
    {
        public ShColor() : base("color", "Changes console color") { }

        public override void Execute(string[] args)
        {
            if(args.Length == 0)
            {
                Stdio.Write.Println("Specify color code!", ConsoleColor.Red);
            }
            else if (args.Length == 1 || args.Length > 2)
            {
                Stdio.Write.Println("Argument can't be 1 or more than 2!", ConsoleColor.Red);
            }
            else
            {
                try
                {
                    if (int.Parse(args[0]) > 15)
                    {
                        Stdio.Write.Println("There's only 15 color available! (0 - 15)", ConsoleColor.Red);
                    }
                    else
                    {
                        Console.ForegroundColor = (ConsoleColor)int.Parse(args[0]);
                    }

                    if (int.Parse(args[1]) > 15)
                    {
                        Stdio.Write.Println("There's only 15 color available! (0 - 15)", ConsoleColor.Red);
                    }
                    else
                    {
                        Console.BackgroundColor = (ConsoleColor)int.Parse(args[0]);
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
