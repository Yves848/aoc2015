using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

string CalculateMD5Hash(string input)
{
  // Create an MD5 object
  using (MD5 md5 = MD5.Create())
  {
    // Compute the hash as a byte array
    byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

    // Convert the byte array to a hexadecimal string
    StringBuilder sb = new StringBuilder();
    foreach (byte b in hashBytes)
    {
      sb.Append(b.ToString("x2")); // Format as a two-digit hexadecimal number
    }

    return sb.ToString();
  }
}

string puzzle = "yzbqklnj";
// string puzzle = "abcdef";
// string puzzle = "pqrstuv";

void solve() {
  var i = 1;
  while(true) {
    var result = CalculateMD5Hash(puzzle+i.ToString());
    if (result.StartsWith("000000")) break;
    i++;
  }
  Console.WriteLine($"part 1 : {i}");
}

solve();