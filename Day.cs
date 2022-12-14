using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace Advent;

public class Day
{
    #region Class stuff
    public Day(int day, int part, string? fileNameOverride = null)
    {
        DayNumber = day;
        Part = part;
        InputFile = MakeList(fileNameOverride);
        var watch = System.Diagnostics.Stopwatch.StartNew();
        Console.WriteLine($"DAY {DayNumber} PART {Part}: " + GetSolution());
        watch.Stop();
        Console.WriteLine($"RUNTIME: {watch.ElapsedMilliseconds}ms");
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
            9 => Day9(),
            10 => Day10(),
            11 => Day11(),
            12 => Day12(),
            13 => Day13(),
            14 => Day14(),
            //15 => Day15(),
            //16 => Day16(),
            //17 => Day17(),
            //18 => Day18(),
            //19 => Day19(),
            //20 => Day20(),
            //21 => Day21(),
            //22 => Day22(),
            //23 => Day23(),
            //24 => Day24(),
            //25 => Day25(),
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
            if (line.Any())
            {
                sumOfCalories += int.Parse(line);
            }
            else
            {
                listOfSums.Add(sumOfCalories);
                sumOfCalories = 0;
            }
        }

        if (Part == 1) return listOfSums.Max().ToString();

        for (int i = 0; i < 3; i++)
        {
            int maxValue = listOfSums.Max();
            topThreeElvesTotal += maxValue;
            listOfSums.RemoveAt(listOfSums.IndexOf(maxValue));
        }

