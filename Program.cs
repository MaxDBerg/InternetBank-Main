using InternetBank;
using System;
using System.IO;

namespace CampusVarberg___InternetBank4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dynamic[] users = new dynamic[5];
            dynamic[][] accounts = new dynamic[users.Length][];
            users[0] = new BankUser("Max", "1234", 3);
            users[1] = new BankUser("Anas", "1234", 1);
            users[2] = new BankUser("Tobias", "1234", 2);
            users[3] = new BankUser("Reidar", "1234", 5);
            users[4] = new BankUser("Kristian", "1234", 4);
            for (int i = 0; i < users.Length; i++)
            {
                accounts[i] = new dynamic[users[i].AccountsPerUser];
                for (int j = 0; j < users[i].AccountsPerUser; j++)
                {
                    accounts[i][j] = new BankAccount((j + 1) * users[i].AccountsPerUser * 1000);
                }
            }

            MainMenu(users, accounts);

        }

        public static string Login(dynamic[] users)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Input Username");
                string userInputUsername = Console.ReadLine();

                Console.WriteLine("Input Password");
                string userInputPassword = Console.ReadLine();

                foreach (var item in users)
                {
                    if (userInputUsername == item.UserName)
                    {
                        if (userInputPassword == item.Password)
                        {
                            return item.UserName;
                        }
                    }
                }
                Console.WriteLine("Incorrect Username or Password, Please try again: ");
            }
            return "unknown";
        }
        public static void MainMenu(dynamic[] users, dynamic[][] accounts)
        {
            do
            {
                string userLoggedIn = Login(users);
                bool loggedIn = true;

                if (userLoggedIn == "unknown")
                {
                    return;
                }

                do
                {
                    Console.WriteLine("Du får nu fyra val \n1 - Se dina konton och saldo \n2 - Överföring mellan konton \n3 - Ta ut pengar \n4 - Logga ut");

                    int userMenuChoice = 5;

                    while (!int.TryParse(Console.ReadLine(), out userMenuChoice))
                    {
                        Console.WriteLine("Please input a Number: ");
                    }

                    switch (userMenuChoice)
                    {
                        case 1:
                            for (int i = 0; i < users.Length; i++)
                            {
                                if (users[i].UserName == userLoggedIn)
                                {
                                    AccountsBalance(users[i], accounts[i], 0);
                                }
                            }
                            break;

                        case 2:
                            for (int i = 0; i < users.Length; i++)
                            {
                                if (users[i].UserName == userLoggedIn)
                                {
                                    MakeATransaction(users[i], accounts[i]);
                                }
                            }
                            break;

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

                    PressContinue(loggedIn);

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
        }
        public static void AccountsBalance(dynamic accountOwner, dynamic[] userAccounts, int index)
        {
            string[] accountNames = { "Lönekonto", "Sparkonto", "Sparkonto", "Sparkonto", "Sparkonto" };
            int accNum = 0;

            if (index == 0)
            {
                for (int i = 0; i < accountOwner.AccountsPerUser; i++)
                {
                    Console.WriteLine($"{accountOwner.UserName}'s Konton: \nKonto:\t{accountNames[accNum]} - Balance:\t{userAccounts[i].Balance}");
                    accNum++;
                }
            }
            else if (index > 1)
            {
                for (int i = 0; i < accountOwner.AccountsPerUser; i++)
                {
                    Console.WriteLine($"{accountOwner.UserName}'s Konton: \nKonto:\t{accountNames[accNum]} - Balance:\t{userAccounts[i].Balance}\t--\t{i + 1}");
                    accNum++;
                }
            }
            else
            {
                Console.WriteLine("Index out of range!");
            }
        }
        public static void MakeATransaction(dynamic accountOwner, dynamic[] userAccounts)
        {
            bool userInputTryAgain;
            decimal amountMoney;
            AccountsBalance(accountOwner, userAccounts, userAccounts.Length);

            do
            {
                Console.WriteLine("How much money do you want to transfer?: ");
                while (!decimal.TryParse(Console.ReadLine(), out amountMoney))
                {
                    Console.WriteLine("Please input a Number: ");
                }

                Console.WriteLine("From which account do you want to transfer money from?: ");
                int transferFrom = InputNumberCheck(userAccounts) - 1;

                Console.WriteLine("From which account do you want to transfer money to?: ");
                int transferTo = InputNumberCheck(userAccounts) - 1;

                userInputTryAgain = FailedTransaction(userAccounts, transferFrom, amountMoney);

                if (userInputTryAgain == true)
                {
                    userAccounts[transferTo].MakeADeposit(amountMoney);

                    AccountsBalance(accountOwner, userAccounts, 0);
                }
                else
                {
                    Console.WriteLine("Not enough money in Account( {0} );\tMoney in Account( {0} ) - {1}", transferFrom, userAccounts[transferFrom].Balance);
                }

            } while (userInputTryAgain);

        }
        public static void WithdrawBalance(dynamic accountOwner, dynamic[] userAccounts)
        {
            bool withdrawelCompleted;
            decimal amountMoney;

            AccountsBalance(accountOwner, userAccounts, userAccounts.Length);

            Console.WriteLine("How much money do you want to withdraw?: ");
            while (!decimal.TryParse(Console.ReadLine(), out amountMoney))
            {
                Console.WriteLine("Please input a Number: ");
            }

            Console.WriteLine("From which account do you want to withdraw money from?: ");
            int transferFrom = InputNumberCheck(userAccounts) - 1;

            Console.WriteLine("Input Password to proceed with the withdrawel: ");
            string userInputPassword = Console.ReadLine();

            if (userInputPassword == accountOwner.Password)
            {
                withdrawelCompleted = FailedTransaction(userAccounts, transferFrom, amountMoney);

                do
                {
                    if (withdrawelCompleted)
                    {
                        AccountsBalance(accountOwner, userAccounts, 0);
                    }
                    else
                    {
                        Console.WriteLine("Not enough money in Account( {0} );\tMoney in Account( {0} ) - {1}", transferFrom, userAccounts[transferFrom].Balance);
                    }
                } while (!withdrawelCompleted);
            }
            else
            {
                Console.WriteLine("Incorrect Password: Withdrawel was cancelled");
            }
        }
        public static int InputNumberCheck(dynamic[] userAccounts)
        {
            int transfer;
            bool transferGranted = false;
            do
            {
                while (!int.TryParse(Console.ReadLine(), out transfer))
                {
                    Console.WriteLine("Please input a Number: ");
                }

                if (transfer < 1 || transfer > userAccounts.Length)
                {
                    Console.WriteLine("You have to input a number between 1 and {0}", userAccounts.Length);
                }
                else
                {
                    transferGranted = true;
                    return transfer;
                }
            } while (transferGranted == false);

            return transfer;
        }
        public static bool FailedTransaction(dynamic[] userAccounts, int transferFrom, decimal amountMoney)
        {
            bool transferCompleted;
            bool userInputTryAgain = false;
            char userInputYesOrNo;

            transferCompleted = userAccounts[transferFrom].MakeAWithdrawel(amountMoney);

            do //Remeber to make a yes or no method because you need to use it twice
            {
                if (transferCompleted != true)
                {
                    Console.WriteLine("Do you want to try again? Yes - y, No - n: ");

                    while (!char.TryParse(Console.ReadLine(), out userInputYesOrNo))
                    {
                        Console.WriteLine("Please input 'y' for Yes or 'n' for No: ");
                    }

                    switch (userInputYesOrNo)
                    {
                        case 'y': userInputTryAgain = true; break;

                        case 'n': return false;

                        default: Console.WriteLine("Please input 'y' for Yes or 'n' for No: "); userInputTryAgain = true; break;
                    }

                }
            } while (userInputTryAgain);

            return true;
        }
    }
}