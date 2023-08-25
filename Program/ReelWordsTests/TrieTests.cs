using ReelWords;
using Xunit;

namespace ReelWordsTests
{
    public class TrieTests
    {
        private const string TEST_WORD = "parallel";
        private const string TEST_WORD2 = "parall";

        [Fact]
        public void TrieInsertTest()
        {
            Trie trie = Trie.Instance;
            trie.Insert(TEST_WORD);
            Assert.True(trie.Search(TEST_WORD));
        }

        [Fact]
        public void TrieDeleteTest()
        {
            Trie trie = Trie.Instance;
            trie.Insert(TEST_WORD);
            Assert.True(trie.Search(TEST_WORD));
            trie.Delete(TEST_WORD);
            Assert.False(trie.Search(TEST_WORD));
        }

        [Fact]
        public void TrieNoFalseDeleteTest()
        {
            Trie trie = Trie.Instance;
            trie.Insert(TEST_WORD);
            Assert.True(trie.Search(TEST_WORD));
            trie.Delete(TEST_WORD2);
            Assert.True(trie.Search(TEST_WORD));
        }

        [Fact]
        public void TrieNoFalseDeleteTest2()
        {
            Trie trie = Trie.Instance;
            trie.Insert(TEST_WORD2);
            Assert.True(trie.Search(TEST_WORD2));
            trie.Delete(TEST_WORD);
            Assert.True(trie.Search(TEST_WORD2));
        }
    }
}