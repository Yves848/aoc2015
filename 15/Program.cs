using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security.Principal;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/15/test.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";
Regex reIngredient = new(@"(\w+):|.*?(-?\d+)");
Dictionary<string, List<int>> ingredients = [];
file.ForEach(line =>
{
  var m = reIngredient.Matches(line);
  string name = m[0].Groups[1].Value;
  int capacity = int.Parse(m[1].Groups[2].Value);
  int durability = int.Parse(m[2].Groups[2].Value);
  int flavor = int.Parse(m[3].Groups[2].Value);
  int texture = int.Parse(m[4].Groups[2].Value);
  int calories = int.Parse(m[5].Groups[2].Value);
  ingredients.Add(name, new List<int> { capacity, durability, flavor, texture, calories });
});


void GenerateCombinations(int[] current, int index, int remainingSum, List<int[]> combinations)
{
  if (index == current.Length)
  {
    if (remainingSum == 0)
    {
      bool valid = true;
      current.ToList().ForEach(i =>
      {
        valid = valid && (i != 0);
      });
      if (valid)
        combinations.Add((int[])current.Clone());
    }
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
  combinations.ForEach(c =>
  {
    int temp = 1;
    for (int p = 0; p < 4; p++)
    {
      int prop = 0;
      for (int i = 0; i < ingredients.Count; i++)
      {
        int val = ingredients.ToList()[i].Value[p];
        int qt = c[i];
        int tot = val * qt;// > 0 ? val * qt : 0;
        prop += tot;
      }
      if (prop < 0) prop = 0;
      temp *= prop;
    }
    if (temp > ans) ans = temp;
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  var combinations = new List<int[]>();
  GenerateCombinations(new int[ingredients.Count], 0, 100, combinations);
  combinations.ForEach(c =>
  {
    int temp = 1;
    for (int p = 0; p < 5; p++)
    {
      int prop = 0;
      int calories = 0;
      for (int i = 0; i < ingredients.Count; i++)
      {
        int val = ingredients.ToList()[i].Value[p];
        int qt = c[i];
        int tot = val * qt;// > 0 ? val * qt : 0;
        if (p < 4)
        {
          prop += tot;
        }
        else
        {
          calories += tot;
        }
      }
      if (prop < 0) prop = 0;
      if (calories == 500) {
       if (temp > ans) ans = temp;
      }
      temp *= prop;
    }

  });
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
