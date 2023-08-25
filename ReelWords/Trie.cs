namespace ReelWords
{
    public class Trie
    {
        public const char TRIE_ROOT = '^';
        public const char WORD_STOP = '=';
        private static Trie instance = null;
        private static Node root = null;

        private Trie()
        {
            root = new Node(TRIE_ROOT, null);
        }

        public static Trie Instance
        {
            get
            {
                if (instance == null)
                    instance = new Trie();
                return instance;
            }
        }

        public void Insert(string s)
        {
            var currentNode = root;
            foreach (var c in s)
            {
                var child = currentNode.FindChildNode(c);
                if (child == null)
                {
                    child = new Node(c, currentNode);
                    currentNode.children.Add(child);
                }
                currentNode = child;
            }
            currentNode.children.Add(new Node(WORD_STOP, currentNode));
        }

        public bool Search(string s)
        {
            var currentNode = root;
            foreach (var c in s)
            {
                Node child = currentNode.FindChildNode(c);
                if (child == null)
                    return false;
                currentNode = child;
            }
            return currentNode.FindChildNode(WORD_STOP) != null;
        }

        public void Delete(string s)
        {
            var currentNode = root;
            foreach (var c in s)
            {
                Node child = currentNode.FindChildNode(c);
                if (child == null)
                    return;
                currentNode = child;
            }

            if (currentNode.FindChildNode(WORD_STOP) == null)
                return;

            currentNode.DeleteChildNode(WORD_STOP);

            if (!currentNode.IsLeafNode())
                return;

            currentNode = currentNode.parent;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                currentNode.DeleteChildNode(s[i]);
                currentNode = currentNode.parent;
            }
        }
    }
}