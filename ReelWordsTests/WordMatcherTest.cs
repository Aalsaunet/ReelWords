using System.Collections.Generic;
using ReelWords;
using Xunit;

namespace ReelWordsTests
{
    public class WordMatcherTest
    {
        private const string TEST_WORD_LONG = "hey";
        private const string EXPECTED_HELP_STRING = "There are 1 " +
            "possible word combinations from these letters.\n";

        [Fact]
        public void FindIndexMatchesTest()
        {
            Letter a = new Letter('a', 0, 1);
            Letter b = new Letter('b', 1, 1);
            Letter c = new Letter('c', 2, 1);

            Letter[] usableLetters = { a, b, c };
            char[] inputLetters = { 'b', 'c' };

            List<int> result = WordMatcher.FindIndexMatches(usableLetters, inputLetters);

            Assert.True(result.Count == 2);
            Assert.True(result[0] == 1);
            Assert.True(result[1] == 2);
        }

        [Fact]
        public void GenerateHintTests()
        {
            Trie trie = Trie.Instance;
            trie.Insert(TEST_WORD_LONG);

            Letter h = new Letter('h', 0, 1);
            Letter e = new Letter('e', 1, 1);
            Letter y = new Letter('y', 2, 1);
            Letter[] usableLetters = { h, e, y };

            string result = WordMatcher.GenerateHint(usableLetters);
            Assert.True(result.Equals(EXPECTED_HELP_STRING));
        }
    }
}

