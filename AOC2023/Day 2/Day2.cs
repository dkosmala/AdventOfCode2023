using System.Text.RegularExpressions;

namespace AOC2023;
public class Day2
{
   public void run() {
      string[] raw = System.IO.File.ReadAllLines(@"C:\code\AdventOfCode2023\AOC2023\Day 2\input.txt");
      List<string> lines = raw.ToList();

      int sum = 0, sum2 = 0;
      int redMax = 12, greenMax = 13, blueMax = 14;
      int redMax2, greenMax2, blueMax2;

      foreach(string line in lines) {
         string rxRed = @"(\d+) (red|blue|green)";
         var matches = Regex.Matches(line, rxRed);

         redMax2 = matches.Where(m => m.Groups[2].Value == "red").Max(m => int.Parse(m.Groups[1].Value));
         greenMax2 = matches.Where(m => m.Groups[2].Value == "green").Max(m => int.Parse(m.Groups[1].Value));
         blueMax2 = matches.Where(m => m.Groups[2].Value == "blue").Max(m => int.Parse(m.Groups[1].Value));

         sum2 += redMax2 * greenMax2 *  blueMax2;

         if (matches.Any(m => (m.Groups[2].Value == "red" && int.Parse(m.Groups[1].Value) > redMax) || 
            (m.Groups[2].Value == "green" && int.Parse(m.Groups[1].Value) > greenMax) || 
            (m.Groups[2].Value == "blue" && int.Parse(m.Groups[1].Value) > blueMax))) {
            continue;
         }

         sum += int.Parse(line.Split(":")[0].Split(" ")[1]);
      }

      Console.WriteLine("Q1: " + sum);
      Console.WriteLine("Q2: " + sum2);
   }
}


