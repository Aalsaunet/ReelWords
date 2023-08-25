using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReelWords
{
    public static class ReelsGameManager
    {
        public const string DICT_PATH = "Resources/american-english-large.txt";
        public const string SCORES_PATH = "Resources/scores.txt";
        public const string REELS_PATH = "Resources/reels.txt";
        public const string EXTENDED_REELS_PATH = "Resources/extended-reels.txt";
        
        private static int totalScore = 0;

        static void Main(string[] args)
        {
            DisplayIntroText();
            LoadResourcesFromFile();

            while (true)
            {
                // Get and display the current reel letters
                Letter[] usableLetters = ReelsManager.Instance.GetCurrentLetters();
                Console.Out.WriteLine("> " + FormatLettersForOutput(usableLetters));

                // Get answer from user
                string userInput = Console.ReadLine();
                if (userInput.Length >= 2 && userInput[0] == ':' && userInput[1] == 'q')
                    break; // Terminates the game and the program execution

                // Check if the submitted letters are all from reels and valid
                char[] inputLetters = userInput.ToCharArray();
                List<int> indexMatches = FindIndexMatches(usableLetters, inputLetters);
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

        public static string FormatLettersForOutput(Letter[] usableLetters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var letter in usableLetters)
                sb.Append(letter.letterValue);
            return sb.ToString();
        }

        private static List<int> FindIndexMatches(Letter[] usableLetters, char[] inputLetters)
        {
            Array.Sort(inputLetters);
            Array.Sort(usableLetters, (x, y) => x.letterValue.CompareTo(y.letterValue));
            List<int> indexMatches = new List<int>();
            int i = 0, j = 0;

            while (i < usableLetters.Length && j < inputLetters.Length) {
                if (usableLetters[i].letterValue == inputLetters[j]) {
                    indexMatches.Add(usableLetters[i].reelIndex);
                    j++;
                }
                i++;
            }
            return indexMatches;
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
            Console.Out.WriteLine("\n...And if you aren't having fun, type ':q' to quit (although I can't see why you would ^^).");
            Console.Out.WriteLine("\nHere are your first set of letters:");
        }

        private static void LoadResourcesFromFile()
        {
            var dictLines = File.ReadLines(DICT_PATH);
            foreach (var line in dictLines)
                Trie.Instance.Insert(line);

            var scoreLines = File.ReadLines(SCORES_PATH);
            foreach (var line in scoreLines)
                ReelsManager.Instance.InsertLetterScore(line);

            var reelLines = File.ReadLines(REELS_PATH);
            foreach (var line in reelLines)
                ReelsManager.Instance.InsertReel(line);    
        }
    }
}
