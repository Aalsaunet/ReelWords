using System;
using System.Collections.Generic;

namespace ReelWords
{
    public class ReelsManager
    {
        public bool randomizeReelPositions = true;
        private static ReelsManager instance = null;
        private Dictionary<char, int> letterToScore = null;
        private List<Letter[]> reels;
        private List<int> reelsPositions;

        private ReelsManager()
        {
            letterToScore = new Dictionary<char, int>();
            reels = new List<Letter[]>();
            reelsPositions = new List<int>();
        }

        public static ReelsManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ReelsManager();
                return instance;
            }
        }

        public void InsertReel(string reelLine)
        {
            string[] reelLetters = reelLine.Split();
            Letter[] lettersInReel = new Letter[reelLetters.Length];

            for (int i = 0; i < lettersInReel.Length; i++)
            {
                char letter = reelLetters[i][0];
                if (char.IsLetter(letter))
                {
                    int points = letterToScore.ContainsKey(letter) ? letterToScore[letter] : 0;
                    lettersInReel[i] = new Letter(letter, reels.Count, points);
                }
            }

            int initialPosition = randomizeReelPositions ? new Random().Next(0, lettersInReel.Length) : 0;
            reels.Add(lettersInReel);
            reelsPositions.Add(initialPosition);
        }

        public void InsertLetterScore(string s)
        {
            string[] letterAndScore = s.Split();
            letterToScore[char.Parse(letterAndScore[0])] = Int32.Parse(letterAndScore[1]);
        }

        public Letter[] GetCurrentLetters()
        {
            Letter[] letters = new Letter[reels.Count];
            for (int i = 0; i < letters.Length; i++)
                letters[i] = reels[i][reelsPositions[i]];
            return letters;
        }

        public void IncrementIndices(List<int> indices)
        {
            foreach (var index in indices)
            {
                reelsPositions[index] = (reelsPositions[index] + 1)
                    % reels[index].Length;
            }
        }

        public int calculatePointsGained(List<int> indices)
        {
            int sum = 0;
            foreach (var index in indices)
                sum += reels[index][reelsPositions[index]].pointValue;
            return sum;
        }

        public void ClearReels()
        {
            letterToScore = new Dictionary<char, int>();
            reels = new List<Letter[]>();
            reelsPositions = new List<int>();
        }
    }
}

