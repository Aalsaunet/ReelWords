namespace ReelWords
{
    public class Trie
    {
        private static Trie instance = null;
        private static Node root = null;

        private Trie()
        {
            root = new Node('^', null);
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
        }

        public bool Search(string s)
        {
            var currentNode = root;
            foreach (var c in s)
            {
                var child = currentNode.FindChildNode(c);
                if (child == null)
                    return false;
                currentNode = child;
            }
            return true;
        }

        public void Delete(string s)
        {
            var currentNode = root;
            foreach (var c in s)
            {
                var child = currentNode.FindChildNode(c);
                if (child == null)
                    return;
                currentNode = child;
            }

            currentNode = currentNode.parent;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                currentNode.DeleteChildNode(s[i]);
                currentNode = currentNode.parent;
            }
        }
    }
}