using System.Text.RegularExpressions;

using System.Runtime.InteropServices;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/02/test.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
//l x w x h
void part1()
{
  Regex re = new(@"\d+");
  int ans = 0;
  file.ForEach(line =>
  {
    MatchCollection d = re.Matches(line);
    var (l, w, h) = (int.Parse(d[0].Value), int.Parse(d[1].Value), int.Parse(d[2].Value));
    ans += (2 * l * w + 2 * w * h + 2 * h * l);
    List<int> dim = [l * w, w * h, h * l];
    ans += dim.Min();
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
  Regex re = new(@"\d+");
  int ans = 0;
  file.ForEach(line =>
  {
    MatchCollection d = re.Matches(line);
    var (l, w, h) = (int.Parse(d[0].Value), int.Parse(d[1].Value), int.Parse(d[2].Value));
    List<int> di = [l,w,h];
    di.Sort();
    var p1 = di[0]+di[0]+di[1]+di[1];
    var p2 = l*w*h;
    ans += p1+p2;
    
  });
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
