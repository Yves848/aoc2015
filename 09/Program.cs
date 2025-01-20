﻿using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.IO.Pipelines;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/09/data.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
Regex re = new(@"([\S]+) to ([\S]+) = (\d+)");

Dictionary<(string, string), int> travels = [];
HashSet<string> routes = [];
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

bool travelExists(string travel)
{
  bool result = false;
  for (int i = 0; i < routes.Count; i++)
  {
    if (routes.ToList()[i].StartsWith(travel))
    {
      result = true;
      break;
    }
  }
  return result;
}

(string, int) findNext(string town, string travel)
{
  (string, int) result = ("", 0);

  for (int i = 0; i < travels.ToList().Count; i++)
  {
    var (t1, t2) = travels.ToList()[i].Key;
    var d = travels.ToList()[i].Value;
    if (t1 == town && !travel.Contains(t2))
    {
      if (!travelExists(travel + t2))
      {
        result = (t2, d);
        break;
      }
    }
  }
  ;
  return result;
}



void part1()
{
  int ans = int.MaxValue;
  Stack<(string,List<string>)> Q = [];
  towns.ToList().ForEach(t =>
  {
    Q.Push((t,[t]));
    while (Q.Count > 0)
    {
      var (town, travel) = Q.Pop();

      for (int i = 0; i < travels.ToList().Count; i++)
      {
        var (t1, t2) = travels.ToList()[i].Key;
        var d = travels.ToList()[i].Value;
        if (t1 == town)
        {
          if (!travel.Contains(t2)) {
            travel.Add(t2);
            town = t2;
          } else {
            Q.Push((t1,[..travel]));
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