        return topThreeElvesTotal.ToString();
    }
    #endregion
    #region Day 2 (refactor needed!)
    private string Day2()
    {
        return Part == 1 ? Day2_Part1() : Day2_Part2();

        string Day2_Part1()
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

        string Day2_Part2()
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
    }
    #endregion
    #region Day 3
    private string Day3()
    {
        var list = InputFile.ToList();
        int prioTotal = 0;
        var listOf3Rucksacks = new List<string>();
        int counter = 0;

        foreach (string rucksack in list)
        {
            if (Part == 1)
            {
                int length = rucksack.Length / 2;
                var firstCompartment = rucksack[..length];
                var secondCompartment = rucksack[length..];

                prioTotal += GetCharPoints(firstCompartment, secondCompartment);
            }

            if (Part == 2)
            {
                counter++;
                listOf3Rucksacks.Add(rucksack);

                if (counter == 3)
                {
                    var firstSack = listOf3Rucksacks[0];
                    var secondSack = listOf3Rucksacks[1];
                    var thirdSack = listOf3Rucksacks[2];

                    counter = 0;
                    listOf3Rucksacks.Clear();

                    prioTotal += GetCharPoints(firstSack, secondSack, thirdSack);
                }
            }
        }

        return prioTotal.ToString();

        static int GetCharPoints(string one, string two, string? three = null)
        {
            foreach (char c in one)
            {
                foreach (char d in two)
                {
                    if (d == c)
                    {
                        if (three == null) return char.IsUpper(d) ? d - 38 : d - 96;
                        else foreach(char e in three)
                                if (e == d) return char.IsUpper(e) ? e - 38 : e - 96;
                    }
                }   
            }
            return 0;
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
            var a = pair[0].Split('-');
            var b = pair[1].Split('-');

            if (Part == 1) 
            {
                if (AContainsB() || BContainsA()) total++;
            }

            else if (IsAnyOverlap()) total++;

            bool AContainsB()
            {
                return int.Parse(a[0]) <= int.Parse(b[0]) && int.Parse(a[1]) >= int.Parse(b[1]);
            }
            bool BContainsA()
            {
                return int.Parse(b[0]) <= int.Parse(a[0]) && int.Parse(b[1]) >= int.Parse(a[1]);
            }
            bool IsAnyOverlap()
            {
                return int.Parse(a[1]) >= int.Parse(b[0]) && int.Parse(b[1]) >= int.Parse(a[0]);
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
                    grid[to] = grid[to] += grid[from][^1..];
                    grid[from] = grid[from][..^1];
                }
            }
            else
            {
                grid[to] = grid[to] += grid[from][^move..];
                grid[from] = grid[from][..^move];
            }
        }

        foreach (string column in grid)
        {
            finalString += column[^1..];
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

            foreach (string row in list.Reverse())
            {
                int position = 0;
                int column = 1;

                foreach (char ch in row)
                {
                    position++;
                    if (position == 2 && ch != ' ')
                    {
                        if (column == 1) string1 += ch;
                        if (column == 2) string2 += ch;
                        if (column == 3) string3 += ch;
                        if (column == 4) string4 += ch;
                        if (column == 5) string5 += ch;
                        if (column == 6) string6 += ch;
                        if (column == 7) string7 += ch;
                        if (column == 8) string8 += ch;
                        if (column == 9) string9 += ch;
                    }
                    if (position == 4)
                    {
                        column++;
                        position = 0;
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
        List<string> list = InputFile.ToList();
        int totalCount = 0;
        List<string> foldersInCurrentFolder = new();
        int sizeOfCurrentFolder = 0;
        Day7 day = new();
        Day7.Folder folder = new(day, @"/");
        int rowCounter = 0;

        foreach (string line in list)
            ParseLine(line);

        int freeSpace = 70000000 - day.List[0].Size;
        int spaceNeeded = 30000000 - freeSpace;
        List<int> bigEnough = new();
        foreach (Day7.Folder? f in day.List!)
        {
            //DebugPrintAllFolders(f);
            if (Part == 1 && !f.TooBig) totalCount += f.Size;
            if (Part == 2 && f.Size >= spaceNeeded) bigEnough.Add(f.Size);
        }
        int closest = Part == 2 ? bigEnough.Order().First() : 0;

        return (Part == 1) ? totalCount.ToString(): 
             /* Part == 2 */ closest.ToString();


        void ParseLine(string line)
        {
            rowCounter++;
            string[] s = line.Split(' ');
            if (s[0] == "$") CommandHandler(s);
            else DefaultHandler(s);

            if (line == list[^1]) //last line in list
                FolderLogic();
        }

        void CommandHandler(string[] s)
        {
            if (s is ["$", "cd", string dir])
            {
                FolderLogic();

                if (dir == "..") 
                    folder = folder.Parent!;
                else if (folder.SubFolders != null)
                    folder = folder.SubFolders.ToList().Find(x => x.Name == dir)!;
            }
        }

        void DefaultHandler(string[] s)
        {
            if (s[0] == "dir") foldersInCurrentFolder.Add(s[1]);
            else sizeOfCurrentFolder += int.Parse(s[0]);
        }

        void FolderLogic()
        {
            if (foldersInCurrentFolder.Count > 0) MakeSubFolders();
            if (sizeOfCurrentFolder > 0) SaveFolderSize();

            void SaveFolderSize()
            {
                folder.Size = sizeOfCurrentFolder;
                sizeOfCurrentFolder = 0;
            }
            void MakeSubFolders()
            {
                folder.AddMoreFolders(day, foldersInCurrentFolder);
                foldersInCurrentFolder.Clear();
            }
        }

        //static void DebugPrintAllFolders(Day7.Folder f)
        //{
        //    string allAncestors = "";
        //    while (f.Parent != null)
        //    {
        //        allAncestors = @$"{f.Parent.Name}/{allAncestors}";
        //        f = f.Parent;
        //    }
        //    Console.WriteLine($"{allAncestors}{f.Name} {f.Size}");
        //}
    }


    #endregion
    #region Day 8 (refactor needed!)
    private string Day8()
    {
        var grid = MakeGrid();
        int l = grid.Length;
        int x = 0;
        int y = 0;
        int visible = 0;
        int highestScenicValue = 0;

        foreach (var row in grid)
        {
            foreach (var tree in row)
            {
                CheckAllDirections(x, y, tree);
                x++;
                if (x == l) x = 0;
            }
            y++;
            if (y == l) y = 0;
        }

        return Part == 1 ?  visible.ToString():
            /* Part == 2 */ highestScenicValue.ToString();


        void CheckAllDirections(int col, int row, int input)
        {
            int temp = Part == 1 ? CheckLeft(row, col, input) + CheckRight(row, col, input) + CheckUp(row, col, input) + CheckDown(row, col, input):
                                    CheckLeft(row, col, input) * CheckRight(row, col, input) * CheckUp(row, col, input) * CheckDown(row, col, input);
            if (temp > 0)
            {
                visible++;
                if (highestScenicValue < temp)
                    highestScenicValue = temp;
            }
        }

        int CheckLeft(int rowPos, int colPos, int input)
        {
            int distance = 0;
            for (int col = colPos-1; col >= 0; col--)
            {
                distance++;
                if (input <= grid[rowPos][col]) return Part == 1 ? 0 : distance;
            }
            return Part == 1 ? 1 : distance;
        }

        int CheckRight(int rowPos, int colPos, int input)
        {
            int distance = 0;
            for (int col = colPos+1; col < l; col++)
            {
                distance++;
                if (input <= grid[rowPos][col]) return Part == 1 ? 0 : distance;
            }
            return Part == 1 ? 1 : distance;
        }

        int CheckUp(int rowPos, int colPos, int input)
        {
            int distance = 0;
            for (int row = rowPos-1; row >= 0; row--)
            {
                distance++;
                if (input <= grid[row][colPos]) return Part == 1 ? 0 : distance;
            }
            return Part == 1 ? 1 : distance;
        }

        int CheckDown(int rowPos, int colPos, int input)
        {
            int distance = 0;
            for (int row = rowPos+1; row < l; row++)
            {
                distance++;
                if (input <= grid[row][colPos]) return Part == 1 ? 0 : distance;
            }
            return Part == 1 ? 1 : distance;
        }

        int[][] MakeGrid()
        {
            string[] list = InputFile;
            List<int[]> gridList = new();
            List<int> rowList = new();
            foreach (var row in list)
            {
                foreach (var c in row)
                    rowList.Add(int.Parse(c.ToString()));

                gridList.Add(rowList.ToArray());
                rowList.Clear();
            }
            return gridList.ToArray();
        }
    }
    #endregion
    #region Day 9 (Part 2 not done)
    private string Day9()
    {
        var list = InputFile.ToList();
        var grid = MakeGrid();
        var headPos = new int[]{ 500, 500 };
        var tailPos = new int[]{ 500, 500 };
        var lastHeadPos = new int[] { 500, 500 };
        int visitedAtleastOnce = 1; //Starting position included

        foreach (var line in list)
        {
            var sp = line.Split(" ");
            var direction = sp[0];
            int steps = int.Parse(sp[1]);

            for (int i = 0; i < steps; i++)
            {
                lastHeadPos[0] = headPos[0];
                lastHeadPos[1] = headPos[1];
                MoveHead(direction);
                MoveTail();
            }
        }

        return visitedAtleastOnce.ToString();


        void MoveHead(string input)
        {
             switch (input)
             {
                case "U":
                    headPos[0]--;
                    break;
                case "D":
                    headPos[0]++;
                    break;
                case "L":
                    headPos[1]--;
                    break;
                case "R":
                    headPos[1]++;
                    break;
             }
        }

        void MoveTail()
        {
            if (tailPos[0] > headPos[0] + 1 || tailPos[0] < headPos[0] - 1 || tailPos[1] > headPos[1] + 1 || tailPos[1] < headPos[1] - 1)
            {
                tailPos[0] = lastHeadPos[0];
                tailPos[1] = lastHeadPos[1];
                if (grid[tailPos[0]][tailPos[1]] == 0)
                {
                    grid[tailPos[0]][tailPos[1]] = 1;
                    visitedAtleastOnce++;
                }
            }
        }

        int[][] MakeGrid()
        {
            int[][] grid = new int[1000][];
            for (int i = 0; i < 1000; i++)
                grid[i] = new int[1000];
            return grid;
        }
    }
    #endregion
    #region Day 10
    private string Day10()
    {
        var list = InputFile.ToList();
        int cycle = 0;
        int x = 1;
        int sum = 0;
        var crtOutput = "\n";

        foreach (string instruction in list)
        {
            StartNewCycle();
            if (instruction == "noop") continue;
            StartNewCycle();
            x += int.Parse(instruction.Split(' ')[1]);
        }

        return Part == 1 ? sum.ToString() : crtOutput;

        void StartNewCycle()
        {
            cycle++;
            if (Part == 1 && cycle == 20 || (cycle - 20) % 40 == 0) sum += cycle * x;
            PrintToCRT(cycle > 40 ? cycle % 40 : cycle, x);
        }

        void PrintToCRT(int t, int x)
        {
            if (IsLit()) crtOutput += "#";
            else crtOutput += ".";
            if (t % 40 == 0) crtOutput += "\n";

            bool IsLit()
            {
                var c = t - 1;
                if (c < 0) return false;
                return (c >= x - 1 && c <= x + 1);
            }
        }
    }
    #endregion
    #region Day 11 (Not done)
    private string Day11()
    {
        var list = InputFile.ToList();

        return "";
    }
    #endregion
    #region Day 12 (Not done, buggy mess)
    private string Day12()
    {
        var grid = MakeGrid();
        var startPos = FindStartPosition(grid);
        int currentLowestNumberOfSteps = 1000;
        int numberOfSimulatedSteps = 0;

        SimulateStep(startPos, MakeBoolGrid(grid.Length), 0, 0);

        Console.WriteLine($"Steps simulated: {numberOfSimulatedSteps}");
        return currentLowestNumberOfSteps.ToString();


        void SimulateStep(int[] pos, List<List<bool>> visited, int steps, int depth, char? dir = null)
        {
            depth++;
            numberOfSimulatedSteps++;
            visited[pos[0]][pos[1]] = true;
            //Console.WriteLine($"[{pos[0]}][{pos[1]}] {grid[pos[0]][pos[1]]}");
            if (grid[pos[0]][pos[1]] != 'E')
            {
                var lookDown = Look('D', pos.ToArray(), visited, steps, depth);
                var lookRight = Look('R', pos.ToArray(), visited, steps, depth);
                var lookLeft = Look('L', pos.ToArray(), visited, steps, depth);
                var lookUp = Look('U', pos.ToArray(), visited, steps, depth);
                if (lookRight + lookLeft + lookUp + lookDown == 48*4 /* 4 nollor */)
                {
                    PrintMap(visited, pos);
                }
            }

            else if (steps < currentLowestNumberOfSteps) currentLowestNumberOfSteps = steps;
        }

        void PrintMap(List<List<bool>> visited, int[] pos)
        {
            Console.WriteLine("\n\n");
            string str = string.Empty;
            for (int i = 0; i < visited.Count; i++)
            {
                for (int j = 0; j < visited.Count; j++)
                {
                    if (i == pos[0] && j == pos[1]) str += 'O';
                    else if (i == startPos[0] && j == startPos[1]) str += 'S';
                    else str += visited[i][j] == true ? "." : "#";
                }
                Console.WriteLine(str);
                str = string.Empty;
            }
        }

        char Look(char direction, int[] pos, List<List<bool>> visited, int steps, int depth)
        {
            int y = pos[0];
            int x = pos[1];
            switch (direction)
            {
                case 'L':
                    x--;
                    break;
                case 'R':
                    x++;
                    break;
                case 'U':
                    y--;
                    break;
                case 'D':
                    y++;
                    break;
            }
            
            if (x < 0 || y < 0 || x > 40 || y > 40) { return '0'; }

            var current = grid[pos[0]][pos[1]];
            var checking = grid[y][x];

            if (checking <= current + 1 && !visited[y][x])
            {
                steps++;
                SimulateStep(new int[] {y, x}, visited, steps, depth, direction);
                return current;
            }
            return '0';
        }

        int[] FindStartPosition(char[][] grid)
        {
            for (int i = 0; i < grid.Length; i++)
                for (int j = 0; j < grid[i].Length; j++)
                    if (grid[i][j] == 'S')
                    {
                        grid[i][j] = 'a';
                        return new int[] { i, j };
                    }
            return new int[] { 0, 0 };
        }

        char[][] MakeGrid()
        {
            string[] list = InputFile;
            List<char[]> gridList = new();
            List<char> rowList = new();
            foreach (string row in list)
            {
                foreach (var c in row)
                    rowList.Add(c);

                gridList.Add(rowList.ToArray());
                rowList.Clear();
            }
            return gridList.ToArray();
        }

        List<List<bool>> MakeBoolGrid(int length)
        {
            List<List<bool>> gridList = new();
            List<bool> rowList = new();
            for (int j = 0; j < length; j++)
                rowList.Add(false);

            for (int i = 0; i < length; i++)
            {
                gridList.Add(rowList);
            }
            return gridList;
        }
    }
    #endregion
    #region Day 13 (Not done)
    private string Day13()
    {
        var list = InputFile.ToList();

        return "";
    }
    #endregion
    #region Day 14 (Not done)
    private string Day14()
    {
        var list = InputFile.ToList();

        return "";
    }
    #endregion


    #region Day Template
    private string DayX()
    {
        var list = InputFile.ToList();

        return "";
    }
    #endregion
}