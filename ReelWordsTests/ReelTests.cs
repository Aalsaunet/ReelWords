﻿using ReelWords;
using Xunit;

namespace ReelWordsTests
{
	public class ReelTests
	{
        private const string SAMPLE_REEL = "u d x c l a e";
        private const string SAMPLE_REEL2 = "e y v p q y n";
        private const string SAMPLE_LETTER = "u";
        private const string SAMPLE_LETTERS = "ue";

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

        [Fact]
        public void ReelInsertMultipleTest()
        {
            Reel reel = Reel.Instance;
            reel.Insert(SAMPLE_REEL);
            reel.Insert(SAMPLE_REEL2);
            Assert.True(reel.GetCurrentLetters().Equals(SAMPLE_LETTERS));
        }
    }
}

