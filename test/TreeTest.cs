using System;
using Tree;
using Xunit;

namespace test
{
    public class RedBlackTest
    {
        [Fact]
        public void TestInsert()
        {
            RBTree tree = new RBTree(new Node(7));

            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(4);

            Assert.True(tree.DisplayTreeStr() == "(3) (4) (5) (7) (8) ", $"I'm displaying {tree.DisplayTreeStr()}");

            return;
        }
    }
}
