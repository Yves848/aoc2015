using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.IO.Pipelines;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/07/data.txt").ToList();
List<string> file2 = [.. file];
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
Dictionary<string, int> reg = [];
Regex dest = new(@"-> (\w+)");
Regex left = new(@"(.*) ->");
List<string> operations = ["AND", "OR", "NOT", "LSHIFT", "RSHIFT"];

int getValue(string val)
{
  int result = 0;
  if (!int.TryParse(val, out result))
  {
    if (!reg.ContainsKey(val)) reg.Add(val, -1);
    result = reg[val];
  }
  return result;
}

void setValue(string r, int val)
{
  if (!reg.ContainsKey(r)) reg.Add(r, 0);
  if (r=="b") val = 956;
  reg[r] = val;
}

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
  int i = 0;
  while (i < file.Count)
  {
    string line = file[i];
    string d = dest.Match(line).Groups[1].Value;
    string l = left.Match(line).Groups[1].Value;
    int val = 0;
    if (line.Contains("AND"))
    {
      Regex reAnd = new(@"(.+) AND (.+)");
      var p1 = getValue(reAnd.Match(l).Groups[1].Value);
      var p2 = getValue(reAnd.Match(l).Groups[2].Value);
      if (p1 == -1 || p2 == -1)
      {
        i++;
      }
      else
      {
        setValue(d, p1 & p2);
        file.RemoveAt(i);
      }
      if (i >= file.Count) i = 0;
      continue;
    }
    if (line.Contains("OR"))
    {
      Regex reOr = new(@"(.+) OR (.+)");
      var p1 = getValue(reOr.Match(l).Groups[1].Value);
      var p2 = getValue(reOr.Match(l).Groups[2].Value);
      if (p1 == -1 || p2 == -1)
      {
        i++;
      }
      else
      {
        setValue(d, p1 | p2);
        file.RemoveAt(i);
      }
      
      if (i >= file.Count) i = 0;
      continue;
    }
    if (line.Contains("NOT"))
    {
      Regex reNot = new(@"NOT (.+)");
      var p1 = getValue(reNot.Match(l).Groups[1].Value);
      if (p1 == -1)
      {
        i++;
      }
      else
      {
        setValue(d, 65536 + ~p1);
        file.RemoveAt(i);
      }
      if (i >= file.Count) i = 0;
      continue;
    }
    if (line.Contains("LSHIFT"))
    {
      Regex reLShift = new(@"(.+) LSHIFT (.+)");
      var p1 = getValue(reLShift.Match(l).Groups[1].Value);
      var p2 = getValue(reLShift.Match(l).Groups[2].Value);
      if (p1 == -1 || p2 == -1)
      {
        i++;
      }
      else
      {
        setValue(d, p1 << p2);
        file.RemoveAt(i);
      }
      if (i >= file.Count) i = 0;
      continue;
    }
    if (line.Contains("RSHIFT"))
    {
      Regex reRShift = new(@"(.+) RSHIFT (.+)");
      var p1 = getValue(reRShift.Match(l).Groups[1].Value);
      var p2 = getValue(reRShift.Match(l).Groups[2].Value);
      if (p1 == -1 || p2 == -1)
      {
        i++;
      }
      else
      {
        setValue(d, p1 >> p2);
        file.RemoveAt(i);
      }
      if (i >= file.Count) i = 0;
      
      continue;
    }
    var p = getValue(l);
    if (p == -1)
    {
      i++;
    }
    else
    {
      setValue(d, p);
      file.RemoveAt(i);
    }
    if (i >= file.Count) i = 0;

  }
  
  ans = reg["a"];
  
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  int i = 0;
  file = [.. file2];
  reg.Clear();
  while (i < file.Count)
  {
    string line = file[i];
    string d = dest.Match(line).Groups[1].Value;
    string l = left.Match(line).Groups[1].Value;
    int val = 0;
    if (line.Contains("AND"))
    {
      Regex reAnd = new(@"(.+) AND (.+)");
      var p1 = getValue(reAnd.Match(l).Groups[1].Value);
      var p2 = getValue(reAnd.Match(l).Groups[2].Value);
      if (p1 == -1 || p2 == -1)
      {
        i++;
      }
      else
      {
        setValue(d, p1 & p2);
        file.RemoveAt(i);
      }
      if (i >= file.Count) i = 0;
      continue;
    }
    if (line.Contains("OR"))
    {
      Regex reOr = new(@"(.+) OR (.+)");
      var p1 = getValue(reOr.Match(l).Groups[1].Value);
      var p2 = getValue(reOr.Match(l).Groups[2].Value);
      if (p1 == -1 || p2 == -1)
      {
        i++;
      }
      else
      {
        setValue(d, p1 | p2);
        file.RemoveAt(i);
      }
      
      if (i >= file.Count) i = 0;
      continue;
    }
    if (line.Contains("NOT"))
    {
      Regex reNot = new(@"NOT (.+)");
      var p1 = getValue(reNot.Match(l).Groups[1].Value);
      if (p1 == -1)
      {
        i++;
      }
      else
      {
        setValue(d, 65536 + ~p1);
        file.RemoveAt(i);
      }
      if (i >= file.Count) i = 0;
      continue;
    }
    if (line.Contains("LSHIFT"))
    {
      Regex reLShift = new(@"(.+) LSHIFT (.+)");
      var p1 = getValue(reLShift.Match(l).Groups[1].Value);
      var p2 = getValue(reLShift.Match(l).Groups[2].Value);
      if (p1 == -1 || p2 == -1)
      {
        i++;
      }
      else
      {
        setValue(d, p1 << p2);
        file.RemoveAt(i);
      }
      if (i >= file.Count) i = 0;
      continue;
    }
    if (line.Contains("RSHIFT"))
    {
      Regex reRShift = new(@"(.+) RSHIFT (.+)");
      var p1 = getValue(reRShift.Match(l).Groups[1].Value);
      var p2 = getValue(reRShift.Match(l).Groups[2].Value);
      if (p1 == -1 || p2 == -1)
      {
        i++;
      }
      else
      {
        setValue(d, p1 >> p2);
        file.RemoveAt(i);
      }
      if (i >= file.Count) i = 0;
      
      continue;
    }
    var p = getValue(l);
    if (p == -1)
    {
      i++;
    }
    else
    {
      setValue(d, p);
      file.RemoveAt(i);
    }
    if (i >= file.Count) i = 0;

  }
  
  ans = reg["a"];
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
