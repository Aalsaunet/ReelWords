using System.Collections.Generic;

namespace ReelWords
{
    public class Node
    {
        public char value { get; set; }
        public Node parent { get; set; }
        public List<Node> children { get; set; }

        public Node(char value, Node parent)
        {
            this.value = value;
            this.parent = parent;
            children = new List<Node>();
        }

        public bool IsLeafNode()
        {
            return children.Count == 0;
        }

        public Node FindChildNode(char c)
        {
            foreach (var child in children)
                if (child.value == c)
                    return child;

            return null;
        }

        public void DeleteChildNode(char c)
        {
            for (var i = 0; i < children.Count; i++)
                if (children[i].value == c)
                    children.RemoveAt(i);
        }
    }
}

