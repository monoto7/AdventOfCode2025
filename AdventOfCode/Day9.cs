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

            SortedList<Int64, List<Coordinate>> xValues = new SortedList<long, List<Coordinate>>();
            SortedList<Int64, List<Coordinate>> yValues = new SortedList<long, List<Coordinate>>();


            foreach (String line in input)
            {
                string[] split = line.Split(',');
                Coordinate coordinate = new Coordinate();
                coordinate.x = Int64.Parse(split[0]);

                if (!xValues.ContainsKey(coordinate.x))
                {
                    xValues.Add(coordinate.x, new List<Coordinate>());
                }
                xValues[coordinate.x].Add(coordinate);

                coordinate.y = Int64.Parse(split[1]);

                if (!yValues.ContainsKey(coordinate.y))
                {
                    yValues.Add(coordinate.y, new List<Coordinate>());
                }
                yValues[coordinate.y].Add(coordinate);

                coordinates.Add(coordinate);
            }

            maxX = xValues.Count + 2;

            maxY = yValues.Count + 2;

            int x = 1;
            foreach(Int64 key in xValues.Keys)
            {
                foreach(Coordinate coordinate in xValues[key])
                {
                    coordinate.tempx = x;
                }
                x++;
            }
            int y = 1;
            foreach (Int64 key in yValues.Keys)
            {
                foreach (Coordinate coordinate in yValues[key])
                {
                    coordinate.tempy = y;
                }
                y++;
            }

            getLargest(coordinates);


            
            //got bored with trying to figure out part 2.
            getLargestAndCheck(coordinates, makeMap(coordinates));
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
                        Int64 area = ((Math.Abs(coordinate.x - coordinate2.x) + 1) * (Math.Abs(coordinate.y - coordinate2.y) + 1));
                        areas.Add(area);
                        
                    }
                    else
                    {
                        Int64 area = Math.Abs((coordinate.x - coordinate2.x + 1) * (coordinate.y - coordinate2.y + 1));
                        
                    }
                }
            }
            Console.WriteLine(areas.Last());
            
        }

        static bool checkContained(Coordinate coordinate1, Coordinate coordinate2, char[][] chars)
        {
            int x = coordinate1.tempx < coordinate2.tempx ? coordinate1.tempx : coordinate2.tempx;
            int y = coordinate1.tempy < coordinate2.tempy ? coordinate1.tempy : coordinate2.tempy;
            int xDif = Math.Abs(coordinate1.tempx - coordinate2.tempx);
           

            while(xDif != -1)
            {
                int yDif = Math.Abs(coordinate1.tempy - coordinate2.tempy);
                while (yDif!= -1)
                {
                    if (chars[x + xDif][y + yDif] == '-')
                    {
                        return false;
                    }
                    yDif--;
                }
                xDif--;
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
                chars[c.tempx][c.tempy] = '@';
                if (curPos != (-1, -1))
                {

                    Int64 i1 =  c.tempx - curPos.Item1;
                    Int64 i2 =  c.tempy - curPos.Item2;
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
                curPos = (c.tempx, c.tempy);
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
            CheckSeed(chars);
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
        

        class Coordinate()
        {
           public Int64 x, y;
           public int tempx, tempy;
        }
    }
}
