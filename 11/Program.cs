
using System.ComponentModel.DataAnnotations;
using System.IO.Pipelines;
using System.Text.RegularExpressions;

string alphabet = "abcdefghijklmnopqrstuvwxyz";
List<string> triple = [];
for (int i = 0; i < alphabet.Length - 2; i++)
{
  triple.Add(alphabet.Substring(i, 3));
}

string puzzle = "hepxcrrq";

string increment(string pwd)
{
  int index = pwd.Length - 1;
  while (true)
  {
    char c = pwd[index];
    c++;
    if (c > 'z')
    {
      pwd = pwd.Remove(index, 1).Insert(index, "a");
      index--;
    }
    else
    {
      if ("iol".Contains(c.ToString())) c++;
      pwd = pwd.Remove(index, 1).Insert(index, c.ToString());
      break;
    }
  }
  return pwd;
}

bool triplet(string pwd)
{
  List<string> t = [];
  for (int i = 0; i < pwd.Length - 2; i++)
  {
    t.Add(pwd.Substring(i, 3));
  }
  return triple.Intersect(t).ToList().Count() > 0;
}

bool pairs(string pwd) {
  Regex re = new(@"(\w)\1");
  var matches = re.Matches(pwd);
  HashSet<string> p = [];
  matches.ToList().ForEach(m=> {
    p.Add(m.Value);
  });
  return p.Count > 1;
}

void part1()
{
  bool correct = false;
  while (!correct)
  {
    puzzle = increment(puzzle);
    correct = triplet(puzzle);
    correct = correct && !puzzle.Contains("i");
    correct = correct && !puzzle.Contains("o");
    correct = correct && !puzzle.Contains("l");
    correct = correct && pairs(puzzle);
  }
      Console.WriteLine(puzzle);
}

void part2()
{
  puzzle = "hepxcrrq";
  bool correct = false;
  int i = 0;
  while (!correct)
  {
    puzzle = increment(puzzle);
    correct = triplet(puzzle);
    correct = correct && !puzzle.Contains("i");
    correct = correct && !puzzle.Contains("o");
    correct = correct && !puzzle.Contains("l");
    correct = correct && pairs(puzzle);
    if (correct && i == 0) {
      correct = false;
      i++;
    }
  }
      Console.WriteLine(puzzle);
}

part1();

part2();

// var u = triple.Intersect(triple2).ToList();

// triple.ForEach(f => Console.WriteLine(f));
