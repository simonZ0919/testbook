// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, Zach!");

using System;
using System.Collections.Generic;

namespace TestBook // Note: actual namespace depends on the project name.
{
    class Program
    {
        static void Main(string[] args)
        {
            // array initialize
            // equal: var x = new double[3]{1,2,3}
            var arr = new[] { 1.2, 2.5, 3.6 };
            // List with generic type
            var grades = new List<double>() { 1.2, 2.5, 3.6 };
            
            // class initialization
            var book = new DiskBook("Zach's book");
            // subscribe to event twice
            book.GradeAddEvent += OnGradeAdded;

            // print log
            Console.WriteLine(InMemoryBook.CATEGORY);
            Console.WriteLine($"The book name: {book.Name}");

            // add grade from console input
            EnterGrade(book);

            // book.AddGrade(99.9);
            // book.AddGrade(89.6);
            // book.AddGrade(82.5);
            var stat = book.GetStatistics();
            // format the result with 1 digit Number
            Console.WriteLine($"The lowest grade is {stat.Low}");
            Console.WriteLine($"The average grade is {stat.Average:N1}");
            Console.WriteLine($"The letter grade is {stat.Letter}");
        }

        private static void EnterGrade(IBook? book)
        {
            Console.WriteLine("Enter a grade or 'q' to exit");
            // checked nulllable reference
            string? input;
            while ((input = Console.ReadLine()) != "q")
            {
                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                // catch exception
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}