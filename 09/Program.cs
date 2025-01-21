using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Spectre.Console;
using System.Threading.Tasks.Dataflow;
using System.IO.Pipelines;
using System.Diagnostics;

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

List<string> destinations(string town, string seen)
{
  List<string> result = travels.Where(kvp => kvp.Key.Item1 == town && !seen.Contains(kvp.Key.Item2)).ToList().Select(t => t.Key.Item2).ToList();
  return result;
}
void Part1()
{
  int ans = int.MaxValue;
  Stack<(string, List<string>)> Q = [];
  Rule rule = new Rule();
  towns.ToList().ForEach(town =>
  {
    HashSet<string> visit = [];
    string visited = "";
    List<string> dest = destinations(town, visited).ToList();
    while (dest.Count > 0)
    {
      List<string> route = [town];
      route.Add(dest[0]);
      visited += dest[0];
      if (dest.Count > 1)
      {
        dest.RemoveAt(0);
        dest = destinations(dest[0], visited);
      } else {
        routes.Add(route);
        visit.Add(visited);
        dest = destinations(town, visited);

      }
    }
    
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void Part2()
{
  var ans = 0;
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

Part1();

Part2();