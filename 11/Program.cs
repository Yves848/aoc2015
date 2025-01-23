using System.Security.Cryptography;

string puzzle = "hepxcrrq";

string alphabet = "abcdefghijklmnopqrstuvwxyz";
List<string> triple = [];
for (int i = 0; i < alphabet.Length-2; i++){
  triple.Add(alphabet.Substring(i,3));
}

string test = "dkfjdskmjfsdmlkabcmlsùmdlsùp";

List<string> triple2 = [];
for (int i = 0; i < test.Length-2; i++){
  triple2.Add(test.Substring(i,3));
}

var u = triple.Intersect(triple2);
Console.WriteLine(u);
// triple.ForEach(f => Console.WriteLine(f));
