using System;
using System.Collections.Generic;

namespace Arx.Sys.Stdio
{
    public static class Write
    {
        public static void Println(string text) => Console.WriteLine(text);
        public static void Println(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void Print(string text) => Console.Write(text);
        public static void Print(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
