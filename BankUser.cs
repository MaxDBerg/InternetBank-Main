using System;
using System.Collections.Generic;
using System.Text;

namespace CampusVarberg___InternetBank4
{
    internal class BankUser
    {
        //Fields
        public string UserName { get; set; } //Contains the username of the user
        private string password { get; set; } //Can only be accessed from within the class
        public string Password { get { return password; } } // lets us read the password variable outside the class 
        public int AccountsPerUser { get; set; } //Contains how many accounts this user has

        //Constructor
        public BankUser(string userName, string password, int accountsPerUser)
        {
            UserName = userName;
            this.password = password;
            AccountsPerUser = accountsPerUser;
        }


    }
}
