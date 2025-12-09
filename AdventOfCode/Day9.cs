using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Day9
    {
        public static Int64 maxX;
        public static Int64 maxY;
        public static List<(Int64, Int64)> seedpositions = new List<(Int64, Int64)>();
        public static void SelectInput()
        {
            Console.WriteLine("Select input, 1:Example, 2:Main, otherwise return");
            String[] input = { };
            switch (Console.ReadLine())
            {
                case "1":
                    input = File.ReadAllLines("Day9Inputs/Day9Example.txt");
                    break;
                case "2":
                    input = File.ReadAllLines("Day9Inputs/Day9Input.txt");
                    break;
                default:
                    return;
            }
            List<Coordinate> coordinates = new List<Coordinate>();

            maxX = 0;
            maxY = 0;
            foreach (String line in input)
            {
                string[] split = line.Split(',');
                Coordinate coordinate = new Coordinate();
                coordinate.x = Int64.Parse(split[0]);
                if (coordinate.x > maxX) maxX = coordinate.x + 2;
                coordinate.y = Int64.Parse(split[1]);
                if (coordinate.y > maxY) maxY = coordinate.y + 2;
                coordinates.Add(coordinate);
            }
            getLargest(coordinates);

            
            //got bored with trying to figure out part 2.
            //getLargestAndCheck(coordinates, makeMap(coordinates));
        }

        static void getLargest(List<Coordinate> coordinates)
        {
            SortedSet<Int64> areas = new SortedSet<Int64>();
            foreach (Coordinate coordinate in coordinates)
            {
                foreach(Coordinate coordinate2 in coordinates)
                {
                    Int64 area = Math.Abs((coordinate.x - coordinate2.x + 1) * (coordinate.y - coordinate2.y +1));
                    areas.Add(area);
                }
            }
            Console.WriteLine("Part 1:" + areas.Last());
        }

        static void getLargestAndCheck(List<Coordinate> coordinates, char[][] chars)
        {
            SortedSet<Int64> areas = new SortedSet<Int64>();
            foreach (Coordinate coordinate in coordinates)
            {
                foreach (Coordinate coordinate2 in coordinates)
                {
                    if (checkContained(coordinate, coordinate2, chars))
                    {
                        Int64 area = Math.Abs((coordinate.x - coordinate2.x + 1) * (coordinate.y - coordinate2.y + 1));
                        areas.Add(area);
                    }
                }
            }
            Console.WriteLine(areas.Last());
        }

        static bool checkContained(Coordinate coordinate1, Coordinate coordinate2, char[][] chars)
        {
            Int64 x = coordinate1.x - coordinate2.x;
            bool leftx = false;
            
            x *= -1;
            while (x != 0)
            {
                Int64 y = coordinate1.y - coordinate2.y;
                y *= -1;
                bool lefty = false;
                if (chars[coordinate1.x + x][coordinate1.y + y] == '@' || chars[coordinate1.x + x][coordinate1.y + y] == 'X')
                {
                    if (leftx)
                    {
                        return false;
                    }
                }
                {
                    leftx = true;
                }
                while (y != 0)
                {
                    if (chars[coordinate1.x + x][coordinate1.y + y] == '@' || chars[coordinate1.x + x][coordinate1.y + y] == 'X')
                    {
                        if (lefty)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        lefty = true;
                    }
                    y = y > 0 ? y - 1 : y < 0 ? y + 1 : y;
                }
                x = x > 0 ? x - 1 : x < 0 ? x + 1 : x;
            }
            return true;
            
        }

       


        static char[][] makeMap(List<Coordinate> coordinates)
        {
            char[][] chars = new char[maxX][];
            for (int i = 0; i< chars.Length; i++)
            {
                chars[i] = new char[maxY];
            }
            coordinates.Add(coordinates[0]);
            (Int64, Int64) curPos = (-1, -1);
            foreach(Coordinate c in coordinates)
            {
                chars[c.x][c.y] = '@';
                if (curPos != (-1, -1))
                {

                    Int64 i1 =  c.x - curPos.Item1;
                    Int64 i2 =  c.y - curPos.Item2;
                    while (i1 != 0 || i2 != 0)
                    {
                        if (chars[curPos.Item1 + i1][ curPos.Item2 + i2] != '@')
                        {
                            chars[curPos.Item1 + i1][ curPos.Item2 + i2] = 'X';
                        }
                        i1 = i1 > 0 ? i1-1:i1<0?i1+1:i1;
                        i2 = i2 > 0 ? i2-1 : i2 < 0 ? i2+1 : i2;
                    }

                }
                curPos = (c.x, c.y);
            }
            for (int i = 0; i < maxX; i++)
            {
                for (int i1 = 0; i1 < maxY; i1++)
                {
                    if (chars[i][ i1] == 0)
                    {
                        chars[i][ i1] = '.';
                    }
                }
            }
            Console.WriteLine("Map Made");
            //CheckSeed(chars);
            return chars;
        }

        static void CheckSeed(char[][] chars)
        {
            seedpositions.Add((0, 0));
            while(seedpositions.Count > 0)
            {
                FillMapScan(chars, seedpositions[0]);
            }
        }
        static void FillMapScan(char[][] chars, (Int64, Int64) curpos)
        {
            bool move = true;
            while (move)
            {
                move = false;
                chars[curpos.Item1][curpos.Item2] = '-';
                seedpositions.RemoveAll(e=> e == curpos);

                if (curpos.Item1 - 1 > 0 && chars[curpos.Item1 - 1][curpos.Item2] == '.')
                {
                    seedpositions.Add((curpos.Item1 - 1, curpos.Item2));
                }
                if (curpos.Item1 + 1 < maxX && chars[curpos.Item1 + 1][curpos.Item2] == '.')
                {
                    seedpositions.Add((curpos.Item1 + 1, curpos.Item2));
                }
                if (curpos.Item2 - 1 > 0 && chars[curpos.Item1][curpos.Item2 - 1] == '.')
                {
                    seedpositions.Add((curpos.Item1, curpos.Item2 - 1));
                    
                }
                if (curpos.Item2 + 1 < maxY && chars[curpos.Item1][curpos.Item2 + 1] == '.')
                {
                    curpos.Item2++;
                    move = true;
                }
                
            }

        }
        static void FillMap(char[][] chars, (Int64, Int64) curpos)
        {
           
            chars[curpos.Item1][curpos.Item2] = '-';
            
                if (curpos.Item1 - 1 > 0 && chars[curpos.Item1 - 1][curpos.Item2] == '.')
                {
                    FillMap(chars, (curpos.Item1 - 1, curpos.Item2));
                }
                if (curpos.Item1 + 1 < maxX && chars[curpos.Item1 + 1][curpos.Item2] == '.')
                {
                    FillMap(chars, (curpos.Item1 + 1, curpos.Item2));
                }
                if (curpos.Item2 - 1 > 0 && chars[curpos.Item1][curpos.Item2 -1] == '.')
                {
                    FillMap(chars, (curpos.Item1, curpos.Item2 - 1));
                }
                if (curpos.Item2 + 1 < maxY && chars[curpos.Item1][curpos.Item2 + 1] == '.')
                {
                    FillMap(chars, (curpos.Item1, curpos.Item2 + 1));
                }

        }
        

        struct Coordinate()
        {
           public Int64 x, y;
        }
    }
}
