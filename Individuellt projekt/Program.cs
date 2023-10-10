using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;

namespace Individuellt_projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                //A set of different arrays that saves different kind of information about users. 
                int loginAttempts = 0;
                string[] users = new string[] { "admin", "SuperAdmin", "Simon", "Anton", "Emm" };
                int[] pinCode = new int[] { 1111, 2222, 3333, 4444, 5555 };
                string[] accounts = new string[] {"admin Sparkonto", "admin Lönekonto", "SuperAdmin Sparkonto", "SuperAdmin Lönekonto",
                "Simon Sparkonto", "Simon Lönekonto", "Anton Sparkonto", "Anton Lönekonto", "Emm Sparkonto", "Emm Lönekonto"};
                double[] money = new double[] { 100.5, 1000.5, 90.5, 945.70, 145.50, 1000.85, 153.60, 843.50, 120.5, 1250.5 };
               
                int userIndex = -1; 
                bool loggedIn = false;
                int loggedInPin = -1;

                Console.WriteLine("Välkommen till banken!\n");
                while (true) 
                {
                    //The log in bit in the code. Uses a while loop that let you have 3 login attempts. 
                    while (!loggedIn)
                    {
                        Console.Write("Var god skriv in ditt användarnamn: ");
                        string username = Console.ReadLine();

                        Console.Write("Var god skriv in PIN-koden: ");
                        int pin = Convert.ToInt32(Console.ReadLine());

                        userIndex = Array.IndexOf(users, username); //Searchs for a index that compares if the username macthes to users. If it does, it returns 0 or more, otherwise -1. 

                        if (userIndex >= 0 && pinCode[userIndex] == pin) //If previous returns 0 or more and pinCode matches what the user input was, the bool loggedIn becomes true and ends the while loop. 
                        {
                            loggedIn = true;
                            loggedInPin= pin;
                            Console.WriteLine("Inloggning lyckades! Du skickas vidare till menyn.");
                        }
                        else
                        {
                            Console.WriteLine("Felaktigt användarnamn eller PIN-kod.\n");
                            loginAttempts++;
                        }
                        if (loginAttempts >= 3)//If you fail 3 attemps to log in, the program will shut down. 
                        {
                            Console.WriteLine("\nFör många felaktiga försök. Programmet avslutas.");
                            return;
                        }

                    }
                    //The menu section in the code. The user chooses between 1-4. After the user input it goes to the switch meny based on the input. 
                    Console.WriteLine("\n--------------MENY--------------");
                    Console.WriteLine("[1] Visa konton och saldo");
                    Console.WriteLine("[2] Överföring mellan konton");
                    Console.WriteLine("[3] Ta ut pengar");
                    Console.WriteLine("[4] Logga ut");
                    Console.Write("Välj mellan menyval 1-4: ");
                    Int32.TryParse(Console.ReadLine(), out int menu);

                    switch (menu)
                    {
                        case 1:
                            Console.Clear();
                            CheckMoney(accounts, money, users[userIndex]);//Put in the values from accounts, money, and users[userIndex] into the method.
                            Console.WriteLine("\nTryck ENTER för att komma till menyn.");
                            Console.ReadKey();
                            break;
                        case 2:
                            Console.Clear();
                            TransferMoney(accounts, money, users[userIndex]);//Put in the values from accounts, money, and users[userIndex] into the method.
                            Console.WriteLine("\nTryck ENTER för att komma till menyn.");
                            Console.ReadKey();
                            break;
                        case 3:
                            Console.Clear();
                            WithdrawMoney(accounts, money, users[userIndex], loggedInPin);//Put in the values from accounts, money, users[userIndex] and loggedInPin into the method.
                            Console.WriteLine("\nTryck ENTER för att komma till menyn.");
                            Console.ReadKey();
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine($"Du har blivit utloggad...\n");
                            loggedIn = false; //Set the the bool to false so that the inner while loop resets. Also set the loginAttempts to 0, so new users can log in again. 
                            loginAttempts = 0;
                            loggedInPin = -1;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Felaktig inmatning, du måste välja mellan menyval 1-4....");
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public static void CheckMoney(string[] accounts, double[] money, string loggedInUser) //Fetch the value from accounts,money and which user that is logged in.  
        {                                
            Console.WriteLine($"Saldo för konton för användare {loggedInUser}:");
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].StartsWith(loggedInUser)) //Checks if index of the account matches the beggining string for the user thats logged in. 
                {
                    Console.WriteLine($"{accounts[i]} : {money[i]} kr"); //Prints out accounts and money with the same index.                                         
                }
            }
        }
        public static void TransferMoney(string[] accounts, double[] money, string loggedInUser) //Fetch the value from accounts,money and which user that is logged in.
        {
            /* This section of the code is the transfer. It will firstly show the availabe accounts that is associated with the logged in user. After that you choose which account to transfer from, then the amount and
            then which account to transfer to. All the info saves in different variables. If anything doesn't go the way its supposed to, the program will let you know.*/

            CheckMoney(accounts, money, loggedInUser);

            Console.Write("Ange vilket konto som du vill föra över pengar från: ");
            string fromAccount = Console.ReadLine();
            int fromAccountIndex = -1;
            int toAccountIndex = -1;
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].StartsWith(loggedInUser) && accounts[i].Contains(fromAccount)) //Checks if the accounts start with the same string as loggedInUser and if accounts matches the user input. 
                {
                    fromAccountIndex = i;
                }
            }

            if (fromAccountIndex < 0 || fromAccountIndex >= accounts.Length)
            {
                Console.WriteLine("Ogiltigt kontoval.");
                return;
            }

            Console.Write("Ange vilket belopp du vill överföra: ");
            double.TryParse(Console.ReadLine(), out double transferAmount);            
            if (transferAmount <= 0)
            {
                Console.WriteLine("Felaktigt belopp att överföra.\n");
                return; 
            }

            Console.Write("Ange vilket konto du vill föra över pengar till: ");
            string toAccount = Console.ReadLine();
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].StartsWith(loggedInUser) && accounts[i].Contains(toAccount)) // Checks if the accounts start with the same string as loggedInUser and if accounts matches the user input.
                {
                    toAccountIndex = i;
                }
            }
            if (toAccountIndex < 0 || toAccountIndex >= accounts.Length || toAccountIndex == fromAccountIndex) 
            { 
                Console.WriteLine("Felaktigt kontoval att överföra till.\n");
            }

            if (money[fromAccountIndex] >= transferAmount)//If the amount of money you want to transfer is less or equal to the amount thats in the account, the program will continue.
            {
                money[fromAccountIndex] -= transferAmount;
                money[toAccountIndex] += transferAmount;
                Console.WriteLine($"Överföringen av {transferAmount} kr lyckades.\n");                
                CheckMoney(accounts, money, loggedInUser);
            }
            else //Otherwise, it will tell you that you don't have enough money. 
            {
                Console.WriteLine("Du har inte tillräckligt med pengar på kontot.");
            }
            
        }
        public static void WithdrawMoney(string[] accounts, double[] money, string loggedInUser, int loggedInPin) //Fetch the value from accounts,money, pinCode and which user that is logged in.
        {
            /*This section of the code is the withdraw money. First it prints out the accounts that is associated with the logged in user. It then lets the user enter which account it wants to transfer money
            from and saves it as a string. It then uses a for-loop to check through all the accounts and see if the string matches any accounts. If so it will let you choose the amount to withdraw and then
            you enter the pin again. If you do anything wrong, the program will tell you.  */

            CheckMoney(accounts, money, loggedInUser);
            Console.Write("Ange vilket konto du vill ta ut pengar ifrån: ");
            string fromAccount = Console.ReadLine();

            int fromAccountIndex = -1;               
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].StartsWith(loggedInUser) && accounts[i].Contains(fromAccount)) // Checks if the accounts start with the same string as loggedInUser and if accounts matches the user input.
                {
                    fromAccountIndex = i;
                }
            }

            if (fromAccountIndex < 0 || fromAccountIndex >= accounts.Length)
            {
                Console.WriteLine("Ogiltigt kontoval.");
                return;
            }

            Console.Write("Ange vilket belopp du vill ta ut: ");
            double.TryParse(Console.ReadLine(), out double transferAmount);
            if (transferAmount <= 0) 
            {
                Console.WriteLine("Felaktigt belopp att ta ut.\n");
                return;
            }
            if (money[fromAccountIndex] >= transferAmount) //If the amount of money you want to transfer is less or equal to the amount thats in the account, the program will continue.
            {                
                Console.Write("Var god skriv in PIN-koden igen: ");
                int pin = Convert.ToInt32(Console.ReadLine());
                
                if (loggedInPin == pin) 
                {
                    money[fromAccountIndex] -= transferAmount;
                    Console.WriteLine($"Du tog ut {transferAmount} kr.\n");
                    CheckMoney(accounts, money, loggedInUser);
                }
                else
                {
                    Console.WriteLine("\nFel pinkod.");
                }
            }
            else
            {
                Console.WriteLine("Du har inte tillräckligt med pengar på kontot.");
            }

        }       
    }
}
