using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.IO.Pipelines;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/14/test.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";

Regex reData = new(@"(\w+) .+ (\d+) km\/s for (\d+) .+ (\d+)");
Dictionary<string, (int, int, int, int)> deers = [];
file.ForEach(deer =>
{
  var m = reData.Match(deer);
  deers.Add(m.Groups[1].Value, (int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value), 0));
});

int distance(string deer, int dist)
{
  int result = 0;
  var (km, t, rest, _) = deers[deer];
  int d1 = km * t;
  int totalTime = t + rest;
  int r = dist / totalTime;
  int remainder = dist - (r * totalTime);
  result = r * d1;
  if (remainder > 0)
  {
    if (remainder >= t)
    {
      result += d1;
    }
    else
    {
      result += (remainder * km);
    }
  }
  return result;
}

void part1()
{
  int ans = 0;
  deers.ToList().ForEach(deer =>
  {
    string name = deer.Key;
    int d = distance(name, 2503);
    if (d > ans) ans = d;
    Console.WriteLine($"{name} {d}");
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  for (int i = 1; i <= 2503; i++)
  {
    int ans = 0;
    HashSet<string> winners = [];
    deers.ToList().ForEach(deer =>
    {
      string name = deer.Key;
      int d = distance(name, i);
      if (d >= ans)
      {
        if (d > ans) winners.Clear();
        ans = d;
        winners.Add(name);
      }
    });
    winners.ToList().ForEach(winner =>
    {

      var (km, t, rest, score) = deers[winner];
      score++;
      deers[winner] = (km, t, rest, score);
    });
  }
  deers.ToList().ForEach(deer =>
  {
    Console.WriteLine($"{deer.Key} : {deer.Value.Item4} points");
  });
  // Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
