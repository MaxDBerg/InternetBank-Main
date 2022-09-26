using System;
using System.Collections.Generic;
using System.Text;

namespace InternetBank
{
    internal class BankAccount
    {   

        //Fields
        private decimal balance { get; set; } //Can only be accessed from within the class
        public decimal Balance { get { return balance; } } // lets us read the balance variable outside the class 

        //Constructors
        public BankAccount(decimal balance)
        {
            this.balance = balance;
        }

        //Methods
        public void MakeADeposit(decimal MoneyAdded) //Makes the deposit
        {
            balance += MoneyAdded;
        }
        public bool MakeAWithdrawel(decimal MoneyRemoved) //Makes the withdrawel
        {
            if (MoneyRemoved <= balance) //If the money we want to remove is larger then the money in the account, we return false
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
