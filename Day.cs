using System.Collections.Generic;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Advent;

public class Day
{
    #region Class stuff
    public Day(int day, int part, string? fileNameOverride = null)
    {
        DayNumber = day;
        Part = part;
        InputFile = MakeList(fileNameOverride);
        Console.WriteLine($"DAY {DayNumber} PART {Part}: " + GetSolution());
    }

    private int DayNumber { get; set; }
    private int Part { get; set; }
    private string[] InputFile { get; set; }

    private string[] MakeList(string? overrideName = null)
    {
        return overrideName == null ? File.ReadAllLines(@$"C:\repos\Advent\input\day{DayNumber}.txt") : File.ReadAllLines(@$"C:\repos\Advent\input\{overrideName}.txt");
    }

    private string GetSolution()
    {
        return DayNumber switch
        {
            1 => Day1(),
            2 => Day2(),
            3 => Day3(),
            4 => Day4(),
            5 => Day5(),
            6 => Day6(),
            7 => Day7(),
            8 => Day8(),
            _ => "oops"
        };
    }
    #endregion

    #region Day 1
    private string Day1()
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
            if (Part == 1) return maxValue.ToString();
            topThreeElvesTotal += maxValue;
            listOfSums.RemoveAt(listOfSums.IndexOf(maxValue));
        }

        return topThreeElvesTotal.ToString();
    }
    #endregion
    #region Day 2 (refactor)
    private string Day2()
    {
        return Part == 1 ? Day2_Part1() : Day2_Part2();
    }
    private string Day2_Part1()
    {
        var list = InputFile.ToList();
        int totalPointsEnemy = 0;
        int totalPointsMe = 0;

        foreach (string row in list)
        {
            int pointsEnemy = 0;
            int pointsMe = 0;
            string[] moves = row.Split(" ");
            if (moves[1] == "X") pointsMe++;      //Rock
            if (moves[1] == "Y") pointsMe += 2;   //Paper
            if (moves[1] == "Z") pointsMe += 3;   //Scissors

            if (moves[0] == "A") pointsEnemy++;      //Rock
            if (moves[0] == "B") pointsEnemy += 2;   //Paper
            if (moves[0] == "C") pointsEnemy += 3;   //Scissors

            var outcome = CalculateWinner(moves[0], moves[1]);

            if (outcome == "draw")
            {
                pointsEnemy += 3;
                pointsMe += 3;
            }

            if (outcome == "win")
            {
                pointsMe += 6;
            }

            if (outcome == "loss")
            {
                pointsEnemy += 6;
            }

            totalPointsEnemy += pointsEnemy;
            totalPointsMe += pointsMe;
        }

        return totalPointsMe.ToString();

        static string CalculateWinner(string boss, string me)
        {
            if (boss == "A") //Rock
            {                       //vs
                if (me == "X")      //Rock
                    return "draw";
                else if (me == "Y") //Paper
                    return "win";
                else //Z            Scissors
                    return "loss";

            }
            else if (boss == "B")   //Paper
            {
                if (me == "X")
                    return "loss";
                else if (me == "Y")
                    return "draw";
                else //Z
                    return "win";
            }
            else //C                Scissors
            {
                if (me == "X")
                    return "win";
                else if (me == "Y")
                    return "loss";
                else //Z
                    return "draw";
            }
        }
    }

    private string Day2_Part2()
    {
        var list = InputFile.ToList();
        int totalPointsEnemy = 0;
        int totalPointsMe = 0;

        foreach (string row in list)
        {
            int pointsEnemy = 0;
            int pointsMe = 0;
            string[] moves = row.Split(" ");

            if (moves[0] == "A") pointsEnemy++;      //Rock
            if (moves[0] == "B") pointsEnemy += 2;   //Paper
            if (moves[0] == "C") pointsEnemy += 3;   //Scissors

            var outcome = CalculateWinner(moves[0], moves[1]);

            if (outcome == "draw")
            {
                pointsEnemy += 3;
                pointsMe += 3;
            }

            if (outcome == "win")
            {
                pointsMe += 6;
            }

            if (outcome == "loss")
            {
                pointsEnemy += 6;
            }

            totalPointsEnemy += pointsEnemy;
            totalPointsMe += pointsMe;

            string CalculateWinner(string boss, string me)
            {
                if (boss == "A") //Rock
                {
                    if (me == "X")
                    {
                        pointsMe += 3;
                        return "loss";
                    }
                    else if (me == "Y")
                    {
                        pointsMe += 1;
                        return "draw";
                    }
                    else //Z            Scissors
                    {
                        pointsMe += 2;
                        return "win";
                    }
                }

                else if (boss == "B")   //Paper
                {
                    if (me == "X")
                    {
                        pointsMe += 1;
                        return "loss";
                    }
                    else if (me == "Y")
                    {
                        pointsMe += 2;
                        return "draw";
                    }
                    else //Z
                    {
                        pointsMe += 3;
                        return "win";
                    }
                }
                else //C                Scissors
                {
                    if (me == "X")
                    {
                        pointsMe += 2;
                        return "loss";
                    }
                    else if (me == "Y")
                    {
                        pointsMe += 3;
                        return "draw";
                    }
                    else //Z
                    {
                        pointsMe += 1;
                        return "win";
                    }
                }
            }
        }

        return totalPointsMe.ToString();
    }
    #endregion
    #region Day 3 (refactor)
    private string Day3()
    {
        var list = InputFile.ToList();
        int prioTotal = 0;

        if (Part == 1) DoPart1();
        else DoPart2();

        return prioTotal.ToString();

        void DoPart1()
        {
            foreach (string rucksack in list)
            {
                int length = rucksack.Length / 2;
                var compartment1 = rucksack[..length];
                var compartment2 = rucksack[length..];

                prioTotal += GetCharPoints(GetChar());

                char GetChar()
                {
                    foreach (char c in compartment1)
                    {
                        foreach (char t in compartment2)
                            if (c == t) return c;
                    }
                    return '.';
                }
            }
        }

        void DoPart2()
        {
            var listOf3Rucksacks = new List<string>();
            int counter = 0;

            foreach (string rucksack in list)
            {
                counter++;
                listOf3Rucksacks.Add(rucksack);

                if (counter == 3)
                {
                    var compartment1 = listOf3Rucksacks[0];
                    var compartment2 = listOf3Rucksacks[1];
                    var compartment3 = listOf3Rucksacks[2];

                    counter = 0;
                    listOf3Rucksacks.Clear();

                    prioTotal += GetCharPoints(GetChar());

                    char GetChar()
                    {
                        foreach (char c in compartment1)
                        {
                            foreach (char d in compartment2)
                            {
                                if (c == d)
                                {
                                    foreach (char e in compartment3)
                                    {
                                        if (e == d) return e;
                                    }
                                }
                            }
                        }
                        return '.';
                    }
                }
            }
        }

        int GetCharPoints(char c)
        {
            return char.IsUpper(c) ? CharScorer(char.ToLower(c)) + 26 : CharScorer(c);
        }

        static int CharScorer(char c)
        {
            return c switch
            {
                'a' => 1,
                'b' => 2,
                'c' => 3,
                'd' => 4,
                'e' => 5,
                'f' => 6,
                'g' => 7,
                'h' => 8,
                'i' => 9,
                'j' => 10,
                'k' => 11,
                'l' => 12,
                'm' => 13,
                'n' => 14,
                'o' => 15,
                'p' => 16,
                'q' => 17,
                'r' => 18,
                's' => 19,
                't' => 20,
                'u' => 21,
                'v' => 22,
                'w' => 23,
                'x' => 24,
                'y' => 25,
                'z' => 26,
                _ => 0
            };
        }
    }
    #endregion
    #region Day 4
    private string Day4()
    {
        var list = InputFile.ToList();
        int total = 0;
        foreach (string line in list)
        {
            var pair = line.Split(',');
            var range1 = pair[0].Split('-');
            var range2 = pair[1].Split('-');

            if (Part == 1)
            {
                if (OneContainsTwo())
                    total++;
                else if (TwoContainsOne())
                    total++;
            }

            else if (IsAnyOverlap())
                total++;

            bool OneContainsTwo()
            {
                return int.Parse(range1[0]) <= int.Parse(range2[0]) && int.Parse(range1[1]) >= int.Parse(range2[1]);
            }
            bool TwoContainsOne()
            {
                return int.Parse(range2[0]) <= int.Parse(range1[0]) && int.Parse(range2[1]) >= int.Parse(range1[1]);
            }
            bool IsAnyOverlap()
            {
                return int.Parse(range1[1]) >= int.Parse(range2[0])
                    && int.Parse(range2[1]) >= int.Parse(range1[0]);
            }
        }
        return total.ToString();
    }
    #endregion
    #region Day 5
    private string Day5()
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

            if (Part == 1)
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
    private string Day6()
    {
        string file = InputFile[0];
        return Calculate().ToString();

        int Calculate()
        {
            int counter = 0;
            string memory = "";
            foreach (char c in file)
            {
                counter++;
                int k = Part == 1 ? 4 : 14;
                memory += c;
                if (memory.Length == k+1)
                    memory = memory[1..];
                if (memory.Length == k)
                {
                    var check = CheckForDuplicates(counter, memory);
                    if (check != 0) return check;
                }
            }
            return 0;
        }

        int CheckForDuplicates(int cn, string input)
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
    #region Day 7
    private string Day7()
    {
        var list = InputFile.ToList();
        //bool ls = false;
        string currentDirectory;
        double totalCount = 0;
        string[] currentPath = Array.Empty<string>();
        List<string> tempFolderList = new();
        double tempSize = 0;
        Day7 day = new();
        Day7.Folder folder = new(day, "/");
        int rowCounter = 0;

        foreach (string line in list)
            ParseLine(line);

        List<double> d = new();

        foreach (var f in day.List!)
        {
            string allAncestors = "";
            var temp = f;
            while (temp.Parent != null)
            {
                allAncestors = @$"{temp.Parent.Name}/{allAncestors}";
                temp = temp.Parent;
            }
            var x = f.TooBig ? "Big" : "Small";

            if (f.Size >= 30000000)
            {
                d.Add(f.Size);
                Console.WriteLine($"{allAncestors}{f.Name} {x} {f.Size}");
            }

            if (Part == 1 && !f.TooBig) totalCount += f.Size;
        }
        var closest = d.OrderBy(item => Math.Abs(30000000 - item)).First().ToString();

        if (Part == 1) return totalCount.ToString();
        else return closest;

        void ParseLine(string line)
        {
            rowCounter++;
            var sSplit = line.Split(' ');
            if (sSplit[0] == "$") CommandHandler(sSplit);
            else DefaultHandler(sSplit);
            if (rowCounter == 1052) LastRow();
        }

        void DefaultHandler(string[] sSplit)
        {
            if (sSplit[0] == "dir")
            {
                tempFolderList.Add(sSplit[1]);
            }
            else if (char.IsDigit(sSplit[0][0]))
            {
                var temp = int.Parse(sSplit[0]);
                tempSize += temp;
            }
        }

        void ChangeDirectory(string dir)
        {
            if (tempFolderList.Count != 0) folder.AddMoreFolders(day, tempFolderList);
            if (tempSize != 0)
                folder.Size = tempSize;
            tempSize = 0;
            tempFolderList.Clear();

            if (dir == "..")
            {
                currentPath = currentPath.SkipLast(1).ToArray();
                folder = folder.Parent!;
            }
            else
            {
                currentPath = currentPath.Append(dir).ToArray();
                if (folder.SubFolders != null)
                    folder = folder.SubFolders.ToList().Find(x => x.Name == currentPath.Last())!;
            }
            currentDirectory = currentPath.Last();
        }

        void CommandHandler(string[] sSplit)
        {
            if (sSplit is ["$", "cd", var directory])
            {
                ChangeDirectory(directory);
            }
        }

        void LastRow()
        {
            if (tempFolderList.Count != 0) folder.AddMoreFolders(day, tempFolderList);
            if (tempSize != 0)
                folder.Size = tempSize;
        }
    }
    #endregion
    #region Day 8
    private string Day8()
    {
        var grid = MakeGrid();
        int visible = 0;
        //int visible = 392 +  97 * 97; //99x99 + alla kanter är alltid synliga
        GridMethod(grid);

        return visible.ToString();


        void GridMethod(int[][] grid)
        {
            int l = grid.Length;
            int x = 0;
            int y = 0;

            foreach (var row in grid)
            {
                foreach (var value in row)
                {
                    if (CheckAllDirections(x, y, value)) visible++;
                    x++;
                    if (x == l) x = 0;
                }
                y++;
                if (y == l) y = 0;
            }

            bool CheckAllDirections(int col, int row, int input)
            {
                if (CheckLeft(row, col, input)
                    || CheckRight(row, col, input)
                    || CheckUp(row, col, input)
                    || CheckDown(row, col, input))
                {
                    return true;
                }
                else return false;
            }

            bool CheckLeft(int row, int colPos, int input)
            {
                for (int col = colPos-1; col >= 0; col--)
                {
                    if (input <= grid[row][col]) return false;
                }
                return true;
            }

            bool CheckRight(int row, int colPos, int input)
            {
                for (int col = colPos+1; col < l; col++)
                {
                    if (input <= grid[row][col]) return false;
                }
                return true;
            }

            bool CheckUp(int rowPos, int col, int input)
            {
                for (int row = rowPos-1; row >= 0; row--)
                {
                    if (input <= grid[row][col]) return false;
                }
                return true;
            }

            bool CheckDown(int rowPos, int col, int input)
            {
                for (int row = rowPos+1; row < l; row++)
                {
                    if (input <= grid[row][col]) return false;
                }
                return true;
            } 
        }

        int[][] MakeGrid()
        {
            string[] list = InputFile;
            List<int[]> gridList = new();
            List<int> rowList = new();
            foreach (var row in list)
            {
                foreach (var c in row)
                {
                    rowList.Add(int.Parse(c.ToString()));
                }
                gridList.Add(rowList.ToArray());
                rowList.Clear();
            }
            return gridList.ToArray();
        }
    }
    #endregion
}