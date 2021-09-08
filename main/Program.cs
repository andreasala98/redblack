using System;
using Tree;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            RBTree tree = new RBTree(new Node(7));

            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(3);
            tree.Insert(11);
            tree.Insert(6);
            tree.Insert(2);

            tree.DisplayTree();

            tree.deleteNode(5);
            Console.WriteLine("After deletion of 5:");
            tree.DisplayTree();

            tree.deleteNode(8);
            Console.WriteLine("After deletion of 8:");
            tree.DisplayTree();

            tree.deleteNode(6);
            Console.WriteLine("After deletion of 6:");
            tree.DisplayTree();

            tree.deleteNode(12345);
            Console.WriteLine("After deletion of 12345:");
            tree.DisplayTree();
        }
    }
}
