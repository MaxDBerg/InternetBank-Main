using System;
using System.Collections.Generic;
using System.Text;

namespace InternetBank
{
    internal class BankAccount
    {   

        //Fields
        private decimal balance { get; set; }
        public decimal Balance { get { return balance; } }

        //Constructors
        public BankAccount(decimal balance)
        {
            this.balance = balance;
        }

        //Methods
        //public bool MakeADeposit(decimal MoneyAdded)
        //{

        //}
        //public bool MakeAWithdrawel(decimal MoneyRemoved)
        //{

        //}
    }
}
