using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/08/test.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";

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
  int code = 0;
  int str = 0;
  Regex c = new(@"\\x([a-f|\d]{2})");
  file.ForEach(line => {
    line = line.Substring(1);
    line = line.Substring(0,line.Length-1);
    int l1 = @line.Length +2;
    code += l1;
    line = Regex.Unescape(line);
    string temp = @line.Replace("\\"," ").Replace("\""," ");
    int l2 = c.Replace(temp," ").Length;
    str += l2;
  });
  ans = code-str;
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  int code = 0;
  int str = 0;
  Regex c = new(@"\\x([a-f|\d]{2})");
  file.ForEach(line => {
    string line2 = line;
    line = line.Substring(1);
    line = line.Substring(0,line.Length-1);
    line2 =  line;
    int l1 = @line.Length +2;
    code += l1;
    line2 = line2.Replace("\\\"","\\\\\\\"");
    line2 = "\""+line2+"\"";
    var m = c.Match(line2);
    if (m.Success) {
      line2 = c.Replace(line2,"\\\""+m.Value+"\"");
    }
    
    int l2 = line2.Length+4;
    Console.WriteLine($"{line2} {l2}");
    str += l2;
  });
  ans = str-code;
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
