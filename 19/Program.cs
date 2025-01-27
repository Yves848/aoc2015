using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}/git/aoc2015/19/test.txt");
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
var blocs = file.Split($"{lf}{lf}");

List<(string,string)> substitutions = [];


blocs[0].Split(lf).ToList().ForEach(line => {
  var l = line.Split(" => ");
  substitutions.Add((l[0],l[1]));
});

string molecule = blocs[1];

void part1()
{
  HashSet<string> molecules = [];
  int ans = 0;
  substitutions.ForEach(s => {
    var (from,to) = s;
    Regex re = new(@$"{from}");
    var m = re.Matches(molecule);
    m.ToList().ForEach(m => {
      molecules.Add(molecule.Remove(m.Index,m.Length).Insert(m.Index,to));
    });
  });
  Console.WriteLine($"Part 1 - Answer : {molecules.Count}");
}

void part2()
{
  int ans = 0;
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
