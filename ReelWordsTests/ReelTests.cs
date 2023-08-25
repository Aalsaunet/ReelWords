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

        public ReelTests() {
            ReelsManager.Instance.randomizeReelPositions = false;
        }

        [Fact]
        public void ReelInsertTest()
        {
            ReelsManager.Instance.ClearReels();
            ReelsManager.Instance.InsertReel(SAMPLE_REEL);
            Assert.True(ReelsManager.Instance.GetCurrentLetters().Equals(SAMPLE_LETTER));
        }

        [Fact]
        public void ReelInsertMultipleTest()
        {
            ReelsManager.Instance.ClearReels();
            ReelsManager.Instance.InsertReel(SAMPLE_REEL);
            ReelsManager.Instance.InsertReel(SAMPLE_REEL2);
            Assert.True(ReelsManager.Instance.GetCurrentLetters().Equals(SAMPLE_LETTERS));
        }

        [Fact]
        public void ReelIncrementationTest()
        {
            ReelsManager.Instance.ClearReels();
            ReelsManager.Instance.InsertReel(SAMPLE_REEL);
            ReelsManager.Instance.InsertReel(SAMPLE_REEL2);
            ReelsManager.Instance.IncrementIndices(new List<int> { 1, 4, 5, 6 });
            Assert.True(ReelsManager.Instance.GetCurrentLetters().Equals(SAMPLE_REEL3));
        }
    }
}

