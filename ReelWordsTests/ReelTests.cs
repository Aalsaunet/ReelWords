using System.Collections.Generic;
using ReelWords;
using Xunit;

namespace ReelWordsTests
{
	public class ReelTests
	{
        private const string SAMPLE_REEL = "u d x c l a e";
        private const string SAMPLE_REEL2 = "e y v p q y n";
        private const string SAMPLE_REEL3 = "u y x c q y n";
        private const string SAMPLE_LETTER = "u";
        private const string SAMPLE_LETTERS = "ue";
        private const string SAMPLE_LETTERS2 = "dyy";
        private const string SAMPLE_LETTER_SCORE = "u 1";
        private const int SAMPLE_INDEX = 0;
        private const int SAMPLE_SCORE = 1;

        public ReelTests() {
            ReelsManager.Instance.randomizeReelPositions = false;
        }

        [Fact]
        public void ReelInsertTest()
        {
            ReelsManager.Instance.ClearReels();
            ReelsManager.Instance.InsertReel(SAMPLE_REEL);

            Letter[] usableLetters = ReelsManager.Instance.GetCurrentLetters();
            string result = ReelsGameManager.FormatLettersForOutput(usableLetters);

            Assert.True(result.Equals(SAMPLE_LETTER));
        }

        [Fact]
        public void ReelInsertMultipleTest()
        {
            ReelsManager.Instance.ClearReels();
            ReelsManager.Instance.InsertReel(SAMPLE_REEL);
            ReelsManager.Instance.InsertReel(SAMPLE_REEL2);

            Letter[] usableLetters = ReelsManager.Instance.GetCurrentLetters();
            string result = ReelsGameManager.FormatLettersForOutput(usableLetters);

            Assert.True(result.ToString().Equals(SAMPLE_LETTERS));
        }

        [Fact]
        public void ReelIncrementationTest()
        {
            ReelsManager.Instance.ClearReels();
            ReelsManager.Instance.InsertReel(SAMPLE_REEL);
            ReelsManager.Instance.InsertReel(SAMPLE_REEL2);
            ReelsManager.Instance.InsertReel(SAMPLE_REEL3);
            ReelsManager.Instance.IncrementIndices(new List<int> { 0, 1, 2 });

            Letter[] usableLetters = ReelsManager.Instance.GetCurrentLetters();
            string result = ReelsGameManager.FormatLettersForOutput(usableLetters);

            Assert.True(result.Equals(SAMPLE_LETTERS2));
        }

        [Fact]
        public void InsertLetterTest()
        {
            ReelsManager.Instance.ClearReels();
            ReelsManager.Instance.InsertLetterScore(SAMPLE_LETTER_SCORE);
            ReelsManager.Instance.InsertReel(SAMPLE_REEL);
            int score = ReelsManager.Instance.calculatePointsGained(new List<int> { SAMPLE_INDEX });

            Assert.True(score == SAMPLE_SCORE);
        }
    }
}

