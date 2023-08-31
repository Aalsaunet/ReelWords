using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Threading;

namespace ReelWords
{
    public static class ReelsGameManager
    {
        public const string DICT_PATH = "Resources/american-english-large.txt";
        public const string SCORES_PATH = "Resources/scores.txt";
        public const string REELS_PATH = "Resources/reels.txt";
        public const string EXTENDED_REELS_PATH = "Resources/extended-reels.txt";
        public const int THREAD_COUNT = 3;

        private static int totalScore = 0;

        static void Main(string[] args)
        {
            DisplayIntroText();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            LoadResourcesFromFile();
            stopwatch.Stop();
            // Single threaded setup time: 221ms, multithreaded/dataflow: 107ms 
            Console.Out.WriteLine("[Load time: " + stopwatch.ElapsedMilliseconds + "ms]");
            Console.Out.WriteLine("\nHere are your first set of letters:");

            while (true)
            {
                // Get and display the current reel letters
                Letter[] usableLetters = ReelsManager.Instance.GetCurrentLetters();
                Console.Out.WriteLine("> " + FormatLettersForOutput(usableLetters));

                // Get answer from user
                string userInput = Console.ReadLine();

                // Handle command actions
                if (userInput.Length >= 2 && userInput[0] == ':')
                {
                    switch (userInput[1])
                    {
                        case 'q':
                            Environment.Exit(0);
                            break;
                        case 'h':
                            Console.Out.WriteLine("> " + WordMatcher.GenerateHint(usableLetters));
                            break;
                        case 's':
                            Console.Out.WriteLine("> " + WordMatcher.GenerateHint(usableLetters, true));
                            break;
                    }
                    continue;
                }

                // Check if the submitted letters are all from reels and valid
                char[] inputLetters = userInput.ToLower().ToCharArray();
                List<int> indexMatches = WordMatcher.FindIndexMatches(usableLetters, inputLetters);
                bool allLettersUsable = indexMatches.Count == inputLetters.Length;

                if (!allLettersUsable)
                {
                    Console.Out.WriteLine("[!] Only use the provided letters to form a word!");
                    continue;
                }

                if (!Trie.Instance.Search(userInput))
                {
                    Console.Out.WriteLine("[!] That's not an accepted word!");
                    continue;
                }

                // Word is valid, calculate points gained and total score
                int pointsGained = ReelsManager.Instance.calculatePointsGained(indexMatches);
                totalScore += pointsGained;
                Console.Out.WriteLine("Nice work! You gained " + pointsGained + " points. " +
                    "Your total score is now " + totalScore + ".");

                ReelsManager.Instance.IncrementIndices(indexMatches);
            }
        }

        private static void DisplayIntroText()
        {
            Console.Out.WriteLine("Welcome to ReelWords!");
            Console.Out.WriteLine("In this game you score points by combining " +
                "(some of) the letters displayed on the screen into words - kind of like Scrabble!");
            Console.Out.WriteLine("When doing this you score points based on the combined point " +
                "values of the letters you use, and when used those letter will be replaced by new ones.");
            Console.Out.WriteLine("Try to see how many total points you can score before running out of " +
                "word ideas. But most of all: Have a ton of fun! :D");
            Console.Out.WriteLine("\n...And if you aren't having fun, type ':h' for a hint, ':s' " +
                "to see some words you could submit or - in the worst case - ':q' to quit (although I can't see why you would^^).");
        }


        static void Produce(ITargetBlock<string> target, string fromPath)
        {
            foreach (var line in File.ReadLines(fromPath))
                target.Post(line);
            target.Complete();
        }

        static async void ConsumeAsync(ISourceBlock<string> source, Action<string> handler)
        {
            while (await source.OutputAvailableAsync())
            {
                string line = await source.ReceiveAsync();
                handler(line.ToLower());
            }
        }

        private static void LoadResourceAsync(string fromPath, Action<string> handler) {
            var buffer = new BufferBlock<string>();
            ConsumeAsync(buffer, handler);
            Produce(buffer, fromPath);
        }

        private static void LoadResourcesFromFile()
        {
            var threads = new Thread[THREAD_COUNT];
            threads[0] = new Thread(() => LoadResourceAsync(DICT_PATH, Trie.Instance.Insert));
            threads[1] = new Thread(() => LoadResourceAsync(SCORES_PATH, ReelsManager.Instance.InsertLetterScore));
            threads[2] = new Thread(() => LoadResourceAsync(REELS_PATH, ReelsManager.Instance.InsertReel));

            foreach (var t in threads)
                t.Start();
            foreach (var t in threads)
                t.Join();
        }

        public static string FormatLettersForOutput(Letter[] usableLetters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var letter in usableLetters)
                sb.Append(letter.letterValue);
            return sb.ToString();
        }
    }
}
