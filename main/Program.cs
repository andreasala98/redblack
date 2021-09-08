using System;
using Tree;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\t**************************************");
            Console.WriteLine("\t*      RED-BLACK TREE INSERTION      *");
            Console.WriteLine("\t*         Andrea Sala - 2021         *");
            Console.WriteLine("\t*  github.com/andreasala98/redblack  *");
            Console.WriteLine("\t**************************************\n\n");

            int keyNode;

            Console.Write("Please enter root node value: ");

            try
            {
                keyNode = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Error in reading node key. Exiting from the program");
                return;
            }

            RBTree tree = new RBTree(new Node(keyNode));

            while (true)
            {
                Console.Write("Do you want to add another node? (y/n): ");
                var ans = Console.ReadLine();

                if (ans == "y")
                {
                    Console.Write("Insert node value: ");
                    
                    try
                    {
                        var val = Convert.ToInt32(Console.ReadLine());
                        tree.Insert(val);
                        Console.WriteLine($"\tNode {val} registered.\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error in reading node key. Deleting this node.");
                        continue;
                    }
    
                }
                else if (ans == "n")
                {
                    Console.WriteLine("\n");
                    break;
                }
                else
                {
                    Console.WriteLine("Answer not recognized. Please enter only y or n\n");
                }
            }


            tree.DisplayTree();
            Console.WriteLine("\n##############################\n");
            Console.WriteLine(tree.DisplayFull());

            return;
        }
    }
}
