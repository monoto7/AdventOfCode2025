using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Day4
    {
        public static int xSize = 0;
        public static int ySize = 0;
        public static void SelectInput()
        {
            Console.WriteLine("Select input, 1:Example, 2:Main, otherwise return");
            String[] input = { };
            switch (Console.ReadLine())
            {
                case "1":
                    input = File.ReadAllLines("Day4Inputs/Day4Example.txt");
                    break;
                case "2":
                    input = File.ReadAllLines("Day4Inputs/Day4Input.txt");
                    break;
                default:
                    return;
            }
            xSize = input[0].Length;
            ySize = input.Length;
            char[,] charGrid = new char[xSize, ySize];
            int[,] intGrid = new int[xSize, ySize];
            int i1 = 0;
            
            foreach(string s in input)
            {
                int i2 = 0;
                foreach(char c in s.ToCharArray())
                {
                    charGrid[i1, i2] = c;
                    i2++;
                }
                i1++;
            }

            checkAdjacent(charGrid, intGrid);

            int total = 0;
            
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if(intGrid[i, j] < 4 && charGrid[i,j] == '@')
                    {
                        total++;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("total part 1: " + total);

            checkAdjacentRepeating(charGrid);
            total = 0;
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if (charGrid[i, j] == 'x')
                    {
                        total++;
                    }
                }
            }
            Console.WriteLine("total part 2: " + total);
        }

        public static void checkAdjacentRepeating(char[,] chargrid)
        {
            bool unresolved = true;
            while (unresolved)
            {
                int total = 0;
                int[,] intGrid = new int[xSize, ySize];
                checkAdjacent(chargrid, intGrid);

                for (int i = 0; i < xSize; i++)
                {
                    for (int j = 0; j < ySize; j++)
                    {
                        if (intGrid[i, j] < 4 && chargrid[i, j] == '@')
                        {
                            chargrid[i, j] = 'x';
                            total++;
                        }
                    }
                }

                if (total == 0)
                {
                    unresolved = false;
                }
            }
        }


        public static void checkAdjacent(char[,] chargrid, int[,] intgrid)
        {
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    if (chargrid[i, j] == '@')
                    {
                        setAdjacent(i, j, intgrid);
                        
                    }
                }
            }
        }

        public static void setAdjacent(int x, int y, int[,] intgrid)
        {
            if(x-1 >= 0)
            {
                intgrid[x - 1, y] += 1;
                if (y - 1 >= 0)
                {
                    intgrid[x - 1, y - 1] += 1;
                }
                if (y + 1 < ySize)
                {
                    intgrid[x - 1, y + 1] += 1;
                }
            }
            if (y - 1 >= 0)
            {
                intgrid[x, y-1] += 1;
            }
            if (x + 1 < xSize)
            {
                intgrid[x + 1, y] += 1;
                if (y - 1 >= 0)
                {
                    intgrid[x + 1, y - 1] += 1;
                }
                if (y + 1 < ySize)
                {
                    intgrid[x + 1, y + 1] += 1;
                }
            }
            if (y + 1 < ySize)
            {
                intgrid[x, y+1] += 1;
            }

           
        }
    }
}
