using Arx.Sys.Stdio;
using System;

namespace Arx.Sys.Shell.Commands.Basic
{
    public class ShColor : ShellCommand
    {
        public ShColor() : base("color", "Changes console color", Sys.User.UserAccess.Guest) { }

        public override void Execute(string[] args)
        {
            if(args.Length == 0)
            {
                Stdio.Write.Println("Specify color code!", ConsoleColor.Red);
                Write.Println("\n");
                for(int i = 0; i < 15; i++)
                {
                    Write.Print($" {i} "); Console.BackgroundColor = intToConsoleColor(i); Write.Print("   "); Console.ResetColor(); Write.Println("");
                }
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
                        Stdio.Write.Println("There's only 16 color available! (0 - 15)", ConsoleColor.Red);
                    }
                    else
                    {
                        Console.ForegroundColor = (ConsoleColor)int.Parse(args[0]);
                    }

                    if (int.Parse(args[1]) > 15)
                    {
                        Stdio.Write.Println("There's only 16 color available! (0 - 15)", ConsoleColor.Red);
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

        public ConsoleColor intToConsoleColor(int code)
        {
            switch(code)
            {
                case 0:
                    return ConsoleColor.Black;
                case 1:
                    return ConsoleColor.DarkBlue;
                case 2:
                    return ConsoleColor.DarkGreen;
                case 4:
                    return ConsoleColor.DarkRed;
                case 5:
                    return ConsoleColor.DarkMagenta;
                case 6:
                    return ConsoleColor.DarkYellow;
                case 7:
                    return ConsoleColor.Gray;
                case 8:
                    return ConsoleColor.DarkGray;
                case 9:
                    return ConsoleColor.Blue;
                case 10:
                    return ConsoleColor.Green;
                case 11:
                    return ConsoleColor.Cyan;
                case 12:
                    return ConsoleColor.Red;
                case 13:
                    return ConsoleColor.Magenta;
                case 14:
                    return ConsoleColor.Yellow;
                case 15:
                    return ConsoleColor.White;
            }

            return ConsoleColor.Black;
        }
    }
}
