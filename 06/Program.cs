using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/06/test.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
Regex record = new(@"(on|off|toggle) (\d+),(\d+)[\s\D]+(\d+),(\d+)");


void print(string str, bool valid)
{
  if (valid)
  {
    Console.ForegroundColor = ConsoleColor.Green;
  }
  else
  {
    Console.ForegroundColor = ConsoleColor.Red;
  }
  Console.Write($"{str} ");
}

void part1()
{
  int ans = 0;
  int[,] grid = new int[1000,1000];
  file.ForEach(line => {
    var m = record.Matches(line);
    var op = m[0].Groups[1].Value;
    int x1 = int.Parse(m[0].Groups[2].Value);
    int y1 = int.Parse(m[0].Groups[3].Value);
    int x2 = int.Parse(m[0].Groups[4].Value);
    int y2 = int.Parse(m[0].Groups[5].Value);
    for (int c = x1; c<= x2; c++) {
      for (int r = y1; r <= y2; r++) {
        switch (op) {
          case "on" : 
            grid[c,r] = 1;
          break;
          case "off": 
            grid[c,r] = 0;
          break;
          case "toggle":
            grid[c,r] = grid[c,r]== 1 ? 0 : 1;
          break;
        }
      }
    }
  });
  for(int c = 0; c < 1000; c++) {
    for(int r = 0; r < 1000; r++) {
      ans += grid[c,r];
    }
  }
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  long ans = 0;
  long[,] grid = new long[1000,1000];
  file.ForEach(line => {
    var m = record.Matches(line);
    var op = m[0].Groups[1].Value;
    int x1 = int.Parse(m[0].Groups[2].Value);
    int y1 = int.Parse(m[0].Groups[3].Value);
    int x2 = int.Parse(m[0].Groups[4].Value);
    int y2 = int.Parse(m[0].Groups[5].Value);
    for (int c = x1; c<= x2; c++) {
      for (int r = y1; r <= y2; r++) {
        switch (op) {
          case "on" : 
            grid[c,r] += 1;
          break;
          case "off": 
            if (grid[c,r] > 0) grid[c,r] -= 1;
          break;
          case "toggle":
            grid[c,r] = grid[c,r] + 2;
          break;
        }
      }
    }
  });
  for(int c = 0; c < 1000; c++) {
    for(int r = 0; r < 1000; r++) {
      ans += grid[c,r];
    }
  }
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
