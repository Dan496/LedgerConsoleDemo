using System;
using System.Collections.Generic;
using System.Text;

namespace BankLedgerConsole
{
    class UserRepository
    {
        // No persistence, only in-memory for the demo.
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
