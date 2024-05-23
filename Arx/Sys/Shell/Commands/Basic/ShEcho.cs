using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Arx.Sys.Shell.Commands.Basic
{
    public class ShEcho : ShellCommand
    {
        public ShEcho() : base("echo", "Print back argument", Sys.User.UserAccess.Guest) { }

        public override void Execute(string[] args)
        {
            if (args[0] == ">")
            {
                try
                {
                    string[] fileLines = null!;
                    if (!args[1].Contains(Kernel.currentDir))
                    {
                        fileLines = File.ReadAllLines(Kernel.currentDir + args[1]);
                    }
                    else
                    {
                        fileLines = File.ReadAllLines(args[1]);
                    }
                    
                    for(int i = 0; i < fileLines.Length; i++)
                    {
                        Stdio.Write.Println(fileLines[i]);
                    }
                }
                catch (Exception ex)
                {
                    Stdio.Write.Println("Exception: " + ex.ToString(), ConsoleColor.Red);
                }
            }
            else if (args[0] == ">>")
            {
                try
                {
                    if (!args[1].Contains(Kernel.currentDir))
                    {
                        using (StreamWriter write = new StreamWriter(Kernel.currentDir + args[1], true))
                        {
                            write.AutoFlush = true;
                            List<StringBuilder> textLine = new List<StringBuilder>();
                            int currentLine = 0;
                            textLine.Add(new StringBuilder());
                            Stdio.Write.Println("Press ESC to finish - Press Enter for new line");
                            while (true)
                            {
                                ConsoleKeyInfo key = Console.ReadKey();

                                if (key.Key == ConsoleKey.Enter)
                                {
                                    textLine.Add(new StringBuilder());
                                    Stdio.Write.Println("");
                                    currentLine++;
                                }
                                else if (key.Key == ConsoleKey.Escape)
                                {
                                    Stdio.Write.Println("");
                                    break;
                                }
                                else if (key.Key == ConsoleKey.Backspace)
                                {
                                    if (textLine[currentLine].Length != 0)
                                    {
                                        textLine[currentLine].Length--;
                                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                        Stdio.Write.Print(" ");
                                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                    }
                                }
                                else
                                {
                                    textLine[currentLine].Append(key.KeyChar);
                                }
                            }

                            foreach (var i in textLine)
                            {
                                write.WriteLine(i.ToString());
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter write = new StreamWriter(args[1], true))
                        {
                            write.AutoFlush = true;
                            List<StringBuilder> textLine = new List<StringBuilder>();
                            int currentLine = 0;
                            textLine.Add(new StringBuilder());
                            Stdio.Write.Println("Press ESC to finish - Press Enter for new line");
                            while (true)
                            {
                                ConsoleKeyInfo key = Console.ReadKey();

                                if (key.Key == ConsoleKey.Enter)
                                {
                                    textLine.Add(new StringBuilder());
                                    Stdio.Write.Println("");
                                    currentLine++;
                                }
                                else if (key.Key == ConsoleKey.Escape)
                                {
                                    Stdio.Write.Println("");
                                    break;
                                }
                                else if (key.Key == ConsoleKey.Backspace)
                                {
                                    textLine[currentLine].Length--;
                                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                    Stdio.Write.Print(" ");
                                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                }
                                else
                                {
                                    textLine[currentLine].Append(key.KeyChar);
                                }
                            }

                            foreach (var i in textLine)
                            {
                                write.WriteLine(i.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Stdio.Write.Println("Exception: " + ex.ToString(), ConsoleColor.Red);
                }
            }
            else
            {
                StringBuilder echoText = new StringBuilder();
                for (int i = 0; i < args.Length; i++)
                {
                    echoText.Append(args[i] + " ");
                }
                Stdio.Write.Println(echoText.ToString());
            }
        }
    }
}
