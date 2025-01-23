using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
// List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/10/data.txt").ToList();
// var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";

// string puzzle = "1"; // test
string input = "1113122113"; // real


void part1()
{
  int ans = 0;
  string puzzle = input;
  for (int i = 0; i < 40; i++)
  {
    string result = "";
    char currentChar = puzzle[0];
    string currentGroup = "";
    foreach (char c in puzzle)
    {
      if (c == currentChar)
      {
        currentGroup += c;
      }
      else
      {
        //result.Add(currentGroup);
        result += currentGroup.Length.ToString() + currentGroup[0];
        currentGroup = c.ToString();
        currentChar = c;
      }
    }

    result += currentGroup.Length.ToString() + currentGroup[0];
    puzzle = result;
  }
  ans = puzzle.Length;
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  string puzzle = input;
  for (int i = 0; i < 50; i++)
  {
    string result = "";
    char currentChar = puzzle[0];
    string currentGroup = "";
    foreach (char c in puzzle)
    {
      if (c == currentChar)
      {
        currentGroup += c;
      }
      else
      {
        //result.Add(currentGroup);
        result += currentGroup.Length.ToString() + currentGroup[0];
        currentGroup = c.ToString();
        currentChar = c;
      }
    }

    result += currentGroup.Length.ToString() + currentGroup[0];
    puzzle = result;
    Console.WriteLine($"Part 2 - {i}");
  }
  ans = puzzle.Length;
  // ans = puzzle.Length;
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
