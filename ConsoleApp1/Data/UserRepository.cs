using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class UserRepository
    {
        // Without a persistence medium, we can just have an in-memory list to represent a Context.
        private List<User> UserStore { get; set; }

        public UserRepository()
        {
            UserStore = new List<User>();
        }

        public User Get(string username)
        {
            return UserStore.Find(u => u.Username == username);
        }

        public User Add(string username)
        {
            User newUser = null;
            // We want at least one character for a username, and we don't want duplicates.
            if (username.Length != 0 && !UserStore.Exists(u => u.Username == username))
            {
                newUser = new User { Username = username };
                UserStore.Add(newUser);
            }

            return newUser;
        }
    }
}
