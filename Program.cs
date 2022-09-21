using InternetBank;
using System;
using System.IO;

namespace CampusVarberg___InternetBank4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dynamic[] user = new dynamic[5];
            dynamic[][] account = new dynamic[user.Length][];
            user[0] = new BankUser("Max", "1234", 3);
            user[1] = new BankUser("Anas", "1234", 1);
            user[2] = new BankUser("Tobias", "1234", 2);
            user[3] = new BankUser("Reidar", "1234", 5);
            user[4] = new BankUser("Kristian", "1234", 4);
            for (int i = 0; i < user.Length; i++)
            {
                account[i] = new dynamic[user[i].AccountsPerUser];
                for (int j = 0; j < user[i].AccountsPerUser; j++)
                {
                    account[i][j] = new BankAccount((j + 1) * user[i].AccountsPerUser * 10000);
                }
            }

            do
            {
                string userLoggedIn = Login(user);
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
                            for (int i = 0; i < user.Length; i++)
                            {
                                if (user[i].UserName == userLoggedIn)
                                {
                                    AccountsBalance(user[i], account[i]);
                                }
                            }
                            break;

                        case 2: break;

                        case 3: break;

                        case 4: loggedIn = false; break;

                        default: Console.WriteLine("You have to input a number between 1 and 4"); break;
                    }                    
                    
                    PressContinue(loggedIn);

                } while (loggedIn == true);

            } while (true);

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

        public static void AccountsBalance(dynamic accountOwner, dynamic[] userAccounts)
        {
            string[] accountNames = { "Lönekonto", "Sparkonto", "Sparkonto", "Sparkonto", "Sparkonto" };
            int accNum = 0;

            for (int i = 0; i < accountOwner.AccountsPerUser; i++)
            {
                Console.WriteLine($"{accountOwner.UserName}'s Konton: \nKonto:\t{accountNames[accNum]} - Balance:\t{userAccounts[i].Balance}");
                accNum++;
            }
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
    }
}

//dynamic[] account = new dynamic[accountOwner.AccountsPerUser];
//for (int i = 0; i < accountOwner.AccountsPerUser; i++)
//{
//    account[i] = new BankAccount((i + 1) * accountOwner.AccountsPerUser * 10000);
//}

// Nested Classes for accounts in BankUser