using System;
using System.Collections.Generic;
using System.Text;

namespace CampusVarberg___InternetBank4
{
    internal class BankUser
    {
        public string UserName { get; set; }
        private string password { get; set; }
        public string Password { get { return password; } }
        public int AccountsPerUser { get; set; }

        public BankUser(string userName, string password, int accountsPerUser)
        {
            UserName = userName;
            this.password = password;
            AccountsPerUser = accountsPerUser;
        }


    }
}
