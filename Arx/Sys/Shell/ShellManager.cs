using System;
using System.Collections.Generic;
using System.IO;

using Arx.Sys.Stdio;
using Arx.Sys.User;

using Arx.Sys.Shell.Commands.Basic;
using Arx.Sys.Shell.Commands.Filesystem;
using Arx.Sys.Shell.Commands.Other;
using Arx.Sys.Shell.Commands.User;

namespace Arx.Sys.Shell
{
    public class ShellManager
    {
        public List<ShellCommand> commands { get; } = new List<ShellCommand>()
        {
            new ShFile(),
            new ShList(),
            new ShDir(),
            new ShClear(),
            new ShPower(),
            new ShEcho(),
            new ShCd(),
            new ShColor(),
            new ShPwd(),
            new ShLogin(),
            new ShAsm(),
            new ShUseradd(),
            new ShUserrm(),
            new ShGui(),
            new ShHelp(),
        };

        public void Call()
        {
            Stdio.Write.Print($"{Kernel.UserManager.user.userName}@{Kernel.UserManager.UserAccessToString(Kernel.UserManager.user.userAccess)}", ConsoleColor.Cyan);  
            Stdio.Write.Print(" ~:> ");
            string commandln = Console.ReadLine();

            string[] commandlnSplit = commandln.Split(' ');
            List<string> args = new List<string>();

            for(int i = 0; i < commandlnSplit.Length; i++)
            { if(i != 0) { args.Add(commandlnSplit[i]); } }
            
            foreach (var i in commands) 
            { 
                if(commandlnSplit[0] == i.shellName) 
                { 
                    if(i.shellAccess == UserAccess.Guest)
                    {
                        i.Execute(args.ToArray());
                    }
                    else if (i.shellAccess == UserAccess.User)
                    {
                        if((int)Kernel.UserManager.user.userAccess >= 1) { i.Execute(args.ToArray()); }
                        else
                        {
                            Write.Println("Insufficient user access!", ConsoleColor.Red);
                        }
                    }
                    else if(i.shellAccess == UserAccess.Root)
                    {
                        if ((int)Kernel.UserManager.user.userAccess >= 2) { i.Execute(args.ToArray()); }
                        else
                        {
                            Write.Println("Insufficient user access!", ConsoleColor.Red);
                        }
                    }
                    else if(i.shellAccess == UserAccess.System)
                    {
                        if ((int)Kernel.UserManager.user.userAccess == 3) { i.Execute(args.ToArray()); }
                        else
                        {
                            Write.Println("Insufficient user access!", ConsoleColor.Red);
                        }
                    }
                } 
            }

            if (File.Exists(@"0:\System\Log\command.log"))
            {
                using (StreamWriter write = new StreamWriter(@"0:\System\Log\command.log", true))
                {
                    write.AutoFlush = true;
                    write.WriteLine(commandln);
                    write.Close();
                }
            }
            else
            {
                File.Create(@"0:\System\Log\command.log");
                using (StreamWriter write = new StreamWriter(@"0:\System\Log\command.log", true))
                {
                    write.AutoFlush = true;
                    write.WriteLine(commandln);
                    write.Close();
                }
            }
        }
    }
}
