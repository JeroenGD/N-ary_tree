using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Nary_tree;

namespace Nary_tree_testing
{
    [TestFixture]
    public class Program
    {
        // Test Add child by checking value of childnodes added
        [TestCase]
        public void AddChildTest()
        {
            // Arrange
            Tree<string> Groot = new Tree<string>("I ");
            Tree<int> MultiTree = new Tree<int>(24);

            // Act
            TreeNode<string> Gchild1_1 = Groot.AddChildNode("suck.");
            TreeNode<string> Gchild1_2 = Groot.AddChildNode("am ");
            TreeNode<string> Gchild2_1 = Groot.AddChildNode("Groot", Gchild1_2);

            TreeNode<int> Mchild1_1 = MultiTree.AddChildNode(6);
            TreeNode<int> Mchild1_2 = MultiTree.AddChildNode(4);
            TreeNode<int> Mchild2_1 = MultiTree.AddChildNode(3, Mchild1_1);
            TreeNode<int> Mchild2_2 = MultiTree.AddChildNode(2, Mchild1_1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(Groot.TrunkNode.ChildNodes[1].ChildNodes[0].Value == "Groot");
                Assert.That(MultiTree.TrunkNode.ChildNodes[0].ChildNodes[0].Value == 3);
                Assert.That(MultiTree.TrunkNode.ChildNodes[0].ChildNodes[1].Value == 2);
            });
        }
        // check if removeNode works by adding and removing nodes and checking the node and leaf count.
        // And check if node form which child has been remove still has children
        [TestCase]
        public void RemoveNodeTest()
        {
            // Arrange
            Tree<string> Groot = new Tree<string>("I ");
            TreeNode<string> Gchild1_1 = Groot.AddChildNode("suck.");
            TreeNode<string> Gchild1_2 = Groot.AddChildNode("am ");
            TreeNode<string> Gchild2_1 = Groot.AddChildNode("Groot", Gchild1_2);
            TreeNode<string> Gchild3_1 = Groot.AddChildNode("!!!", Gchild2_1);

            // Act
            Groot.RemoveNode(Gchild2_1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(Groot.NrNodes == 3);
                Assert.That(Groot.NrLeafs == 2);
                Assert.That(Gchild1_2.ChildNodes.Count == 0);
            });
        }
        // Test TraverseNodes by checking list length and by checking if contains added child
        [TestCase]
        public void TraverseNodesTest()
        {
            // Arrange
            Tree<string> Groot = new Tree<string>("I ");
            TreeNode<string> Gchild1_1 = Groot.AddChildNode("suck.");
            TreeNode<string> Gchild1_2 = Groot.AddChildNode("am ");
            TreeNode<string> Gchild2_1 = Groot.AddChildNode("Groot", Gchild1_2);
            TreeNode<string> Gchild3_1 = Groot.AddChildNode("!!!", Gchild2_1);

            // Act
            List<TreeNode<string>> NodeList = Groot.TraverseNodes();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(NodeList.Count, Groot.NrNodes);
                Assert.That(NodeList.Contains(Gchild1_2));
            });
        }
        // Check SumToLeafs by checking if sums are correct
        [TestCase]
        public void SumToLeafsTest()
        {
            // Arrange
            Tree<int> MultiTree = new Tree<int>(24);
            TreeNode<int> Mchild1_1 = MultiTree.AddChildNode(6);
            TreeNode<int> Mchild1_2 = MultiTree.AddChildNode(4);
            TreeNode<int> Mchild2_1 = MultiTree.AddChildNode(3, Mchild1_1);
            TreeNode<int> Mchild2_2 = MultiTree.AddChildNode(2, Mchild1_1);

            // Act
            List<int> Sums = MultiTree.SumToLeafs();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(Sums[0] == 33);
                Assert.That(Sums[1] == 32);
                Assert.That(Sums[2] == 28);
            });

        }

        public static void Main() { }
    }
}

