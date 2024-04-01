using Arx.Sys.Stdio;
using System;
using System.IO;

namespace Arx.Sys.User
{
    public class UserManager
    {
        public User user = new User("Guest", "", UserAccess.Guest);

        public static string UserAccessToString(UserAccess userAccess)
        {
            switch(userAccess)
            {
                case UserAccess.Guest:
                    return "Guest";
                case UserAccess.User:
                    return "User";
                case UserAccess.Root:
                    return "Root";
                case UserAccess.System:
                    return "System";
            }

            return string.Empty;
        }

        public static UserAccess StringToUserAccess(string userAccess)
        {
            switch (userAccess)
            {
                case "Guess":
                    return UserAccess.Guest;
                case "User":
                    return UserAccess.User;
                case "Root":
                    return UserAccess.Root;
                case "System":
                    return UserAccess.System;
            }

            return UserAccess.Guest;
        }

        public void CreateUser(User newUser)
        {
            if(Directory.Exists(@"0:\System\Users\"))
            {
                try
                {
                    Directory.CreateDirectory($@"0:\System\Users\{newUser.userName}\");
                    Directory.CreateDirectory($@"0:\System\Users\{newUser.userName}\Home\");

                    File.Create($@"0:\System\Users\{newUser.userName}\user.dat");
                    File.WriteAllLines($@"0:\System\Users\{newUser.userName}\user.dat", new string[]
                    {
                        newUser.userName,
                        newUser.passWord,
                        UserAccessToString(newUser.userAccess)
                    });

                    if (Directory.Exists($@"0:\System\Users\{newUser.userName}")) { Write.Println("User created sucessfully!", ConsoleColor.Green); }
                    else { Write.Println("Failed to create new user!", ConsoleColor.Red); }
                }
                catch(Exception ex)
                {
                    Write.Println("Exception: " + ex.ToString(), ConsoleColor.Red);
                }
            }
            else
            {
                Write.Println("User directory is missing!", ConsoleColor.Red);
            }
        }

        public void RemoveUser(string username)
        {
            try
            {
                if (Directory.Exists($@"0:\System\Users\{username}\"))
                {
                    Directory.Delete($@"0:\System\Users\{username}\");
                }
                else
                {
                    Write.Println("User does not exist!", ConsoleColor.Red);
                    return;
                }

                if(!Directory.Exists($@"0:\System\Users\{username}"))
                {
                    Write.Println("User removed sucessfully!", ConsoleColor.Green);
                }
                else
                {
                    Write.Println("Failed to remove user!", ConsoleColor.Red);
                }

            }
            catch (Exception ex)
            {
                Write.Println("Exception: " + ex.ToString(), ConsoleColor.Red);
            }
        }

        public void Authenticate()
        {
            string username = Read.Prompt("Login", ConsoleColor.Green, "Username");

            if (Directory.Exists($@"0:\System\Users\{username}"))
            {
                try
                {
                    string[] userdata = null!;
                    if (File.Exists($@"0:\System\Users\{username}\user.dat"))
                    {
                        userdata = File.ReadAllLines($@"0:\System\Users\{username}\user.dat");
                    }
                    else
                    {
                        Write.Println("User data was missing!, Please remove corrupted user!", ConsoleColor.Red);
                        return;
                    }

                    string password = Read.Prompt("Login", ConsoleColor.Green, "Password");
                    if(password == userdata[1])
                    {
                        user = new User(userdata[0], userdata[1], StringToUserAccess(userdata[2]));
                    }
                    else
                    {
                        Write.Println("Wrong password!", ConsoleColor.Red);
                    }
                }
                catch (Exception ex)
                {
                    Write.Println("Exception: " + ex.ToString(), ConsoleColor.Red);
                }
            }
            else
            {
                Write.Println("User was not found!", ConsoleColor.Red);
            }
        }
    }
}
