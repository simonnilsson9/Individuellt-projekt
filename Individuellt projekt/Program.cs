using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace Individuellt_projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                int loginAttempts = 0;
                string[] users = new string[] { "admin", "SuperAdmin", "Simon", "Anton", "Emm" };
                int[] pinCode = new int[] { 1111, 2222, 3333, 4444, 5555 };
                string[] accounts = new string[] {"admin Sparkonto", "admin Lönekonto", "SuperAdmin Sparkonto", "SuperAdmin Lönekonto",
                "Simon Sparkonto", "Simon Lönekonto", "Anton Sparkonto", "Anton Lönekonto", "Emm Sparkonto", "Emm Lönekonto"};
                double[] money = new double[] { 100.5, 1000.5, 90.5, 945.70, 145.50, 1000.85, 153.60, 843.50, 120.5, 1250.5 };

                int userIndex = -1; 
                bool loggedIn = false;
                Console.WriteLine("Välkommen till banken!\n");
                while (true)
                {
                    while (!loggedIn)
                    {
                        Console.Write("Var god skriv in ditt användarnamn: ");
                        string username = Console.ReadLine();

                        Console.Write("Var god skriv in PIN-koden: ");
                        int pin = Convert.ToInt32(Console.ReadLine());

                        userIndex = Array.IndexOf(users, username);

                        if (userIndex >= 0 && pinCode[userIndex] == pin)
                        {
                            loggedIn = true;
                            Console.WriteLine("Inloggning lyckades! Du skickas vidare till menyn.");
                        }
                        else
                        {
                            Console.WriteLine("Felaktigt användarnamn eller PIN-kod.\n");
                            loginAttempts++;
                        }
                        if (loginAttempts >= 3)
                        {
                            Console.WriteLine("\nFör många felaktiga försök. Programmet avslutas.");
                            return;
                        }

                    }
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
                            CheckMoney(accounts, money, users[userIndex]);
                            Console.WriteLine("\nTryck ENTER för att komma till menyn.");
                            Console.ReadKey();
                            break;
                        case 2:
                            Console.Clear();
                            TransferMoney(accounts, money, users[userIndex]);
                            Console.WriteLine("\nTryck ENTER för att komma till menyn.");
                            Console.ReadKey();
                            break;
                        case 3:
                            Console.Clear();
                            WithdrawMoney(accounts, money, users[userIndex], pinCode);
                            Console.WriteLine("\nTryck ENTER för att komma till menyn.");
                            Console.ReadKey();
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine($"Du har blivit utloggad...\n");
                            loggedIn = false;
                            loginAttempts = 0;
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

        public static void CheckMoney(string[] accounts, double[] money, string loggedInUser)
        {            
            Console.WriteLine($"Saldo för konton för användare {loggedInUser}:");
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].StartsWith(loggedInUser))
                {
                    Console.WriteLine($"{accounts[i]} : {money[i]} kr");                                        
                }
            }
        }
        public static void TransferMoney(string[] accounts, double[] money, string loggedInUser)
        {
            CheckMoney(accounts, money, loggedInUser);

            Console.Write("Ange vilket konto som du vill föra över pengar från: ");
            string fromAccount = Console.ReadLine();
            int fromAccountIndex = -1;
            int toAccountIndex = -1;
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].StartsWith(loggedInUser) && accounts[i].Contains(fromAccount))
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
                if (accounts[i].StartsWith(loggedInUser) && accounts[i].Contains(toAccount))
                {
                    toAccountIndex = i;
                }
            }
            if (toAccountIndex < 0 || toAccountIndex >= accounts.Length || toAccountIndex == fromAccountIndex) 
            { 
                Console.WriteLine("Felaktigt kontoval att överföra till.\n");
            }

            if (money[fromAccountIndex] >= transferAmount)
            {
                money[fromAccountIndex] -= transferAmount;
                money[toAccountIndex] += transferAmount;
                Console.WriteLine($"Överföringen av {transferAmount} kr lyckades.\n");                
                CheckMoney(accounts, money, loggedInUser);
            }
            else
            {
                Console.WriteLine("Du har inte tillräckligt med pengar på kontot.");
            }
            
        }
        public static void WithdrawMoney(string[] accounts, double[] money, string loggedInUser, int[] pinCode)
        {
            CheckMoney(accounts, money, loggedInUser);
            Console.Write("Ange vilket konto du vill ta ut pengar ifrån: ");
            string fromAccount = Console.ReadLine();

            int fromAccountIndex = -1;           
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].StartsWith(loggedInUser) && accounts[i].Contains(fromAccount))
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
            if (money[fromAccountIndex] >= transferAmount)
            {
                Console.Write("Var god skriv in PIN-koden igen: ");
                int pin = Convert.ToInt32(Console.ReadLine());
                int userIndex = Array.IndexOf(pinCode,pin);
                if (userIndex >= 0 && pinCode[userIndex] == pin)
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
