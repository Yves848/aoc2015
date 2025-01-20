using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Spectre.Console;
using System.Threading.Tasks.Dataflow;

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

void part1()
{
  int ans = int.MaxValue;
  Stack<(string, List<string>)> Q = [];
  Rule rule = new Rule();
  towns.ToList().ForEach(t =>
  {
    Q.Clear();
    Q.Push((t, [t]));
    AnsiConsole.Write(rule);
    // Console.WriteLine(t);
    while (Q.Count > 0)
    {
      var (town, route) = Q.Pop();
      for (int i = 0; i < travels.Count; i++)
      {
        var (t1, t2) = travels.ToList()[i].Key;
        if (t1 == town)
        {
          if (!route.Contains(t2))
          {
            route.Add(t2);
            if (route.Count < towns.Count )
              Q.Push((t2, [.. route]));
          }
          if (route.Count == towns.Count)
          {
            if (!routes.Contains(route))
            {
              routes.Add(route);
              route.ForEach(r =>
              {
                Console.Write($"{r} ");
              });
              Console.WriteLine();
              // route.Clear();
            }

          }
        }
      }

    }

  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
