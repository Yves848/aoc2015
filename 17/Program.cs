using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/17/data.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";

List<int> numbers = file.Select(p => int.Parse(p)).ToList();
numbers.Sort();

void FindCombinations(int[] numbers, int target, List<int> current, List<List<int>> results, int start = 0)
{
  // Debug output
  Console.WriteLine($"Current combination: {string.Join(", ", current)} | Remaining target: {target}");

  if (target == 0)
  {
    results.Add(new List<int>(current));
    return;
  }

  for (int i = start; i < numbers.Length; i++)
  {
    if (numbers[i] > target)
      continue;

    current.Add(numbers[i]);
    FindCombinations(numbers, target - numbers[i], current, results, i + 1); // No repetition allowed
    current.RemoveAt(current.Count - 1); // Backtrack
  }
}


void part1()
{
  int ans = 0;
  List<List<int>> results = [];
  FindCombinations(numbers.ToArray(), 150, new List<int>(), results, 0);
  ans = results.Count;
  Console.WriteLine($"Part 1 - Answer : {ans}");
  Dictionary<int,int> dist = [];
  results.ForEach(r => {
    int nb = r.Count;
    if (!dist.ContainsKey(nb)) dist.Add(nb,0);
    dist[nb] = dist[nb] +1;
  });
  List<int> temp = dist.ToList().Select(p => p.Key).ToList();
  temp.Sort();
  ans = dist[temp[0]];
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
}

part1();

part2();
