/*
children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1
*/

using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/16/data.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
Regex reData = new(@"Sue (\d+)|(\w+): (\d+)");
var dna = new Dictionary<string, int> {
  {"children", 3},
  {"cats", 7},
  {"samoyeds", 2},
  {"pomeranians", 3},
  {"akitas", 0},
  {"vizslas", 0},
  {"goldfish", 5},
  {"trees", 3},
  {"cars", 2},
  {"perfumes", 1}
};
void part1()
{
  int ans = 0;
  file.ForEach(line =>
  {
    var m = reData.Matches(line);
    bool valid = true;
    for (int i = 1; i < m.Count; i++)
    {
      string key = m[i].Groups[2].Value;
      int value = int.Parse(m[i].Groups[3].Value);
      valid = valid && dna.Keys.Contains(key) && dna[key] == value;
    }
    if (valid)
    {
      ans = int.Parse(m[0].Groups[1].Value);
    }
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  file.ForEach(line =>
  {
    var m = reData.Matches(line);
    bool valid = true;
    for (int i = 1; i < m.Count; i++)
    {
      string key = m[i].Groups[2].Value;
      int value = int.Parse(m[i].Groups[3].Value);
      if (new[] { "cats", "trees" }.Contains(key))
      {
        valid = valid && dna[key] < value;
      }
      else if (new[] { "pomeranians", "goldfish" }.Contains(key))
      {
        valid = valid && dna[key] > value;
      }
      else
        valid = valid && dna[key] == value;
    }
    if (valid)
    {
      ans = int.Parse(m[0].Groups[1].Value);
    }
  });
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
