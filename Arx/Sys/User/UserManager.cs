using Arx.Sys.Stdio;
using System;
using System.IO;

namespace Arx.Sys.User
{
    public class UserManager
    {
        public UserManager()
        {
            user = new User("Guest", "", UserAccess.Guest);
        }

        public User user;

        public void SetUserAsGuest() { user = new User("Guest", "", UserAccess.Guest); }

        public bool CheckUserExistance(string username)
        {
            if(Directory.Exists($@"0:\System\Users\{username}"))
            {
                return true;
            }
            return false;
        }

        public string UserAccessToString(UserAccess userAccess)
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

        public UserAccess StringToUserAccess(string userAccess)
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
                    else { Write.Println("Unable to create new user!", ConsoleColor.Red); }
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
                    Write.Println("Unable to remove user!", ConsoleColor.Red);
                }

            }
            catch (Exception ex)
            {
                Write.Println("Exception: " + ex.ToString(), ConsoleColor.Red);
            }
        }

        public void SetPassword(string username)
        {
            if(!Directory.Exists($@"0:\System\Users\{username}\"))
            {
                Stdio.Write.Println("User does not exist!", ConsoleColor.Red);
            }
            else
            {
                int remainingAttempt = 3;
                ReDo:
                string password = Read.Prompt("User Manager", ConsoleColor.Green, "Enter new password");
                string passwordConfirmation = Read.Prompt("User Manager", ConsoleColor.Green, "Enter new password again");
                if(passwordConfirmation == password)
                {
                    string[] userdata = File.ReadAllLines($@"0:\System\Users\{username}\user.dat");
                    userdata[1] = password;
                    File.WriteAllLines($@"0:\System\Users\{username}\user.dat", userdata);
                    userdata = File.ReadAllLines($@"0:\System\Users\{username}\user.dat");
                    if (userdata[1] == password)
                    {
                        Write.Println("Password changed", ConsoleColor.Green);
                    }
                    else
                    {
                        Write.Println("Password didn't changed", ConsoleColor.Red);
                    }
                }
                else
                {
                    remainingAttempt--;
                    if(remainingAttempt == 0)
                    {
                        Write.Println("Failed to change password!", ConsoleColor.Red);
                        return;
                    }
                    else
                    {
                        Write.Println("Password doesn't matched!", ConsoleColor.Red);
                        goto ReDo;
                    }
                }
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

                    if (!string.IsNullOrEmpty(userdata[1]))
                    {
                        string password = Read.Prompt("Login", ConsoleColor.Green, "Password");
                        if (password == userdata[1])
                        {
                            user = new User(userdata[0], userdata[1], StringToUserAccess(userdata[2]));
                        }
                        else
                        {
                            Write.Println("Wrong password!", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        user = new User(userdata[0], userdata[1], StringToUserAccess(userdata[2]));
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
