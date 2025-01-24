using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Runtime.InteropServices.Marshalling;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}/git/aoc2015/12/data.txt");
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
  int ans = 0;

void PrintJsonElement(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                bool red = false; // Part2
                foreach (var property in element.EnumerateObject())
                {
                  red = red || (property.Value.ToString() == "red");
                }
                if (!red)
                foreach (var property in element.EnumerateObject())
                {
                    
                    PrintJsonElement(property.Value);
                }
                
                break;

            case JsonValueKind.Array:
                
                foreach (var item in element.EnumerateArray())
                {
                    PrintJsonElement(item);
                }
                
                break;

            case JsonValueKind.String:
                
                break;

            case JsonValueKind.Number:
                
                ans += (int)element.GetDecimal();
                break;

            case JsonValueKind.True:
            case JsonValueKind.False:
                
                break;

            case JsonValueKind.Null:
                
                break;

            default:
                throw new NotSupportedException("Unknown JSON value kind.");
        }
    }

void part1()
{
  using JsonDocument doc = JsonDocument.Parse(file);
        JsonElement root = doc.RootElement;

        
        PrintJsonElement(root);
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
