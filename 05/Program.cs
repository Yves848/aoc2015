using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.IO.Pipelines;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/05/data.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
Regex vowels = new(@"[a|e|i|o|u]");
Regex doubles = new(@"([a-z])\1");
Regex dble = new(@"([a-z]{2}).*?\1");
Regex surround = new(@"([a-z]).{1}?\1");

string[] forbidden = ["ab", "cd", "pq", "xy"];
bool isNice(string line)
{
  bool result = true;
  result = result && (vowels.Matches(line).Count >= 3);
  result = result && (doubles.IsMatch(line));
  forbidden.ToList().ForEach(x =>
  {
    result = result && !(line.Contains(x));
  });
  return result;
}

bool isNice2(string line)
{
  bool result = true;
  result = result && dble.IsMatch(line);
  result = result && surround.IsMatch(line);
  return result;
}
void part1()
{
  int ans = 0;
  file.ForEach(line =>
  {
    if (isNice(line))
    {
      ans++;
      Console.WriteLine(line);
    }

  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
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

void part2()
{
  int ans = 0;
  file.ForEach(line =>
  {
    if (isNice2(line))
    {
      ans++;
      Console.WriteLine(line);
    }

  });
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
