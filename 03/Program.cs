using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}/git/aoc2015/03/test.txt");
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
Dictionary<char, (int, int)> dir = new Dictionary<char, (int, int)>
{
  {'<',(0,-1)},
  {'^',(-1,0)},
  {'v',(1,0)},
  {'>',(0,1)}
};



// file = "^v^v^v^v^v";
void solve(bool p2)
{
  Dictionary<(int, int), int> houses = new Dictionary<(int, int), int> { { (0, 0), 2 } };
  int y = 0;
  int x = 0;
  int y2 = 0;
  int x2 = 0;
  int i = 0;
  while (i < file.Length)
  {
    char d = file[i];
    var (dy, dx) = dir[d];
    if (p2)
    {
      i++;
      d = file[i];
      var (dy2, dx2) = dir[d];
      y2 += dy2;
      x2 += dx2;
      if (!houses.ContainsKey((y2, x2))) houses.Add((y2, x2), 0);
      houses[(y2, x2)] = houses[(y2, x2)] + 1;
    }
    y += dy;
    x += dx;
    if (!houses.ContainsKey((y, x))) houses.Add((y, x), 0);
    houses[(y, x)] = houses[(y, x)] + 1;
    i++;
  }
  ;

  int ans = houses.Count;
  Console.WriteLine($"Part {(p2 ? "2" : "1")} - Answer : {ans}");
}

solve(false);
solve(true);
