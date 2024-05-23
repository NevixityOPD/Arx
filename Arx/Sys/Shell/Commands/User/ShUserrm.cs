using Arx.Sys.Stdio;
using System;
using System.IO;

namespace Arx.Sys.Shell.Commands.User
{
    public class ShUserrm : ShellCommand
    {
        public ShUserrm() : base("userrm", "Remove existing user", Sys.User.UserAccess.Root) { }

        public override void Execute(string[] args)
        {
            if(args.Length == 0)
            {
                Write.Println("Argument can't be 0!", ConsoleColor.Red);
            }
            else
            {
                if (Directory.Exists($@"0:\System\Users\{args[0]}\"))
                {
                    string[] userdata = File.ReadAllLines($@"0:\System\Users\{args[0]}\user.dat");
                    int attempt = 3;

                    if(Kernel.UserManager.user.userName == userdata[1])
                    {
                        Kernel.UserManager.SetUserAsGuest();
                    }

                    if (!string.IsNullOrEmpty(args[1]))
                    {
                    ReEnterPassword:
                        string password = Read.Prompt("Remove user", ConsoleColor.Green, "Enter user password");
                        if (password != userdata[0])
                        {
                            if (attempt != 0)
                            {
                                attempt--;
                                Write.Println("Wrong password!", ConsoleColor.Red);
                                goto ReEnterPassword;
                            }
                            else
                            {
                                Write.Println("Failed to remove user!", ConsoleColor.Red);
                            }
                        }
                        else
                        {
                            Directory.Delete($@"0:\System\Users\{args[0]}\", true);

                            if (Directory.Exists($@"0:\System\Users\{args[0]}\"))
                            {
                                Write.Println("Unable to remove user!", ConsoleColor.Red);
                            }
                            else
                            {
                                Write.Println("User removed", ConsoleColor.Green);
                            }
                        }
                    }
                    else
                    {
                        Directory.Delete($@"0:\System\Users\{args[0]}\", true);

                        if (Directory.Exists($@"0:\System\Users\{args[0]}\"))
                        {
                            Write.Println("Unable to remove user!", ConsoleColor.Red);
                        }
                        else
                        {
                            Write.Println("User removed", ConsoleColor.Green);
                        }
                    }
                }
                else
                {
                    Write.Println("Unable to find user!", ConsoleColor.Red);
                }
            }
        }
    }
}
