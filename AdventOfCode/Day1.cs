using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal static class Day1
    {
        public static int position = 50;
        public static int countOf0 = 0;
        public static int countOf0IncludingMidRotate = 0;

        public static void SelectInput()
        {
            position = 50;
            countOf0 = 0;
            countOf0IncludingMidRotate = 0;
            Console.WriteLine("Select input, 1:Example, 2:Main, otherwise return");
            String[] input = { };
            switch (Console.ReadLine()) {
                case "1":
                    input = File.ReadAllLines("Day1Inputs/Day1Example.txt");
                    break;
                case "2":
                    input = File.ReadAllLines("Day1Inputs/Day1Input.txt");
                    break;
                default:
                    return;
            }
            Calc0PosCount(input);
            Console.WriteLine("Position: " + position);
            Console.WriteLine("Count of Landed On 0: " + countOf0);
            Console.WriteLine("Count of ever 0: " + (countOf0IncludingMidRotate));
        }
      

        

        public static void Calc0PosCount(string[] input)
        {
            foreach (string line in input)
            {
                string lineTemp = line;
                if (lineTemp.StartsWith("L"))
                {
                    lineTemp = "-" + lineTemp.Substring(1);
                }
                else if(lineTemp.StartsWith("R")) 
                {
                    lineTemp =  lineTemp.Substring(1);
                }
                ChangePosLazyPart2(int.Parse(lineTemp));
            }
        }

        public static void ChangePosLazyPart2(int move)
        {
            while(move != 0)
            {
                if (move > 0)
                {
                    position++;
                    move--;
                }
                else
                {
                    position--;
                    move++;
                }
                if(position == 100 || position == - 100)
                {
                    position = 0;
                }
                if (position == 0)
                {
                    countOf0IncludingMidRotate++;
                }

            }
            if (position == 0)
            {
                countOf0++;
            }
        }
        public static void ChangePos(int move)
        {
            position += move;
            while (position < 0)
            {
                position = position + 100;
            }
            while(position > 99)
            {
                position = position - 100;
            }
            if(position == 0)
            {
                countOf0++;
            }
        }
        
    }
}
