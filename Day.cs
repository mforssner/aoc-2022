using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    public class Puzzle
    {
        public Puzzle(int dayNumber, int part, string? fileNameOverride = null)
        {
            DayNumber = dayNumber;
            Part = part;
            InputFile = MakeList(dayNumber, fileNameOverride);
            Console.WriteLine($"DAY {DayNumber} PART {Part}: " + GetSolution(DayNumber, Part));
        }

        public int DayNumber { get; set; }
        public int Part { get; set; }
        public string[] InputFile { get; set; }

        private static string[] MakeList(int day, string? overrideName = null)
        {
            return overrideName == null ? File.ReadAllLines(@$"C:\repos\Advent\input\day{day}.txt") : File.ReadAllLines(@$"C:\repos\Advent\input\{overrideName}.txt");
        }

        string GetSolution(int day, int part)
        {
            return day switch
            {
                1 => Day1(part),
                2 => "",
                3 => "",
                4 => "",
                5 => Day5(part),
                6 => Day6(part),
                _ => "oops"
            };
        }

        #region Day 1
        private string Day1(int part)
        {
            var startingList = InputFile.ToList();
            List<int> listOfSums = new();
            int sumOfCalories = 0;
            int topThreeElvesTotal = 0;
            foreach (string line in startingList)
            {
                if (line != "")
                {
                    int calories = int.Parse(line);
                    sumOfCalories += calories;
                }
                else
                {
                    listOfSums.Add(sumOfCalories);
                    sumOfCalories = 0;
                }
            }

            for (int i = 1; i <= 3; i++)
            {
                int maxValue = listOfSums.Max();
                if (part == 1) return maxValue.ToString();
                topThreeElvesTotal += maxValue;
                listOfSums.RemoveAt(listOfSums.IndexOf(maxValue));
            }

            return topThreeElvesTotal.ToString();
        }
        #endregion
        #region Day 5
        private string Day5(int part)
        {
            var grid = PopulateLists(InputFile).Take(9).ToList();
            string finalString = "";
            var instructions = InputFile.Skip(10).ToList();
            foreach (string line in instructions)
            {
                var temp = line.Split(" from ");
                int move = int.Parse(temp[0].Split(' ')[1]);
                int from = int.Parse(temp[1].Split(" to ")[0]) - 1;
                int to = int.Parse(temp[1].Split(" to ")[1]) - 1;

                if (part == 1)
                {
                    for (int i = 1; i <= move; i++)
                    {
                        grid[to] = grid[to] += grid[from][(grid[from].Length - 1)..];
                        grid[from] = grid[from][..(grid[from].Length - 1)];
                    }
                }
                else
                {
                    grid[to] = grid[to] += grid[from][(grid[from].Length - move)..];
                    grid[from] = grid[from][..(grid[from].Length - move)];
                }
            }

            foreach (string column in grid)
            {
                finalString += column[(column.Length - 1)..];
            }

            return finalString;

            static string[] PopulateLists(string[] list)
            {
                string string1 = "";
                string string2 = "";
                string string3 = "";
                string string4 = "";
                string string5 = "";
                string string6 = "";
                string string7 = "";
                string string8 = "";
                string string9 = "";

                int columnCounter = 1;
                int charInColumn = 0;
                int rowCounter = 0;
                for (int i = 7; i >= 0; i--)
                {
                    foreach (char ch in list[i])
                    {
                        rowCounter++;
                        charInColumn++;
                        if (rowCounter == 35)
                        {
                            rowCounter = 0;
                            charInColumn = 0;
                            columnCounter = 1;
                        }

                        if (charInColumn == 2 && ch != ' ')
                        {
                            if (columnCounter == 1) string1 += ch;
                            if (columnCounter == 2) string2 += ch;
                            if (columnCounter == 3) string3 += ch;
                            if (columnCounter == 4) string4 += ch;
                            if (columnCounter == 5) string5 += ch;
                            if (columnCounter == 6) string6 += ch;
                            if (columnCounter == 7) string7 += ch;
                            if (columnCounter == 8) string8 += ch;
                            if (columnCounter == 9) string9 += ch;
                        }
                        if (charInColumn == 4)
                        {
                            columnCounter++;
                            charInColumn = 0;
                        }
                    }
                }
                return new string[] { string1, string2, string3, string4, string5, string6, string7, string8, string9 };
            }
        }
        #endregion
        #region Day 6
        private string Day6(int part)
        {
            string file = InputFile[0];
            int k = part == 1 ? 4 : 14; 
            return Calculate().ToString();

            int Calculate()
            {
                int counter = 0;
                string memory = "";
                foreach (char c in file)
                {
                    counter++;
                    memory += c;
                    if (memory.Length == k+1)
                        memory = memory[1..];
                    if (memory.Length == k)
                    {
                        var check = CheckForDuplicatesDay6(counter, memory);
                        if (check != 0) return check;
                    }
                }
                return 0;
            }

            int CheckForDuplicatesDay6(int cn, string input)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = 0; j < input.Length; j++)
                    {
                        if (j != i)
                            if (input[i] == input[j])
                                return 0;
                    }
                }
                return cn;
            }
        }
        #endregion
    }
}


