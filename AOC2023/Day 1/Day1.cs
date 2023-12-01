using System.ComponentModel;
using System.Globalization;

namespace AOC2023;
public class Day1
{
   private string[] nums = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

   public void run() {
      string[] raw = System.IO.File.ReadAllLines(@"C:\code\AdventOfCode2023\AOC2023\Day 1\input.txt");

      //List<string> lines = raw.Take(10).ToList();
      List<string> lines = raw.ToList();

      int sum = 0;

      foreach(string line in lines) {
         int first = -1, second = -1;

         for(int i = 0; i < line.Length; i++) {
            if (Char.IsNumber(line[i])) {
               first = (int)char.GetNumericValue(line[i]);
               break;
            }
         }

         for(int j = line.Length-1; j >= 0; j--) {
            if (Char.IsNumber(line[j])) {
               second = (int)char.GetNumericValue(line[j]);
               break;
            }
         }

         //Console.WriteLine("1st: " + first + ", 2nd: " + second);
         sum += (first * 10) + second;
      }

      Console.WriteLine("Sum: " + sum);
   }

   public void run2() {
      string[] raw = System.IO.File.ReadAllLines(@"C:\code\AdventOfCode2023\AOC2023\Day 1\input.txt");

      //List<string> lines = raw.Take(10).ToList();
      List<string> lines = raw.ToList();

      int sum = 0;

      foreach(string line in lines) {
         sum += (Int32.Parse(recurse(line)) * 10) + Int32.Parse(recurseBack(line));
         //Console.Write("val: " + recurse(line));
         //Console.WriteLine(" " + recurseBack(line));
      }

      Console.WriteLine(sum);
   }

   private string recurse(string line) {
      for(int i = 0; i < nums.Length; i++) {
         if (line.StartsWith(nums[i])) {
            return (i+1).ToString();
         }
      }

      if (char.IsNumber(line[0])) {
         return line[0].ToString();
      }

      if (line.Length <= 1) {
         return "";
      }

      return recurse(line.Substring(1));
   }

   private string recurseBack(string line) {
      //Console.WriteLine(" " + line);

      for (int i = 0; i < nums.Length; i++) {
         if (line.EndsWith(nums[i])) {
            return (i + 1).ToString();
         }
      }

      if (char.IsNumber(line[line.Length - 1])) {
         return line[line.Length - 1].ToString();
      }

      if (line.Length <= 1) {
         return "";
      }

      return recurseBack(line.Substring(0, line.Length - 1));
   }
}
