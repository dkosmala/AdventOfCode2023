using System.Text.RegularExpressions;

namespace AOC2023;
public class Day4
{
   private struct cardResult {
      public int cardNum;
      public int wins;
   }
   public void run() {
      string[] raw = System.IO.File.ReadAllLines(@"C:\code\AdventOfCode2023\AOC2023\Day 4\input.txt");
      List<string> lines = raw.ToList();

      int sum = 0, sum2 = 0;
      int[] copies = new int[234];

      int cardNum = 1;
      foreach(string line in lines) {
         string rawWinners = line.Split(":")[1].Split("|")[0];
         string rawPicks = line.Split(":")[1].Split("|")[1];

         var matches = Regex.Matches(rawWinners, @"\d+");
         List<int> winners = matches.Select(m => int.Parse(m.Value)).ToList();

         matches = Regex.Matches(rawPicks, @"\d+");
         List<int> picks = matches.Select(m => int.Parse(m.Value)).ToList();

         int count = winners.Intersect(picks).ToList().Count;
         if (count > 0) {
            sum += (int)Math.Pow(2, count-1);
         }

         for (int j = 0; j < copies[cardNum]+1; j++) {
            sum2++;

            for (int i = 0; i < count; i++) {
               copies[cardNum + i + 1]++;
            }
         }

         cardNum++;
      }

      Console.WriteLine(sum);
      Console.WriteLine(sum2);
   }
}
