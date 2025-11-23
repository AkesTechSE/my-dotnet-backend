using System;
using System.Collections.Generic;

namespace StudentSystem
{
    // ------- Student Class -------
    public class Student
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Attendance { get; set; }

        // Method to check pass/fail
        public bool IsPassed()
        {
            return Score >= 50 && Attendance >= 75;
        }
    }

    // ------- Main Program -------
    class Program
    {
        static void Main()
        {
            List<Student> students = new List<Student>();
            int choice;

            while (true)
            {
                Console.WriteLine("\n=== Student Management System ===");
                Console.WriteLine("1. Register New Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Exit");
                Console.Write("Choose: ");
                choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    Student s = new Student();

                    Console.Write("\nEnter Name: ");
                    s.Name = Console.ReadLine();

                    Console.Write("Enter Score (0-100): ");
                    s.Score = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Attendance (0-100%): ");
                    s.Attendance = Convert.ToInt32(Console.ReadLine());

                    students.Add(s);

                    Console.WriteLine("\n--- Result ---");
                    if (s.IsPassed())
                    {
                        Console.WriteLine($"Status: PASSED ✔");
                    }
                    else
                    {
                        Console.WriteLine($"Status: FAILED ✖");
                        if (!(s.Score >= 50))
                            Console.WriteLine("Reason: Low Score (<50)");
                        if (!(s.Attendance >= 75))
                            Console.WriteLine("Reason: Low Attendance (<75%)");
                    }
                }
                else if (choice == 2)
                {
                    Console.WriteLine("\n=== All Registered Students ===");
                    foreach (var s in students)
                    {
                        Console.WriteLine($"Name: {s.Name}, Score: {s.Score}, Attendance: {s.Attendance}%, Passed: {s.IsPassed()}");
                    }
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Choice. Try again.");
                }
            }
        }
    }
}
