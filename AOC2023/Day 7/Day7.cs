using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace AOC2023;
public class Day7
{
   private enum Result {
      card1,
      card2,
      neither,
      samesies
   }

   private struct Hand {
      public string hand;
      public int bid;
   }
   public void run() {
      string[] raw = System.IO.File.ReadAllLines(@"C:\code\AdventOfCode2023\AOC2023\Day 7\input.txt");
      List<string> lines = raw.ToList();

      List<Hand> hands = new List<Hand>();

      foreach(string line in lines) {
         string hand = line.Split(" ")[0];
         int bid = int.Parse(line.Split(" ")[1]);

         if (hands.Count == 0) {
            hands.Add(new Hand() { hand = hand, bid = bid } );
            continue;
         }

         for(int i = 0; i < hands.Count; i++) {
            if (IsCardOneHigher(hand, hands[i].hand)) {
               hands.Insert(i, new Hand() { hand = hand, bid = bid});
               break;
            } else if (i == hands.Count-1) {
               hands.Add(new Hand() { hand = hand, bid = bid }); 
               break;
            }
         }
      }

      long sum = 0;
      for (int j = 0; j < hands.Count; j++) {
         sum += (hands.Count - j) * hands[j].bid;
         Console.WriteLine(hands[j].hand + " " + hands[j].bid + " " + (hands.Count - j) + " " + sum);
      }

      Console.WriteLine(sum);
   }

   private bool IsCardOneHigher(string origCard1, string origCard2) {
      string card1 = string.Concat(origCard1.OrderBy(c => c));
      string card2 = string.Concat(origCard2.OrderBy(c => c));

      Result result = Compare(card1, card2, @"(.)\1\1\1\1"); // fives
      if (result == Result.samesies) result = Highest(origCard1, origCard2);
      if (result == Result.card1) return true;
      if (result == Result.card2) return false;

      result = Compare(card1, card2, @"(.)\1\1\1"); // fours
      if (result == Result.samesies) result = Highest(origCard1, origCard2);
      if (result == Result.card1) return true;
      if (result == Result.card2) return false;

      result = Compare(card1, card2, @"(.)\1\1"); // threes
      if (result == Result.samesies) { // need to check for full house
         result = Compare(card1, card2, @"(.)\1(.)\2");
      }
      if (result == Result.samesies) result = Highest(origCard1, origCard2);
      if (result == Result.card1) return true;
      if (result == Result.card2) return false;

      result = Compare(card1, card2, @"(.)\1.?(.)\2"); // two pair
      if (result == Result.samesies) result = Highest(origCard1, origCard2);
      if (result == Result.card1) return true;
      if (result == Result.card2) return false;

      result = Compare(card1, card2, @"(.)\1"); // one pair
      if (result == Result.samesies) result = Highest(origCard1, origCard2);
      if (result == Result.card1) return true;
      if (result == Result.card2) return false;

      result = Highest(origCard1, origCard2);
      if (result == Result.card1) return true;
      if (result == Result.card2) return false;

      return true;
   }

   private Result Compare(string card1, string card2, string pattern) {
      bool match1 = Regex.IsMatch(card1, pattern);
      bool match2 = Regex.IsMatch(card2, pattern);

      if (match1 && !match2) return Result.card1;
      if (!match1 && match2) return Result.card2;
      if (match1 && match2) return Result.samesies;
      return Result.neither;
   }

   private Result Highest(string card1, string card2) {
      for (int i = 0; i < 5; i++) {
         int val1 = Val(card1[i]);
         int val2 = Val(card2[i]);

         if (val1 > val2) return Result.card1;
         if (val2 > val1) return Result.card2;
      }

      return Result.samesies; // shouldn't happen
   }

   private int Val(char card) => card switch {
      'T' => 10,
      'J' => 11,
      'Q' => 12,
      'K' => 13,
      'A' => 14,
      _ => (int)char.GetNumericValue(card)
   };
}
