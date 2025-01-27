using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
// List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/20/data.txt").ToList();
// var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";

int puzzle = 34000000;
Dictionary<int,int> houses = [];
var elfes = new Dictionary<int,int> {{1,1}};
HashSet<(int,int)> visited = [];
void part1()
{
  int ans = 0;
  int house = 1;
  while(true) {
    int elf = 0;
    while (elf < elfes.Count) {
      if (!visited.Contains((elf+1,house))) {
        visited.Add((elf+1,house));
        if (house % (elf+1) == 0) {
          if (!houses.ContainsKey(house)) houses.Add(house,0);
          houses[house] = houses[house] + 10*(elf+1);
          if (houses[house] >= puzzle) {
            ans = house;
            break;
          }
        }
      }
      elf++;
    }
    if (ans > 0) break;
    elfes.Add(elf+1,elf+1);
    house++;
  }
  
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
