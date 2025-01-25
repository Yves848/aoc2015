using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/15/data.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
Regex reIngredient = new(@"^(\w+)");
List<string> ingredients = [];
file.ForEach(line =>
{
  ingredients.Add(reIngredient.Match(line).Value);
});


void GenerateCombinations(int[] current, int index, int remainingSum, List<int[]> combinations)
{
  if (index == current.Length)
  {
    if (remainingSum == 0)
      combinations.Add((int[])current.Clone());
    return;
  }

  for (int i = 0; i <= remainingSum; i++)
  {
    current[index] = i;
    GenerateCombinations(current, index + 1, remainingSum - i, combinations);
  }
}

void part1()
{
  int ans = 0;
  var combinations = new List<int[]>();

  GenerateCombinations(new int[ingredients.Count], 0, 100, combinations);
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
