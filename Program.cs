    using System;

namespace InternetBank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GenerateUsers(out BankUser[] users);
            
            GenerateAccounts(users, out BankAccount[][] accounts);

            MainMenu(users, accounts); //Calls on the method MainMenu() and gives all the users and accounts as parameters

        }

        static void GenerateUsers(out BankUser[] users)
        {
            users = new BankUser[5]; //creates an array for storing objects
            BankAccount[][] accounts = new BankAccount[users.Length][];//creates a jagged array for storing objects
            users[0] = new BankUser("Max", "1234", 3); //creates instances of the class BankUser i.e an Object
            users[1] = new BankUser("Anas", "1234", 3);
            users[2] = new BankUser("Tobias", "1234", 2);
            users[3] = new BankUser("Reidar", "1234", 5);
            users[4] = new BankUser("Kristian", "1234", 4);
        }
        static void GenerateAccounts(BankUser[] users, out BankAccount[][] accounts)
        {
            accounts = new BankAccount[users.Length][];//creates a jagged array for storing objects
            for (int i = 0; i < users.Length; i++) //Generates all the accounts for all the users
            {
                accounts[i] = new BankAccount[users[i].AccountsPerUser];
                for (int j = 0; j < users[i].AccountsPerUser; j++)
                {
                    accounts[i][j] = new BankAccount((j + 1) * users[i].AccountsPerUser * 1000); //Creates the object and generates the value to be stored in "BankUser.Balance"
                }
            }
        }
        public static string Login(BankUser[] users)
        {
            for (int i = 0; i < 3; i++) //The system gives you 3 chances to log in before the program terminates
            {
                Console.WriteLine("Input Username");
                string userInputUsername = Console.ReadLine();

                Console.WriteLine("Input Password");
                string userInputPassword = Console.ReadLine();

                foreach (var item in users) //To compare the values stored in the array, I use this foreach loop to do that
                {
                    if (userInputUsername == item.UserName) //Checks the Username
                    {
                        if (userInputPassword == item.Password) //Checks the Password
                        {
                            return item.UserName; //Returns the Username
                        }
                    }
                }
                Console.WriteLine("Incorrect Username or Password, Please try again: ");
            }
            return "unknown";
        }
        public static void MainMenu(BankUser[] users, BankAccount[][] accounts)
        {
            do
            {
                //Log in
                //----------------------------------------|
                string userLoggedIn = Login(users); //Login() compares the user input to the information that is stored within the users[] array. If it matches the user logs in
                Console.Clear();
                bool loggedIn = true;

                if (userLoggedIn == "unknown") //Checks whether or not the user is logged in
                {
                    return;
                }

                //Main Menu
                //----------------------------------------|
                do
                {
                    Console.WriteLine("Du får nu fyra val \n1 - Se dina konton och saldo \n2 - Överföring mellan konton \n3 - Ta ut pengar \n4 - Logga ut");

                    int userMenuChoice = 5;

                    while (!int.TryParse(Console.ReadLine(), out userMenuChoice)) //Tries to parse the user input, and if it fails it tries again until it gets a valid answer, in this case an "int"
                    {
                        Console.WriteLine("Please input a Number: ");
                    }

                    switch (userMenuChoice)
                    {
                        //Balance
                        //----------------------------------------|
                        case 1:
                            for (int i = 0; i < users.Length; i++) //Determens what data to send the methods
                            {
                                if (users[i].UserName == userLoggedIn)
                                {
                                    AccountsBalance(users[i], accounts[i], 0);
                                }
                            }
                            break;

                        //Transaction
                        //----------------------------------------|
                        case 2:
                            for (int i = 0; i < users.Length; i++)
                            {
                                if (users[i].UserName == userLoggedIn)
                                {
                                    MakeATransaction(users[i], accounts[i]);
                                }
                            }
                            break;

                        //Withdraw
                        //----------------------------------------|
                        case 3:
                            for (int i = 0; i < users.Length; i++)
                            {
                                if (users[i].UserName == userLoggedIn)
                                {
                                    WithdrawBalance(users[i], accounts[i]);
                                }
                            }
                            break;

                        case 4: loggedIn = false; break;

                        default: Console.WriteLine("You have to input a number between 1 and 4"); break;
                    }

                    PressContinue(loggedIn); //Forces the user to press enter if they want to get back to the Mainmenu

                } while (loggedIn);

            } while (true);
        }
        public static void PressContinue(bool loggedIn)
        {
            if (loggedIn == true)
            {
                Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                ConsoleKey enterPressed = Console.ReadKey(true).Key;
                while (!Console.KeyAvailable && enterPressed != ConsoleKey.Enter)
                {
                    enterPressed = Console.ReadKey(true).Key;
                }
            }
            Console.Clear();
        }
        public static void AccountsBalance(BankUser accountOwner, BankAccount[] userAccounts, int index)
        {
            string[] accountNames = { "Lönekonto", "Sparkonto", "Sparkonto", "Sparkonto", "Sparkonto" }; //An array containing the names an account can have

            if (index == 0) //Shows the user its accounts and the balance in each account
            {
                for (int i = 0; i < accountOwner.AccountsPerUser; i++)
                {
                    Console.WriteLine($"{accountOwner.UserName}'s Konton: \nKonto:\t{accountNames[i]} - Balance:\t{userAccounts[i].Balance}");
                }
            }
            else if (index > 1) //The same as the one above but adds an index at the end
            {
                for (int i = 0; i < accountOwner.AccountsPerUser; i++)
                {
                    Console.WriteLine($"{accountOwner.UserName}'s Konton: \nKonto:\t{accountNames[i]} - Balance:\t{userAccounts[i].Balance}\t--\t{i + 1}");
                }
            }
            else
            {
                Console.WriteLine("Index out of range!");
            }
        }
        public static void MakeATransaction(BankUser accountOwner, BankAccount[] userAccounts)
        {
            bool failedTransfer;
            bool withdrawelOkay = false;
            decimal amountMoney;
            int transferTo;
            int transferFrom;
            
            //Transaction
            //----------------------------------------|
            AccountsBalance(accountOwner, userAccounts, userAccounts.Length); //Calls on the method AccountsBalance() with the index to show the user their options

            do
            {
                Console.WriteLine("How much money do you want to transfer?: ");
                while (!decimal.TryParse(Console.ReadLine(), out amountMoney)) //Tries to parse the user input, and if it fails it tries again until it gets a valid answer, in this case a "decimal"
                {
                    Console.WriteLine("Please input a Number: ");
                }
                if (amountMoney > 0)
                {
                    withdrawelOkay = true;
                }
                else
                {
                    Console.WriteLine("The Money you transfer can not be lower or equal to 0");
                }
            } while (!withdrawelOkay);

            do
            {
                Console.WriteLine("From which account do you want to transfer money from?: ");
                while (!int.TryParse(Console.ReadLine(), out transferFrom)) //From which account the money is being withdrawn from
                {
                    Console.WriteLine("Please input a Number: ");
                }
                if (transferFrom > 0 && transferFrom <= 3)
                {
                    withdrawelOkay = true;
                }
                else
                {
                    Console.WriteLine("Please input a number between 1 and 3");
                }
            } while (!withdrawelOkay); //From which account the money is being transferred from

            do
            {
                Console.WriteLine("From which account do you want to transfer money to?: ");
                while (!int.TryParse(Console.ReadLine(), out transferTo)) //From which account the money is being withdrawn from
                {
                    Console.WriteLine("Please input a Number: ");
                }
                if (transferTo > 0 && transferTo <= 3)
                {
                    withdrawelOkay = true;
                }
                else
                {
                    Console.WriteLine("Please input a number between 1 and 3");
                }
            } while (!withdrawelOkay); //To which account the money is being transferred to

            failedTransfer = FailedTransaction(userAccounts, transferFrom, amountMoney); //Checks whether or not he transfer is possible: If the transfer is possible, it withdraws the money from the giving account and returns a bool 

            if (failedTransfer) //Checks if the transfer failed
            {
                userAccounts[transferTo].MakeADeposit(amountMoney); //Makes the deposit

                AccountsBalance(accountOwner, userAccounts, 0); //Shows the user, their accounts again
            }
            else
            {
                Console.WriteLine("Not enough money in Account( {0} );\tMoney in Account( {0} ) - {1}", transferFrom, userAccounts[transferFrom].Balance);
            }
        }
        public static void WithdrawBalance(BankUser accountOwner, BankAccount[] userAccounts)
        {
            bool withdrawelCompleted;
            decimal amountMoney;
            int withdrawFrom;
            bool withdrawelOkay = false;

            //Withdrawel
            //----------------------------------------|
            AccountsBalance(accountOwner, userAccounts, userAccounts.Length);

            do
            {
                Console.WriteLine("How much money do you want to withdraw?: ");
                while (!decimal.TryParse(Console.ReadLine(), out amountMoney)) //Tries to parse the user input, and if it fails it tries again until it gets a valid answer, in this case a "decimal"
                {
                    Console.WriteLine("Please input a Number: ");
                }
                if (amountMoney > 0)
                {
                    withdrawelOkay = true;
                }
                else
                {
                    Console.WriteLine("The Money you withdraw can not be lower or equal to 0");
                }
            } while (!withdrawelOkay);
            
            withdrawelOkay = false;
            
            do
            {
                Console.WriteLine("From which account do you want to withdraw money from?: ");
                while (!int.TryParse(Console.ReadLine(), out withdrawFrom)) //From which account the money is being withdrawn from
                {
                    Console.WriteLine("Please input a Number: ");
                }
                if (withdrawFrom > 0 && withdrawFrom <= 3)
                {
                    withdrawelOkay = true;
                }
                else
                {
                    Console.WriteLine("Please input a number between 1 and 3");
                }
            } while (!withdrawelOkay);

            Console.WriteLine("Input Password to proceed with the withdrawel: ");
            string userInputPassword = Console.ReadLine(); //Asks the user for their credentials to validate the withdrawel

            if (userInputPassword == accountOwner.Password) //Checks if the password is valid
            {
                withdrawelCompleted = FailedTransaction(userAccounts, withdrawFrom, amountMoney); //Tries to withdraw the money and returns a bool for if it worked

                if (withdrawelCompleted)
                {
                    AccountsBalance(accountOwner, userAccounts, 0); //If it worked, we write the balance sheet to the user
                }
                else
                {
                    Console.WriteLine("Not enough money in Account( {0} );\tMoney in Account( {0} ) - {1}", withdrawFrom, userAccounts[withdrawFrom].Balance);
                }
            }
            else
            {
                Console.WriteLine("Incorrect Password: Withdrawel was cancelled");
            }
        }
        public static int InputNumberCheck(BankAccount[] userAccounts)
        {
            int transfer;
            do //Tries to get numerical input from the user
            {
                while (!int.TryParse(Console.ReadLine(), out transfer)) //Tries to parse the user input, and if it fails it tries again until it gets a valid answer, in this case an "decimal"
                {
                    Console.WriteLine("Please input a Number: ");
                }

                if (transfer < 1 || transfer > userAccounts.Length) //Checks if the user input is within the range given
                {
                    Console.WriteLine("You have to input a number between 1 and {0}", userAccounts.Length);
                }
                else
                {
                    return transfer;
                }
            } while (false);

            return transfer;
        }
        public static bool FailedTransaction(BankAccount[] userAccounts, int transferFrom, decimal amountMoney)
        {
            bool transferCompleted;
            bool userInputTryAgain = false;
            char userInputYesOrNo;

            transferCompleted = userAccounts[transferFrom].MakeAWithdrawel(amountMoney); //Tries to withdraw money from the specified account

            do //If it fails I ask the user if they want to try again
            {
                if (!transferCompleted)
                {
                    Console.WriteLine("Do you want to try again? Yes - y, No - n: ");

                    while (!char.TryParse(Console.ReadLine(), out userInputYesOrNo)) //Tries to parse the user input, and if it fails it tries again until it gets a valid answer, in this case an "char"
                    {
                        Console.WriteLine("Please input 'y' for Yes or 'n' for No: ");
                    }

                    switch (Char.ToLower(userInputYesOrNo)) //Checks the user input
                    {
                        case 'y': userInputTryAgain = true; break;

                        case 'n': userInputTryAgain = false; break;

                        default: Console.WriteLine("Please input 'y' for Yes or 'n' for No: "); userInputTryAgain = true; break;
                    }
                }
            } while (userInputTryAgain);
        return transferCompleted;
        } 
    }
}