using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace AOC2023;
public class Day5
{
   private struct Mapping {
      public long source;
      public long dest;
      public long length;
   }
   public void run() {
      string[] raw = System.IO.File.ReadAllLines(@"C:\code\AdventOfCode2023\AOC2023\Day 5\input.txt");
      List<string> lines = raw.ToList();

      List<long> seeds = lines[0].Split(": ")[1].Split(" ").Select(s => long.Parse(s)).ToList();
      List<Mapping> mappings = new List<Mapping>();
      List<List<Mapping>> groups = new List<List<Mapping>>();

      long min = -1;

      foreach(string line in lines.Skip(2)) {
         if (Regex.IsMatch(line, @"[a-z]")) {
            mappings = new List<Mapping>();
            groups.Add(mappings);
         } else if (Regex.IsMatch(line, @"\d")) {
            string[] nums = line.Split(" ");
            mappings.Add(new Mapping() { source = long.Parse(nums[1]), dest = long.Parse(nums[0]), length = long.Parse(nums[2]) });
         }
      }

      foreach (long origSeed in seeds) {
         long seed = origSeed;
         foreach (List<Mapping> group in groups) {
            foreach(Mapping map in group) {
               if (seed >= map.source && seed < map.source+map.length) {
                  //Console.Write(seed + " -> (" + map.dest + " " + map.source + " " + map.length);
                  seed += map.dest - map.source;
                  //Console.WriteLine(") -> " + seed);
                  break;
               }
            }
         }
         //Console.WriteLine();
         min = (min == -1 || seed < min) ? seed : min;
      }
      Console.WriteLine(min);
   }

   public void run2() {
      string[] raw = System.IO.File.ReadAllLines(@"C:\code\AdventOfCode2023\AOC2023\Day 5\input.txt");
      List<string> lines = raw.ToList();

      List<Mapping> seedGroups = new List<Mapping>();
      var matches = Regex.Matches(lines[0].Split(": ")[1], @"(\d+)\w.(\d+)");
      foreach (Match match in matches) {
         seedGroups.Add(new Mapping { source = long.Parse(match.Value.Split(" ")[0]), length = long.Parse(match.Value.Split(" ")[1])});
      }

      List<Mapping> mappings = new List<Mapping>();
      List<List<Mapping>> groups = new List<List<Mapping>>();

      foreach (string line in lines.Skip(2))
      {
         if (Regex.IsMatch(line, @"[a-z]"))
         {
            mappings = new List<Mapping>();
            groups.Add(mappings);
         }
         else if (Regex.IsMatch(line, @"\d"))
         {
            string[] nums = line.Split(" ");
            mappings.Add(new Mapping() { source = long.Parse(nums[1]), dest = long.Parse(nums[0]), length = long.Parse(nums[2]) });
         }
      }

      groups.Reverse();
      
      for(long i = 0; i <= 9999999999; i++) {
         long seed = i;

         foreach (List<Mapping> group in groups) {
            foreach(Mapping mapping in group) {
               if (seed >= mapping.dest && seed < mapping.dest + mapping.length) {
                  //Console.Write(seed + " -> (" + mapping.dest + " " + mapping.source + " " + mapping.length);
                  seed += mapping.source - mapping.dest;
                  //Console.WriteLine(") -> " + seed);
                  break;
               }
            } 
         }
         if (seedGroups.Any(g => seed >= g.source && seed < g.source+g.length)) {
            Console.WriteLine(seed + " " + i);
            return;
         }

         //Console.WriteLine();
      }    
   }
}
