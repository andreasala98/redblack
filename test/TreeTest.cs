using System;
using Tree;
using Xunit;

namespace test
{
    public class RedBlackTest
    {

        [Fact]
        public void TestisRoot()
        {
            Node r = new Node(5);
            RBTree tree = new RBTree(r);
            tree.Insert(4);

            Assert.True(r.isRoot());
            Assert.False(r.Left.isRoot());
            return;
        }
        [Fact]
        public void TestInsert()
        {
            RBTree tree = new RBTree(new Node(7));

            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(4);

            Assert.True(tree.DisplayTreeStr() == "(3) (4) (5) (7) (8) ", $"TestInsert failed! I'm displaying {tree.DisplayTreeStr()}");

            return;
        }

        [Fact]
        public void TestInsertColors()
        {
            RBTree tree = new RBTree(new Node(7));

            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(4);

            tree.Insert(15);
            tree.Insert(9);
            tree.Insert(13);
            tree.Insert(1);

            Assert.True(tree.DisplayTreeColorStr() == "(1RED) (3BLK) (4RED) (5BLK) (7BLK) (8BLK) (9RED) (13RED) (15BLK) ",
                    $"TestInsertColors failed! {tree.DisplayTreeColorStr()}");
            return;
        }
    }
}
