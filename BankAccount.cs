using System;
using System.Collections.Generic;
using System.Text;

namespace InternetBank
{
    internal class BankAccount
    {
        private decimal balance { get; set; }
        public decimal Balance { get { return balance; } }

        public BankAccount(decimal balance)
        {
            this.balance = balance;
        }
    }
}
