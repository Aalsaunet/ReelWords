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
                Console.Out.WriteLine(reel.GetCurrentLetters());
                Console.Out.Write("> ");
                string input = Console.ReadLine();

                // TODO:  Run game logic here using the user input string

                // TODO:  Create simple unit tests to test your code in the ReelWordsTests project,
                // don't worry about creating tests for everything, just important functions as
                // seen for the Trie tests
            }
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
