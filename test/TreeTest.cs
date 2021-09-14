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

            Assert.True(tree.DisplayTreeColorStr() == "(1RED) (3BLK) (4RED) (5BLK) (7BLK) (8BLACK) (9RED) (13RED) (15BLK) ",
                    "TestInsertColors failed!");
            return;
        }

        [Fact]
        void TestDelete()
        {
            RBTree tree = new RBTree(new Node(7));

            tree.Insert(6);
            tree.Insert(11);
            tree.Insert(2);
            tree.Insert(14);
            tree.Insert(10);
            tree.Insert(8);

            Assert.True(tree.DisplayTreeStr() == "(2) (6) (7) (8) (10) (11) (14) ", "TestDelete failed! Check 1");
            Assert.True(tree.DisplayFull() == "(2, RED, h=2)\n(6, BLK, h=1)\n(7, BLK, h=0)\n(8, RED, h=3)\n(10, BLK, h=2)\n(11, RED, h=1)\n(14, BLK, h=2)\n",
                                              $"TestDelete failed! Check 1b, {tree.DisplayFull()}");

            tree.deleteNode(8);

            Assert.True(tree.DisplayTreeStr() == "(2) (6) (7) (10) (11) (14) ", "TestDelete failed! Check 2");
            Assert.True(tree.DisplayFull() == "(2, RED, h=2)\n(6, BLK, h=1)\n(7, BLK, h=0)\n(10, BLK, h=2)\n(11, RED, h=1)\n(14, BLK, h=2)\n",
                                              $"TestDelete failed! Check 2b, {tree.DisplayFull()}");

            tree.deleteNode(14);

            Assert.True(tree.DisplayTreeStr() == "(2) (6) (7) (10) (11) ", "TestDelete failed! Check 3");
            Assert.True(tree.DisplayFull() == "(2, RED, h=2)\n(6, BLK, h=1)\n(7, BLK, h=0)\n(10, BLK, h=2)\n(11, RED, h=1)\n",
                                              $"TestDelete failed! Check 3b, {tree.DisplayFull()}");

            tree.deleteNode(6);

            Assert.True(tree.DisplayFull() == "(2, RED, h=1)\n(7, BLK, h=0)\n(10, BLK, h=2)\n(11, RED, h=1)\n", $"TestDelete failed! Check 4, {tree.DisplayFull()}");

            return;
        }
    }
}
