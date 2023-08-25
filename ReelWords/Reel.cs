using System;
using System.Collections.Generic;

namespace ReelWords
{
	public class Reel
	{
        public bool randomizeIndices = true;
        private static Reel instance = null;
        private List<Letter[]> reelsLetters;
        private List<int> reelsIndices;

        private Reel() {
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

        public void Insert(string s)
        {
            string[] entries = s.Split();
            Letter[] letters = new Letter[entries.Length];

            for (int i = 0; i < letters.Length; i++) {
                if (char.IsLetter(entries[i][0]))
                    letters[i] = new Letter(entries[i][0], reelsLetters.Count);
            }

            reelsLetters.Add(letters);

            int initialIndex = randomizeIndices ? new Random().Next(0, letters.Length) : 0;
            reelsIndices.Add(initialIndex);
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
    }
}

