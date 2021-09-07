using System;
using Tree;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
            // Node.print2D(tree.root);
            Console.Write("Insert root node:");
            var rootval = Console.ReadLine();

            RBTree tree = new RBTree(new Node(Convert.ToInt32(rootval)));
            var r='o';
            int nodeVal;

            
            
            while (r!='X')
            {
                Console.WriteLine("N: new node, P: print tree, X: exit");
                r = Convert.ToChar(Console.Read());

                switch (r) 
                {
                    case 'N':
                        Console.Write("Insert node value: ");
                        nodeVal = Console.Read();
                        tree.Insert(new Node(r));
                        break;
                    case 'P':
                        tree.DisplayTree();
                        break;
                    case 'X':
                    default:
                        break;
                }
            }

            */


            RBTree tree = new RBTree(new Node(7));

            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(4);

            tree.DisplayTree();

            tree.deleteNode(5);

            Console.WriteLine("After deletiion:");
            tree.DisplayTree();
        }
    }
}
