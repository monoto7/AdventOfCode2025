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
                Console.WriteLine("Select Day, available are: 1, otherwise close");
                switch (Console.ReadLine())
                {
                    case "1":
                        Day1.SelectInput();
                        break;
                    default:
                        continueRunning = false;
                        break;
                }
            }
            
        }
    }
}