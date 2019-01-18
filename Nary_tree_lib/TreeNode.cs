using System.Collections;
using System.Collections.Generic;

namespace Nary_tree
{
    public class TreeNode<T> : IEnumerable<T>
    {
        // Each node had a value, most of the time a parent, and sometimes children
        // Each node knows it's parent and children.
        public T Value;
        public TreeNode<T> Parent;
        public List<TreeNode<T>> ChildNodes = new List<TreeNode<T>>();

        public TreeNode(T value, TreeNode<T> parent)
        {
            Value = value;
            Parent = parent;
        }

        // Get list of all children, grandchildren, etc
        public List<TreeNode<T>> TraverseNodes()
        {
            List<TreeNode<T>> result = new List<TreeNode<T>>();
            result.Add(this);
            foreach (TreeNode<T> child in ChildNodes)
            {
                result.AddRange(child.TraverseNodes());
            }
            return result;
        }

        // create enumerator
        public IEnumerator<T> GetEnumerator()
        {
            foreach (TreeNode<T> node in TraverseNodes())
            {
                yield return node.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (TreeNode<T> node in TraverseNodes())
            {
                yield return node.Value;
            }
        }
    }
}
