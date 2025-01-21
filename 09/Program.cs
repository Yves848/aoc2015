using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Spectre.Console;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/09/test.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
Regex re = new(@"([\S]+) to ([\S]+) = (\d+)");

Dictionary<(string, string), int> travels = [];
HashSet<List<string>> routes = [];
HashSet<string> towns = [];
file.ForEach(t =>
{
  var m = re.Matches(t);
  travels.Add((m[0].Groups[1].Value, m[0].Groups[2].Value), int.Parse(m[0].Groups[3].Value));
  travels.Add((m[0].Groups[2].Value, m[0].Groups[1].Value), int.Parse(m[0].Groups[3].Value));
  towns.Add(m[0].Groups[1].Value);
  towns.Add(m[0].Groups[2].Value);
});

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

IEnumerable<List<T>> GetPermutations<T>(List<T> list, int length)
{
  if (length == 1)
    return list.Select(t => new List<T> { t });

  return GetPermutations(list, length - 1)
      .SelectMany(t => list.Where(e => !t.Contains(e)),
                  (t1, t2) => t1.Concat(new List<T> { t2 }).ToList());
}

void Part1()
{
  int ans = int.MaxValue;
  int ans2 = int.MinValue;
  Stack<(string, List<string>)> Q = [];
  Rule rule = new Rule();
  var allRoutes = GetPermutations(towns.ToList(), towns.Count);
  var routeDistances = new List<(string route, int distance)>();

  foreach (var route in allRoutes)
  {
    int totalDistance = 0;
    for (int i = 0; i < route.Count - 1; i++)
    {
      var segment = (route[i], route[i + 1]);
      totalDistance += travels[segment];
    }
    routeDistances.Add((string.Join(" -> ", route), totalDistance));
  }

  // Display all routes and their distances
  foreach (var (route, distance) in routeDistances)
  {
    // Console.WriteLine($"{route} = {distance}");
    if (distance < ans) ans = distance;
    if (distance > ans2) ans2 = distance;
  }
  Console.WriteLine($"Part 1 - Answer : {ans}");
  Console.WriteLine($"Part 2 - Answer : {ans2}");
}

void Part2()
{
  var ans = 0;
  
}

Part1();

// Part2();