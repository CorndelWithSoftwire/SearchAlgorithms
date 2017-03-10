using System;
using System.Diagnostics;
using System.Net;

namespace SearchAlgorithms
{
  class Program
  {
    static void Main()
    {
      var words = new WebClient()
        .DownloadString(@"https://raw.githubusercontent.com/dwyl/english-words/master/words.txt")
        .Split('\n');

      RunTest(words, LinearSearch);
      RunTest(words, BinarySearch);
      //RunTest(words, (list, target) => Array.IndexOf(list, target));
      //RunTest(words, (list, target) => Array.BinarySearch(list, target));

      Console.ReadLine();
    }

    private static void RunTest(string[] words, Action<string[], string> searchAlgorithm)
    {
      const int numberOfTests = 10000;
      var random = new Random();
      var stopwatch = new Stopwatch();

      for (int i = 0; i < numberOfTests; i++)
      {
        var randomTarget = words[random.Next(0, words.Length)];

        stopwatch.Start();
        searchAlgorithm(words, randomTarget);
        stopwatch.Stop();
      }

      Console.WriteLine($"{searchAlgorithm.Method.Name}, total time: {stopwatch.ElapsedMilliseconds}ms");
    }

    private static void LinearSearch(string[] words, string target)
    {
      foreach (string word in words)
      {
        if (word == target) return;
      }
    }

    private static void BinarySearch(string[] words, string target)
    {
      int lowerBound = 0;
      int upperBound = words.Length - 1;
      int middle = upperBound / 2;

      while (words[middle] != target)
      {
        if (String.Compare(words[middle], target, StringComparison.Ordinal) > 0)
        {
          upperBound = middle - 1;
        }
        else
        {
          lowerBound = middle + 1;
        }

        middle = lowerBound + (upperBound - lowerBound) / 2;
      }
    }

  }
}
