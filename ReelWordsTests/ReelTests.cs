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
            ReelsManager reel = ReelsManager.Instance;
            reel.InsertReel(SAMPLE_REEL);
            Assert.True(reel.GetCurrentLetters().Equals(SAMPLE_LETTER));
        }

        [Fact]
        public void ReelInsertMultipleTest()
        {
            ReelsManager reel = ReelsManager.Instance;
            reel.InsertReel(SAMPLE_REEL);
            reel.InsertReel(SAMPLE_REEL2);
            Assert.True(reel.GetCurrentLetters().Equals(SAMPLE_LETTERS));
        }

        [Fact]
        public void ReelIncrementationTest()
        {
            ReelsManager reel = ReelsManager.Instance;
            reel.InsertReel(SAMPLE_REEL);
            reel.IncrementIndices(new List<int> { 1, 4, 5, 6 });
            Assert.True(reel.GetCurrentLetters().Equals(SAMPLE_REEL3));
        }
    }
}

