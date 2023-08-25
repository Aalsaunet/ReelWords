﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReelWords
{
    public static class Program
    {
        public const string DICT_PATH = "Resources/american-english-large.txt";
        public const string SCORES_PATH = "Resources/scores.txt";
        public const string REELS_PATH = "Resources/reels.txt";
        public const string EXTENDED_REELS_PATH = "Resources/extended-reels.txt";
        
        private static int totalScore = 0;

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
                Letter[] usableLetters = reel.GetCurrentLetters();
                StringBuilder sb = new StringBuilder();

                foreach (var letter in usableLetters)
                    sb.Append(letter.displayLetter);

                string displayLetters = new string(sb.ToString());
                Console.Out.WriteLine(displayLetters);

                string input = Console.ReadLine();
                char[] inputLetters = input.ToCharArray();

                List<int> indexMatches = findIndexMatches(usableLetters, inputLetters);
                bool isValidInput = indexMatches.Count == inputLetters.Length;

                if (!isValidInput) {
                    Console.Out.WriteLine("[X] Only use the provided letters to form a word!");
                    continue;
                }

                if (!trie.Search(input)) {
                    Console.Out.WriteLine("[X] That's not an accepted word!");
                    continue;
                }
    
                int pointsGained = reel.calculatePointsGained(indexMatches);
                totalScore += pointsGained;
                Console.Out.WriteLine("Nice work! You gained " + pointsGained + " points. " +
                    "Your total score is now " + totalScore + ".");

                reel.IncrementIndices(indexMatches);
            }
        }

        private static List<int> findIndexMatches(Letter[] usableLetters, char[] inputLetters)
        {
            Array.Sort(inputLetters);
            Array.Sort(usableLetters, (x, y) => x.displayLetter.CompareTo(y.displayLetter));
            List<int> indexMatches = new List<int>();
            int i = 0, j = 0;

            while (i < usableLetters.Length && j < inputLetters.Length) {
                if (usableLetters[i].displayLetter == inputLetters[j]) {
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
        }

        private static void LoadResources()
        {
            // Ingest word dictionary from file and store in the trie
            var dictLines = File.ReadLines(DICT_PATH);
            foreach (var line in dictLines)
            {
                Trie.Instance.Insert(line);
            }

            // Ingest the scores
            var scoreLines = File.ReadLines(SCORES_PATH);
            foreach (var line in scoreLines)
            {
                Reel.Instance.InsertLetterScore(line);
            }

            // Ingest the reels
            var reelLines = File.ReadLines(REELS_PATH);
            foreach (var line in reelLines)
            {
                Reel.Instance.InsertReel(line);
            }      
        }
    }
}
