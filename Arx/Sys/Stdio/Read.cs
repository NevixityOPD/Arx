using System;

namespace Arx.Sys.Stdio
{
    public static class Read
    {
        public static string Prompt(string title, ConsoleColor titleColor, string promptQuestion)
        {
            Write.Print($"[{title}] ", titleColor);
            Write.Print(promptQuestion + "?: ");
            string answer = Console.ReadLine();
            return answer;
        }

        public static bool BoolPrompt(string title, ConsoleColor titleColor, string promptQuestion)
        {
            Write.Print($"[{title}] ", titleColor);
            Write.Print(promptQuestion + "[Y/n]: ");
            string confirmation = Console.ReadLine();
            if (confirmation.Length == 0)
            {
                Write.Println("");
                return false;
            }
            else
            {
                if (confirmation.ToLower()[0] == 'y')
                {
                    Write.Println("");
                    return true;
                }
                else if (confirmation.ToLower()[0] == 'n')
                {
                    Write.Println("");
                    return false;
                }
                else
                {
                    Write.Println("");
                    return false;
                }
            }
        }
    }
}
