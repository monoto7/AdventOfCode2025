using System;

namespace AdventOfCode2025
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            bool continueRunning = true;
            while (continueRunning)
            {
                Console.WriteLine("Select Day, available are: 1, 2, 3, 4, 5, 6, 7, 8, 9; otherwise close");
                switch (Console.ReadLine())
                {
                    case "1":
                        Day1.SelectInput();
                        break;
                    case "2":
                        Day2.SelectInput();
                        break;
                    case "3":
                        Day3.SelectInput();
                        break;
                    case "4":
                        Day4.SelectInput();
                        break;
                    case "5":
                        Day5.SelectInput();
                        break;
                    case "6":
                        Day6.SelectInput();
                        break;
                    case "7":
                        Day7.SelectInput();
                        break;
                    case "8":
                        Day8.SelectInput();
                        break;
                    case "9":
                        Day9.SelectInput();
                        break;
                    default:
                        continueRunning = false;
                        break;
                }
            }
            
        }
    }
}