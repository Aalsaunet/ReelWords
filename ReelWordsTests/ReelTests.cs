using System;
using ReelWords;
using Xunit;

namespace ReelWordsTests
{
	public class ReelTests
	{
        private const string SAMPLE_REEL = "u d x c l a e";
        private const string SAMPLE_LETTER = "u";

        public ReelTests() {
            Reel.Instance.randomizeIndices = false;
        }

        [Fact]
        public void ReelInsertTest()
        {
            Reel reel = Reel.Instance;
            reel.Insert(SAMPLE_REEL);
            Assert.True(reel.GetCurrentLetters().Equals(SAMPLE_LETTER));
        }
    }
}

