using System.Text.RegularExpressions;

namespace AOC2023;
public class Day6
{
   public void run() {
      string[] raw = System.IO.File.ReadAllLines(@"C:\code\AdventOfCode2023\AOC2023\Day 6\input.txt");
      List<string> lines = raw.ToList();

      List<int> times = Regex.Matches(lines[0], @"\d+").Select(m => int.Parse(m.Value)).ToList();
      List<int> distances = Regex.Matches(lines[1], @"\d+").Select(m => int.Parse(m.Value)).ToList();

      int sum = 1;

      // for(int i = 0; i < times.Count; i++) {
      //    int count = 0;

      //    for(int j = 1; j < times[i]; j++) {
      //       if (j * (times[i] - j) > distances[i]) {
      //          count++;
      //       }
      //    }

      //    sum *= count;
      // }

      // Console.WriteLine(sum);

      sum = 1;

      int count2 = 0;
      long time = 46828479;
      long distance = 347152214061471;

      for (int j = 1; j < time; j++) {
         if (j * (time - j) > distance) {
            count2++;
         }
      }

      sum *= count2;

      Console.WriteLine(sum);
      
   }
}
