using System;

namespace CampusVarberg___InternetBank4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dynamic[] user = new dynamic[5];
            user[0] = new BankUser("Max", "1234", 3);
            user[1] = new BankUser("Anas", "1234", 1);
            user[2] = new BankUser("Tobias", "1234", 2);
            user[3] = new BankUser("Reidar", "1234", 5);
            user[4] = new BankUser("Kristian", "1234", 4);

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
                    int userMainMenuChoice = int.Parse(Console.ReadLine());

                    switch (userMainMenuChoice)
                    {
                        case 1:
                            foreach (var item in user)
                            {
                                if (item.UserName == userLoggedIn)
                                {
                                    for (int i = 1; i < item.accountsPerUser; i++)
                                    {
                                        AccountsBalance(item);
                                    }
                                }
                            }
                            break;

                        case 2: break;

                        case 3: break;

                        case 4: loggedIn = false; break;

                        default: Console.WriteLine("User choice: Invalid"); break;

                    }

                    if (loggedIn == true)
                    {
                        Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                        ConsoleKey enterPressed = Console.ReadKey(true).Key;
                        while (!Console.KeyAvailable && enterPressed != ConsoleKey.Enter)
                        {
                            enterPressed = Console.ReadKey(true).Key;
                        }
                    }

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

        public static void AccountsBalance(dynamic[] accountOwner)
        {
            
        }
    }
}
