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

            tree.DisplayTree();

            tree.deleteNode(5);

            Console.WriteLine("After deletion:");
            tree.DisplayTree();
        }
    }
}
