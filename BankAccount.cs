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
        public void MakeADeposit(decimal MoneyAdded)
        {
            balance += MoneyAdded;
        }
        public bool MakeAWithdrawel(decimal MoneyRemoved)
        {
            if (MoneyRemoved <= balance)
            {
                balance -= MoneyRemoved;
                return true;
            }
            else
            {
                Console.WriteLine("Transaction Failed!");
                return false;
            }

        }
    }
}
