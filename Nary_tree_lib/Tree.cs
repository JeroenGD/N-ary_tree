using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace Nary_tree
{
    public class Tree<T> : IEnumerable<T>
    {
        // Keeps track of tree layer in traverse function
        public int TreeLayerCount = 0;

        // First node in tree (the trunk)
        public TreeNode<T> TrunkNode;
        
        // properties of tree
        public int NrNodes;
        public int NrLeafs;

        public Tree(T trunkValue)
        {
            // starting TrunkNode and updating properties
            TrunkNode = new TreeNode<T>(trunkValue, null);
            NrNodes = 1;
            NrLeafs = 1;
        }

        // Adds a child to the tree. When user inputs only value, parent of the 
        // child will be the trunk
        public TreeNode<T> AddChildNode(T nodeValue, TreeNode<T> Parent = null)
        {
            // Check if user has put in parent, if not parent = TrunkNode
            TreeNode<T> parent = Parent ?? TrunkNode;
            // if parent of new node has other children already, new child will
            // form an extra leaf. If first child, leaf count stays the same.
            if (parent.ChildNodes.Count != 0)
            {
                NrLeafs++;
            }
            // generate new node
            TreeNode<T> NewNode = new TreeNode<T>(nodeValue, parent);
            parent.ChildNodes.Add(NewNode);
            // update Node count
            NrNodes++;

            return NewNode;
        }

        // Remove node from tree, if node happens to have childnodes, all 
        // childnodes will be removed too
        public void RemoveNode(TreeNode<T> node)
        {
            // Remove node from parent ChildNodes
            TreeNode<T> Parent = node.Parent;
            int ParentChildCount = Parent.ChildNodes.Count;
            Parent.ChildNodes.Remove(node);
            // Remove parent from node
            node.Parent = null;

            // update count if node is a leaf
            if (node.ChildNodes.Count == 0 && ParentChildCount > 1)
            {
                NrLeafs--;
            }

            // Remove all children of node
            int NrChildNodes = node.ChildNodes.Count;
            for (int i = 0; i < NrChildNodes; i++)
            {
                RemoveNode(node.ChildNodes[i]);
            }
            // remove from node database
            NrNodes--;
        }

        // give all tree nodes
        public List<TreeNode<T>> TraverseNodes()
        {
            return TrunkNode.TraverseNodes();
        }

        // Show tree structure (extra function, Not equal Traverse nodes
        public void PrintTraverseNodes(TreeNode<T> parent, int depth = 0)
        {
            // make line to write to console
            string Line = "";
            // first not first layer(trunk node), add whitspaces and ᴸ for 
            // structure, else don't
            if (TreeLayerCount != 0)
            {
                Line += String.Concat(Enumerable.Repeat(" ", TreeLayerCount - 1));
                Line += "ᴸ";
            }
            // add node value to line
            Line += parent.Value.ToString();
            // write line
            Console.WriteLine(Line);
            // update tree layer count to higher layer
            TreeLayerCount++;
            // print all childnodes using this function
            for (int i = 0; i < parent.ChildNodes.Count; i++)
            {
                PrintTraverseNodes(parent.ChildNodes[i], depth + 1);
            }
            // reduce tree layer count when done writing all children of a node
            TreeLayerCount--;
        }

        // Sum(or concatenate) all nodes, from trunk to leaf, for all leafs 
        public List<T> SumToLeafs()
        {
            // make list for leafs
            List<TreeNode<T>> LeafNodes = new List<TreeNode<T>>();
            // add all nodes without children to Leaf list.
            foreach (TreeNode<T> node in TraverseNodes())
            {
                if (node.ChildNodes.Count == 0)
                {
                    LeafNodes.Add(node);
                }
            }
            // keep track of parent and add filler value.
            TreeNode<T> Parent = TrunkNode;
            // initiate list of summed leaf paths
            List<T> LeafPathSumList = new List<T>();
            // generate summed path for each leaf.
            foreach (TreeNode<T> LeafNode in LeafNodes)
            {
                // make summed leaf path and keep track of parent
                dynamic LeafPathSum = LeafNode.Value;
                Parent = LeafNode.Parent;
                // loop untill node does not have a parent(TrunkNode)
                do
                {
                    LeafPathSum = Parent.Value + LeafPathSum;
                    Parent = Parent.Parent;
                } while (Parent != null);
                // add summed leaf path to list
                LeafPathSumList.Add(LeafPathSum);
            }

            return LeafPathSumList;

        }

        // create enumerator
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T value in TrunkNode)
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (T value in TrunkNode)
            {
                yield return value;
            }
        }
    }
}
