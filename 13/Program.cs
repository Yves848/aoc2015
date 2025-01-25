using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Spectre.Console;


string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/13/test.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
HashSet<string> guest = [];
Dictionary<(string, string), int> happy = [];
Regex re = new(@"(\w+) would (\w+) (\d+) .* next to (\w+)");
file.ForEach(line =>
{
  var m = re.Match(line);
  guest.Add(m.Groups[1].Value);
  guest.Add(m.Groups[4].Value);
  int h = int.Parse(m.Groups[3].Value);
  if (m.Groups[2].Value == "lose") h *= -1;
  happy.Add((m.Groups[1].Value, m.Groups[4].Value), h);
  // happy.Add((m.Groups[4].Value, m.Groups[1].Value), h);
});


void part1()
{
  int ans = 0;
  int h = 0;
  int h2 = 0;
  Rule r = new Rule();
  var allGuests = GetPermutations(guest.ToList(), guest.Count);
  foreach (var guest in allGuests)
  {
    int temp = 0;
    for (int i = 0; i < guest.Count - 1; i++)
    {
      int j = i == 0 ? guest.Count - 1 : i - 1;
      var segment = (guest[i], guest[i + 1]);
      h = happy[segment];
      temp += h;
      segment = (guest[i], guest[j]);
      h2 = happy[segment];
      Console.WriteLine($"{guest[j]} {guest[i]} {guest[i + 1]} {h2} {h}");
      temp += h2;
    }
    h = happy[(guest[guest.Count - 1], guest[0])];
    temp += h;
    h = happy[(guest[guest.Count - 1], guest[guest.Count - 2])];
    temp += h;
    // Console.WriteLine($"{guest[guest.Count-1]} {guest[0]} {h}");
    AnsiConsole.Write(r);
    if (temp > ans) ans = temp;
    Console.WriteLine($"Total : {temp}");
  }
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

int happyness(string guest1, string guest2)
{
  int result = 0;
  if (guest1 == "Yves" || guest2 == "Yves") return 0;

  return happy[(guest1, guest2)];
}

void part2()
{
  int ans = 0;
  int h = 0;
  int h2 = 0;
  Rule r = new Rule();
  guest.Add("Yves");
  var allGuests = GetPermutations(guest.ToList(), guest.Count);
  foreach (var guest in allGuests)
  {
    int temp = 0;
    for (int i = 0; i < guest.Count - 1; i++)
    {
      int j = i == 0 ? guest.Count - 1 : i - 1;
      h = happyness(guest[i], guest[i + 1]);
      temp += h;
      h2 = happyness(guest[i], guest[j]);
      Console.WriteLine($"{guest[j]} {guest[i]} {guest[i + 1]} {h2} {h}");
      temp += h2;

    }

    h = happyness(guest[guest.Count - 1], guest[0]);
    temp += h;
    h = happyness(guest[guest.Count - 1], guest[guest.Count - 2]);
    temp += h;

    // Console.WriteLine($"{guest[guest.Count-1]} {guest[0]} {h}");
    AnsiConsole.Write(r);
    if (temp > ans) ans = temp;
    Console.WriteLine($"Part 2 - Answer : {ans}");
  }
}
part1();

part2();



IEnumerable<List<T>> GetPermutations<T>(List<T> list, int length)
{
  if (length == 1)
    return list.Select(t => new List<T> { t });

  return GetPermutations(list, length - 1)
      .SelectMany(t => list.Where(e => !t.Contains(e)),
                  (t1, t2) => t1.Concat(new List<T> { t2 }).ToList());
}