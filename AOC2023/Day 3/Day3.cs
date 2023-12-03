using System.Globalization;
using System.Text.RegularExpressions;

namespace AOC2023;
public class Day3
{
   private struct Part {
      public int row;
      public int col;
      public int val;
      public string sym;
   }
   public void run() {
      string[] raw = System.IO.File.ReadAllLines(@"C:\code\AdventOfCode2023\AOC2023\Day 3\input.txt");
      List<string> lines = raw.ToList();

      List<Part> nums = new List<Part>();
      List<Part> puncts = new List<Part>();
      int sum = 0, sum2 = 0;
      int row = 0;
      foreach (string line in lines) {
         var matches = Regex.Matches(line, @"\d+");
         
         foreach(Match match in matches) {
            nums.Add(new Part { row = row, col = match.Index, val = int.Parse(match.Value) });
         }

         matches = Regex.Matches(line, @"[^\d\.]");
         foreach(Match match in matches) {
            puncts.Add(new Part { row = row, col = match.Index, sym = match.Value });
         }

         row++;
      }

      foreach(Part num in nums) {
         if (puncts.Any(p => p.row >= num.row-1 && p.row <= num.row+1 && p.col >= num.col-1 && p.col <= num.col+num.val.ToString().Length)) {
            //Console.Write(num.val + " ");
            sum += num.val;
         }
      }

      foreach(Part punct in puncts.Where(p => p.sym == "*")) {
         Part[] gears = nums.Where(num => punct.row >= num.row - 1 && punct.row <= num.row + 1 && punct.col >= num.col - 1 && punct.col <= num.col + num.val.ToString().Length).ToArray();
         if (gears.Length == 2) {
            sum2 += gears[0].val * gears[1].val;
         }
      }

      //Console.WriteLine();
      Console.WriteLine(sum);
      Console.WriteLine(sum2);
   }
}
