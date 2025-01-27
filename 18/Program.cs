using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2015/18/test.txt").ToList();
var lf = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\r\n" : "\n";

int w = file.Count;
int h = file.Count;
int[,] grid = new int[h, w];
void initGrid()
{
  for (int y = 0; y < h; y++)
  {
    for (int x = 0; x < w; x++)
    {
      grid[y, x] = file[y][x] == '#' ? 1 : 0;
    }
  }
}

var dir = new List<(int, int)> {
  {(-1,-1)}, // Up Left
  {(-1,0)}, // Up
  {(-1,1)}, // Up Right
  {(0,-1)}, // Left
  {(0,1)}, // Right
  {(1,-1)}, // Down Left
  {(1,0)}, // Down
  {(1,1)} // Down Right
};

int voisins((int, int) pos)
{
  int result = 0;
  var (y, x) = pos;
  dir.ForEach(d =>
  {
    var (dy, dx) = d;
    if (y + dy >= 0 && y + dy < w && x + dx >= 0 && x + dx < h)
    {
      result += grid[y + dy, x + dx];
    }
  });
  return result;
}

void part1()
{
  int ans = 0;
  initGrid();
  for (int n = 0; n < 100; n++)
  {
    int[,] grid2 = new int[h, w];
    for (int y = 0; y < h; y++)
    {
      for (int x = 0; x < w; x++)
      {
        int nb = voisins((y, x));
        grid2[y, x] = grid[y, x];
        if (grid[y, x] == 1 && (nb < 2 || nb > 3))
        {
          grid2[y, x] = 0;
        }
        if (grid[y, x] == 0 && (nb == 3)) grid2[y, x] = 1;
      }
    }

    for (int i = 0; i < h; i++)
    {
      for (int j = 0; j < w; j++)
      {
        int v = grid2[i, j];
        grid[i, j] = v;
      }
    }
  }
  for (int i = 0; i < h; i++)
  {
    for (int j = 0; j < w; j++)
    {
      ans += grid[i, j];
      // Console.Write(grid[i,j]);
    }
    // Console.WriteLine();
  }
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

int printGrid(int[,] g, bool p = true)
{
  int ans = 0;
  for (int i = 0; i < h; i++)
  {
    for (int j = 0; j < w; j++)
    {
      ans += g[i, j];
      if (p)
      Console.Write(g[i, j]);
    }
    if (p)
    Console.WriteLine();
  }
  if(p)
  Console.WriteLine();
  return ans;
}

void part2()
{
  int ans = 0;
  initGrid();
  printGrid(grid,false);
  for (int n = 0; n < 100; n++)
  {
    int[,] grid2 = new int[h, w];
    grid[0, 0] = 1;
    grid[0, w - 1] = 1;
    grid[h - 1, 0] = 1;
    grid[h - 1, w - 1] = 1;
    for (int y = 0; y < h; y++)
    {
      for (int x = 0; x < w; x++)
      {
        int nb = voisins((y, x));
        grid2[y, x] = grid[y, x];
        if (grid[y, x] == 1 && (nb < 2 || nb > 3))
        {
          grid2[y, x] = 0;
        }
        if (grid[y, x] == 0 && (nb == 3)) grid2[y, x] = 1;
      }
    }
    grid2[0,0]=1;
    grid2[0,w-1]=1;
    grid2[h-1,0] =1;
    grid2[h-1,w-1] =1;
    for (int i = 0; i < h; i++)
    {
      for (int j = 0; j < w; j++)
      {
        int v = grid2[i, j];
        grid[i, j] = v;
      }
    }
    ans = printGrid(grid,false);
  }
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
