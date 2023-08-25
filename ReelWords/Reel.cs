using System;
using System.Collections.Generic;

namespace ReelWords
{
	public class Reel
	{
        public bool randomizeIndices = true;
        private static Reel instance = null;
        private Dictionary<char, int> letterScores = null;
        private List<Letter[]> reelsLetters;
        private List<int> reelsIndices;

        private Reel() {
            letterScores = new Dictionary<char, int>();
            reelsLetters = new List<Letter[]>();
            reelsIndices = new List<int>();
        }

        public static Reel Instance
        {
            get
            {
                if (instance == null)
                    instance = new Reel();
                return instance;
            }
        }

        public void InsertReel(string s)
        {
            string[] entries = s.Split();
            Letter[] letters = new Letter[entries.Length];

            for (int i = 0; i < letters.Length; i++) {
                if (char.IsLetter(entries[i][0]))
                    letters[i] = new Letter(entries[i][0], reelsLetters.Count, letterScores[entries[i][0]]);
            }

            reelsLetters.Add(letters);

            int initialIndex = randomizeIndices ? new Random().Next(0, letters.Length) : 0;
            reelsIndices.Add(initialIndex);
        }

        public void InsertLetterScore(string s)
        {
            string[] letterAndScore = s.Split();
            letterScores[char.Parse(letterAndScore[0])] = Int32.Parse(letterAndScore[1]);
        }

        public Letter[] GetCurrentLetters() {
            Letter[] letters = new Letter[reelsLetters.Count];
            for (int i = 0; i < letters.Length; i++)
                letters[i] = reelsLetters[i][reelsIndices[i]];
            return letters;
        }

        public void IncrementIndices(List<int> indices)
        {
            foreach (var index in indices) {
                reelsIndices[index] = (reelsIndices[index] + 1)
                    % reelsLetters[index].Length;
            }
        }

        public int calculatePointsGained(List<int> indices)
        {
            int sum = 0;
            foreach (var index in indices)
                sum += reelsLetters[index][reelsIndices[index]].pointValue;
            return sum;
        }
    }
}

