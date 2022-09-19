using System;
using System.Collections.Generic;
using System.Text;

namespace CampusVarberg___InternetBank4
{
    internal class BankUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public BankUser(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }


    }
}
