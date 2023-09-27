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
            int loginAttempts = 0;
            string[] users = new string[5] { "admin", "SuperAdmin", "Simon", "Anton", "Emm" };
            int[] pinCode = new int[5] { 1111, 2222, 3333, 4444, 5555 };
            
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

                    int userIndex = Array.IndexOf(users, username);

                    if (userIndex >= 0 && pinCode[userIndex] == pin)
                    {
                        loggedIn = true;
                        Console.WriteLine("Inloggning lyckades! Du skickas vidare till menyn.\n");
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
                Console.WriteLine("[1] Visa konton och saldo");
                Console.WriteLine("[2] Överföring mellan konton");
                Console.WriteLine("[3] Ta ut pengar");
                Console.WriteLine("[4] Logga ut");
                Console.WriteLine("Välj mellan menyval 1-4.");
                Int32.TryParse(Console.ReadLine(), out int menu);

                switch (menu)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        Console.Clear();
                        loggedIn = false;
                        loginAttempts = 0;
                        break;
                    default:
                        break;

                }
            }
            

        }
    }
}
