using System;
using System.Collections.Generic;
using System.Text;

namespace BankLedgerConsole
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
