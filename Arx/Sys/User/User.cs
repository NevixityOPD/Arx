using System;
using System.Collections.Generic;

namespace Arx.Sys.User
{
    public class User
    {
        public User(string userName, string passWord, UserAccess userAccess)
        {
            this.userName = userName;
            this.passWord = passWord;
            this.userAccess = userAccess;
        }

        public string userName { get; }
        public string passWord { get; }
        public UserAccess userAccess { get; }
    }

    public enum UserAccess
    {
        Guest,
        User,
        Root,
        System
    }
}
