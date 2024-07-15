using System;
using System.Collections.Generic;

namespace ATMMachine
{
    internal class Program
    {
        static Dictionary<string, (string password, double balance)> users = new Dictionary<string, (string, double)>();

        static void Main(string[] args)
        {
            int menuItem;

            do
            {
                Console.WriteLine("Please select operation: \r\n1. Log-in\r\n2. Sign-in\r\n3. Leave");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out menuItem))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (menuItem)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        CreateAccount();
                        break;
                    case 3:
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
            while (menuItem != 3);
        }

        static void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (users.ContainsKey(username) && users[username].password == password)
            {
                Console.WriteLine("Login successful!");
                UserMenu(username);
            }
            else
            {
                Console.WriteLine("Invalid credentials. Try again.");
            }
        }

        static void CreateAccount()
        {
            string username;
            while (true)
            {
                Console.Write("Enter username: ");
                username = Console.ReadLine();
                if (users.ContainsKey(username))
                {
                    Console.WriteLine("Username already exists. Try another one.");
                }
                else
                {
                    break;
                }
            }

            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            users.Add(username, (password, 0));
            Console.WriteLine("Account created successfully!");
        }

        static void UserMenu(string username)
        {
            while (true)
            {
                Console.WriteLine("\n1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Log Out");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CheckBalance(username);
                        break;
                    case "2":
                        Deposit(username);
                        break;
                    case "3":
                        Withdraw(username);
                        break;
                    case "4":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        static void CheckBalance(string username)
        {
            Console.WriteLine($"Your balance is: ${users[username].balance:F2}");
        }

        static void Deposit(string username)
        {
            Console.Write("Enter amount to deposit: ");
            if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
            {
                users[username] = (users[username].password, users[username].balance + amount);
                Console.WriteLine($"${amount:F2} deposited successfully.");
                CheckBalance(username);
            }
            else
            {
                Console.WriteLine("Invalid amount. Try again.");
            }
        }

        static void Withdraw(string username)
        {
            Console.Write("Enter amount to withdraw: ");
            if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
            {
                if (amount > users[username].balance)
                {
                    Console.WriteLine("Insufficient balance.");
                }
                else
                {
                    users[username] = (users[username].password, users[username].balance - amount);
                    Console.WriteLine($"${amount:F2} withdrawn successfully.");
                    CheckBalance(username);
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Try again.");
            }
        }
    }
}






