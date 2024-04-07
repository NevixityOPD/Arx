using Arx.Sys.User;
using System;
using System.Collections.Generic;

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
        };

        public void Call()
        {
            Stdio.Write.Print($"{Kernel.UserManager.user.userName}@{UserManager.UserAccessToString(Kernel.UserManager.user.userAccess)}", ConsoleColor.Cyan);  
            Stdio.Write.Print(" ~:> ");
            string commandln = Console.ReadLine();

            string[] commandlnSplit = commandln.Split(' ');
            List<string> args = new List<string>();

            foreach (var i in commandlnSplit) 
            { if(i != commandlnSplit[0]) { args.Add(i); } }
            foreach (var i in commands) 
            { if(commandlnSplit[0] == i.shellName) { i.Execute(args.ToArray()); } }
        }
    }
}
