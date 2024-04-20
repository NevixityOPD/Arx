using Arx.Sys.User;
using System;
using System.Collections.Generic;
using System.IO;

namespace Arx.Sys.Shell
{
    public class ShellManager
    {
        public List<ShellCommand> commands { get; } = new List<ShellCommand>()
        {
            new Commands.ShFile(),
            new Commands.ShList(),
            new Commands.ShDir(),
            new Commands.ShClear(),
            new Commands.ShPower(),
            new Commands.ShEcho(),
            new Commands.ShCd(),
            new Commands.ShColor(),
            new Commands.ShPwd(),
            new Commands.ShLogin(),
            new Commands.ShAsm(),
            new Commands.ShUseradd(),
            new Commands.ShUserrm(),
            new Commands.ShGui(),
            new Commands.ShHelp(),
        };

        public void Call()
        {
            Stdio.Write.Print($"{Kernel.UserManager.user.userName}@{UserManager.UserAccessToString(Kernel.UserManager.user.userAccess)}", ConsoleColor.Cyan);  
            Stdio.Write.Print(" ~:> ");
            string commandln = Console.ReadLine();

            string[] commandlnSplit = commandln.Split(' ');
            List<string> args = new List<string>();

            for(int i = 0; i < commandlnSplit.Length; i++)
            { if(i != 0) { args.Add(commandlnSplit[i]); } }
            foreach (var i in commands) 
            { if(commandlnSplit[0] == i.shellName) { i.Execute(args.ToArray()); } }

            if (File.Exists(@"0:\System\Log\command.log"))
            {
                using (StreamWriter write = new StreamWriter(@"0:\System\Log\command.log"))
                {
                    write.AutoFlush = true;
                    write.WriteLine(commandln);
                    write.Close();
                }
            }
            else
            {
                File.Create(@"0:\System\Log\command.log");
                using (StreamWriter write = new StreamWriter(@"0:\System\Log\command.log"))
                {
                    write.AutoFlush = true;
                    write.WriteLine(commandln);
                    write.Close();
                }
            }
        }
    }
}
