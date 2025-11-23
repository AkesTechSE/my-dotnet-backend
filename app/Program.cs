using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<User> users = new List<User>();
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.WriteLine("\n=== User Registration ===");

            Console.Write("Enter your name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter your age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Invalid age. Please enter a number.");
                continue;
            }

            if (age < 18)
            {
                Console.WriteLine("Registration failed! You must be 18 or older.");
                continue;
            }

            Console.Write("Enter your email: ");
            string email = Console.ReadLine()!;

            // Create and store user
            User newUser = new User(name, age, email);
            users.Add(newUser);

            Console.WriteLine("\n=== Registration Successful! ===");
            Console.WriteLine($"Name : {newUser.Name}");
            Console.WriteLine($"Age  : {newUser.Age}");
            Console.WriteLine($"Email: {newUser.Email}");

            // Ask to continue
            Console.Write("\nRegister another user? (y/n): ");
            string? choice = Console.ReadLine();
            if (choice == null || choice.ToLower() != "y")
            {
                keepRunning = false;
            }
        }

        // Print all registered users
        Console.WriteLine("\n=== All Registered Users ===");
        foreach (var user in users)
        {
            Console.WriteLine($"Name: {user.Name}, Age: {user.Age}, Email: {user.Email}");
        }
    }
}

