using System;
using System.IO;

namespace ReelWords
{
    public static class Program
    {
        public const string DICT_PATH = "Resources/american-english-large.txt";
        public const string REELS_PATH = "Resources/reels.txt";
        public const string SCORES_PATH = "Resources/scores.txt";

        static void Main(string[] args)
        {
            Trie trie = Trie.Instance;
            Reel reel = Reel.Instance;
            bool playing = true;

            DisplayIntroText();
            LoadResources();

            Console.Out.WriteLine("Make words!");

            while (playing)
            {
                char[] usableLetters = reel.GetCurrentLetters();
                string displayLetters = new string(usableLetters);

                Console.Out.WriteLine(displayLetters);

                string input = Console.ReadLine();
                char[] inputLetters = input.ToCharArray();


                if (!isValidInput(usableLetters, inputLetters)) {
                    Console.Out.WriteLine("[X] Only use the provided letters to form a word!");
                    continue;
                }

                if (!trie.Search(input)) {
                    Console.Out.WriteLine("[X] That's not an accepted word!");
                    continue;
                }

                Console.Out.WriteLine("Nice work! You gain X points. The current total is Y.");

                // Increments reels

            }
        }

        private static bool isValidInput(char[] usableLetters, char[] inputLetters)
        {
            Array.Sort(usableLetters); Array.Sort(inputLetters);
            int matches = 0, i = 0, j = 0;

            while (i < usableLetters.Length && j < inputLetters.Length) {
                if (usableLetters[i] == inputLetters[j]) {
                    matches++;
                    j++;
                }
                i++;
            }
            return matches == inputLetters.Length;
        }

        private static void DisplayIntroText()
        {
            Console.Out.WriteLine("Welcome to ReelWords!");
        }

        private static void LoadResources()
        {
            // Ingest word dictionary from file and store in the trie
            var dictLines = File.ReadLines(DICT_PATH);
            foreach (var line in dictLines)
            {
                Trie.Instance.Insert(line);
            }

            // Ingest the reels
            var reelLines = File.ReadLines(REELS_PATH);
            foreach (var line in reelLines)
            {
                Reel.Instance.Insert(line);
            }
        }
    }
}
