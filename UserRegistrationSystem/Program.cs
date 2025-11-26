using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

class Program
{
    // File path to store users
    static string filePath = "users.txt";

    static void Main(string[] args)
    {
        bool isContinue = true;

        while (isContinue)
        {
            Console.WriteLine("\n==== LOGIN SYSTEM ====");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Terminate");
            Console.WriteLine("====================");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine()?.Trim();

            if (choice == "1")
                Register();
            else if (choice == "2")
                Login();
            else if (choice == "3")
            {
                Console.WriteLine("Exiting system...");
                isContinue = false;
            }
            else
                Console.WriteLine("Invalid choice, try again!");
        }
    }

    // ============================
    // Registration Method
    // ============================
    static void Register()
    {
        Console.WriteLine("\n--- Registration ---");

        Console.Write("Enter username: ");
        string username = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(username))
        {
            Console.WriteLine("Username cannot be empty.");
            return;
        }

        // Check if username already exists
        if (UserExists(username))
        {
            Console.WriteLine("Username already exists! Try a different one.");
            return;
        }

        Console.Write("Enter password: ");
        string password = Console.ReadLine() ?? "";

        if (!IsValidPassword(password))
        {
            Console.WriteLine("Password is invalid!");
            ShowPasswordRules();
            return;
        }

        // Choose a delimiter that passwords unlikely contain (here we use '|')
        // Recommended: hash the password instead of storing plain text. See below.
        string storedPassword = password; // or HashPassword(password);

        File.AppendAllText(filePath, $"{username}|{storedPassword}{Environment.NewLine}");
        Console.WriteLine("Registration successful!");
    }

    // ============================
    // Login Method
    // ============================
    static void Login()
    {
        Console.WriteLine("\n--- Login ---");

        Console.Write("Enter username: ");
        string username = Console.ReadLine()?.Trim();

        Console.Write("Enter password: ");
        string password = Console.ReadLine() ?? "";

        // If you store hashed passwords, hash the input before comparing:
        // string checkPassword = HashPassword(password);
        string checkPassword = password;

        // Check credentials
        if (AuthenticateUser(username, checkPassword))
        {
            Console.WriteLine("Login successful! Welcome " + username);
        }
        else
        {
            Console.WriteLine("Invalid username or password!");
        }
    }

    // ============================
    // Password Validation (Regex)
    // ============================
    static bool IsValidPassword(string password)
    {
        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&*!?]).{8,}$";
        return Regex.IsMatch(password, pattern);
    }

    static void ShowPasswordRules()
    {
        Console.WriteLine("\nPassword Requirements:");
        Console.WriteLine("- At least 8 characters");
        Console.WriteLine("- At least one uppercase letter");
        Console.WriteLine("- At least one lowercase letter");
        Console.WriteLine("- At least one digit");
        Console.WriteLine("- At least one special character (@#$%^&*!?)");
    }

    // ============================
    // Check if user exists
    // ============================
    static bool UserExists(string username)
    {
        if (!File.Exists(filePath)) return false;

        foreach (var line in File.ReadAllLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split('|'); // delimiter used in storage
            if (parts.Length < 2) continue;
            string storedUser = parts[0].Trim();
            if (string.Equals(storedUser, username, StringComparison.Ordinal))
                return true;
        }
        return false;
    }

    // ============================
    // Authenticate user
    // ============================
    static bool AuthenticateUser(string username, string password)
    {
        if (!File.Exists(filePath)) return false;

        foreach (var line in File.ReadAllLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split('|'); // use same delimiter
            if (parts.Length < 2) continue;

            string storedUser = parts[0].Trim();
            string storedPass = parts[1].Trim();

            // If using hashed passwords, compare hashed values
            // e.g. if (storedPass == HashPassword(password)) return true;

            if (string.Equals(storedUser, username, StringComparison.Ordinal) &&
                string.Equals(storedPass, password, StringComparison.Ordinal))
            {
                return true;
            }
        }

        return false;
    }

    // ============================
    // Optional: Simple SHA256 Hashing
    // (Enable for production — don't store plain text)
    // ============================
    static string HashPassword(string password)
    {
        using (var sha = SHA256.Create())
        {
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
