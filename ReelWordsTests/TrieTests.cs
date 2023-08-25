using ReelWords;
using Xunit;

namespace ReelWordsTests
{
    public class TrieTests
    {
        private const string TEST_WORD_LONG = "parallel";
        private const string TEST_WORD_SHORT = "paral";

        [Fact]
        public void TrieInsertTest()
        {
            Trie trie = Trie.Instance;
            trie.Insert(TEST_WORD_LONG);
            Assert.True(trie.Search(TEST_WORD_LONG));
        }

        [Fact]
        public void TrieDeleteTest()
        {
            Trie trie = Trie.Instance;
            trie.Insert(TEST_WORD_LONG);
            Assert.True(trie.Search(TEST_WORD_LONG));
            trie.Delete(TEST_WORD_LONG);
            Assert.False(trie.Search(TEST_WORD_LONG));
        }

        [Fact]
        public void TrieNoFalseDeleteTest()
        {
            Trie trie = Trie.Instance;
            trie.Insert(TEST_WORD_LONG);
            Assert.True(trie.Search(TEST_WORD_LONG));
            trie.Delete(TEST_WORD_SHORT);
            Assert.True(trie.Search(TEST_WORD_LONG));
        }

        [Fact]
        public void TrieNoFalseDeleteTest2()
        {
            Trie trie = Trie.Instance;
            trie.Insert(TEST_WORD_SHORT);
            Assert.True(trie.Search(TEST_WORD_SHORT));
            trie.Delete(TEST_WORD_LONG);
            Assert.True(trie.Search(TEST_WORD_SHORT));
        }

        [Fact]
        public void TrieDeleteDoesNotBreakOtherWordsTest()
        {
            Trie trie = Trie.Instance;
            trie.Insert(TEST_WORD_LONG);
            trie.Insert(TEST_WORD_SHORT);
            Assert.True(trie.Search(TEST_WORD_LONG));
            Assert.True(trie.Search(TEST_WORD_SHORT));
            trie.Delete(TEST_WORD_SHORT);
            Assert.True(trie.Search(TEST_WORD_LONG));
        }
    }
}