using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessagesAPI.Misc
{
    public class UserNameCollection
    {
        private static UserNameCollection instance;
        //singleton pattern used for this class
        public static UserNameCollection GetInstance()
        {
            if (instance == null)
            {
                instance = new UserNameCollection();
            }
            return instance;
        }

        private Dictionary<String, String> usernames = null;
        public Dictionary<string, string> Usernames { get => usernames; set => usernames = value; }

        private UserNameCollection()
        {
            usernames = new Dictionary<string, string>();
        }

        public bool Login(string username, string password)
        {
            if (!usernames.ContainsKey(username))
            {
                usernames[username] = password;
                return true;
            }
            else if (usernames[username] == password)
                return true;
            else
                return false;
        }
    }
}