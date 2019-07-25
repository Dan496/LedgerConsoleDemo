using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankLedgerConsole
{
    public class User
    {
        public User()
        {
            Transactions = new List<Transaction>();
        }

        // To simplify managing accounts, we will skip authentication and only deal with uniqueness.
        public string Username { get; set; }
        // TODO: Prevent list modification, this could cause an overdrawn account without validation.
        public List<Transaction> Transactions { get; set; }
        public decimal Balance
        {
            get
            {
                if (Transactions.Count() == 0)
                {
                    return 0m;
                }
                return Transactions.Select(t => t.TransactionType.Equals(TransactionType.Withdrawl) ? -t.Amount : t.Amount).Sum();
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction.TransactionType.Equals(TransactionType.Withdrawl) && (Balance - transaction.Amount < 0m))
            {
                // A ModelErrorState attribute tracking validation errors would be a better solution if more models were to be created.
                throw new ArgumentException("Transaction would result in an overdrawn account.");
            }
            else
            {
                Transactions.Add(transaction);
            }
        }
    }
}
