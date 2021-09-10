/*

This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <https://unlicense.org>


*/




using System;

namespace Tree
{

    /// <summary>
    /// Class representing a red-black tree
    /// </summary>
    public class RBTree {

        /// <summary>
        /// THe root node of the red black tree
        /// </summary>
        private Node root;

        /// <summary>
        /// Constructor passing the root node
        /// </summary>
        public RBTree (Node r)
            => this.root = r;
        

        /// <summary>
        /// Returns the Node with minimum value of a subtree
        /// starting at the given input node
        /// </summary>
        /// <param name="x"> Root of the subtree</param>
        public static Node subtreeMinimum(Node x)
        {
            while(x.Left!=null)  x = x.Left;
            return x;
        }

        /// <summary>
        /// Returns the Node with maximum value of a subtree
        /// starting at the given input node
        /// </summary>
        /// <param name="x"> Root of the subtree</param>
        public static Node subtreeMaximum(Node x)
        {
            while (x.Right != null)  x = x.Right;
            return x;
        }

        /// <summary>
        /// Rotate  a subtree rooted in node x to the left
        /// </summary>
        /// <param name="T">The tree</param>
        /// <param name="x">The pivot of the rotation</param>
        private static void leftRotation (RBTree T, Node x)
        {
            if (x.Right == null) throw new ArgumentException($"Node with key {x.Val} must have a right child!");

            Node y = x.Right;
            x.Right = y.Left; //Attach the rest of the tree (ONE DIRECTION)
            if (y.Left is not null){
                y.Left.Parent = x; //Attach the rest of the tree (OTHER DIRECTION)
            }
            y.Parent = x.Parent;
            if (x.Parent is null) // x is root node
            {
                T.root = y; //Make y the new root
            }
            else if (x == x.Parent.Left) //x is a left child
            {
                x.Parent.Left = y; //put y where x was
            }
            else // x is a right child
            {
                x.Parent.Right = y;//put y where x was
            }

            y.Left = x; //Now we put x where he belongs (both dirs)
            x.Parent = y;

        }

        /// <summary>
        ///  Rotate a subtree rooted in node x to the right
        /// </summary>
        /// <param name="T"> The tree</param>
        /// <param name="x"> Pivot of the rotation</param>
        /// <exception cref="ArgumentException"></exception>
        private static void rightRotation (RBTree T, Node x)
        {
            if (x.Left is null) throw new ArgumentException($"Node with key {x.Val} must have a left child!");

            Node y = x.Left;
            x.Left = y.Right; //Attach the rest of the tree (ONE DIRECTION)
            
            if (y.Right is not null){
                y.Right.Parent = x; //Attach the rest of the tree (OTHER DIRECTION)
            }
            y.Parent = x.Parent;
            if (x.Parent is null) // x is root node
            {
                T.root = y; //Make y the new root
            }
            else if (x == x.Parent.Right) //x is a right child
            {
                x.Parent.Right = y; //put y where x was
            }
            else // x is a left child
            {
                x.Parent.Left = y;//put y where x was
            }

            y.Right = x; //Now we put x where he belongs (both dirs)
            x.Parent = y;
        }

#nullable enable
        public Node? Search(int key)
        {
            bool found = false;
            Node appo = this.root;
            Node? obj = null;

            while (!found)
            {
                if (appo == null) break;
                if (key < appo.Val) { appo = appo.Left; continue; }
                if (key > appo.Val) { appo = appo.Right; continue; }
                if (key == appo.Val) {found = true; obj = appo; }
            }

            if (found) return obj;
            else { Console.WriteLine($"Node {key} not found"); return null; }
        }

        /// <summary>
        /// Insert a new node on a Red Black Tree. This takes O(lg n)
        /// Note that colours will be automatically balanced
        /// </summary>
        /// <param name="newnode"> The value of the Node you want to insert </param>
        public void Insert (int newnodeVal)
        {
            Node newnode = new Node(newnodeVal);
            Node? y = null;
            Node x = this.root;

            while (x != null){
                y = x;
                if (newnode.Val < x.Val)
                    x = x.Left;
                else
                    x = x.Right;
            }
            newnode.Parent = y;
            if (y == null){
                this.root = newnode;
            }
            else if (newnode.Val < y.Val)
                y.Left = newnode;
            else
                y.Right = newnode;

            newnode.Left = newnode.Right = null;
            newnode.Colour = Col.RED;

            FixColorsInsert(this, newnode);

        }
      
        /// <summary>
        /// Recolors nodes after insertion
        /// (only if needed)
        /// </summary>
        private static void FixColorsInsert(RBTree T, Node z) {

            Node y;

            while (z != T.root && z.Parent is not null && z.Parent.Colour == Col.RED) //It enters here only if we have colour problems
            {
                if ((z.Parent).isLeftChild()) // if z parent is a left child
                {
                    y = z.Parent.Parent.Right ?? new Node(int.MaxValue, Col.BLK);
                    if (y.Colour == Col.RED)
                    {
                        z.Parent.Colour = Col.BLK;
                        y.Colour = Col.BLK;
                        z.Parent.Parent.Colour = Col.RED;
                        z = z.Parent.Parent;
                    }
                    else if (z == z.Parent.Right)
                    {
                        z = z.Parent;
                        leftRotation(T, z);
                    }
                    else
                    {
                        z.Parent.Colour = Col.BLK;
                        z.Parent.Parent.Colour = Col.RED;
                        rightRotation(T, z.Parent.Parent);
                    }
                }
                else  // if z parent is a right child
                {
                    y = z.Parent.Parent.Left ?? new Node(int.MaxValue, Col.BLK);
                    if (y.Colour == Col.RED)
                    {
                        z.Parent.Colour = Col.BLK;
                        y.Colour = Col.BLK;
                        z.Parent.Parent.Colour = Col.RED;
                        z = z.Parent.Parent;
                    }
                    else if (z == z.Parent.Left)
                    {
                        z = z.Parent;
                        rightRotation(T, z);
                    }
                    else
                    {
                        z.Parent.Colour = Col.BLK;
                        z.Parent.Parent.Colour = Col.RED;
                        leftRotation(T, z.Parent.Parent);
                    }
                }
                    
            }
            T.root.Colour = Col.BLK;
        }
#nullable enable

        // Some printing functions

        private void InOrderDisplay(Node current)
        {
            if (current != null)
            {
                InOrderDisplay(current.Left);
                Console.Write($"({current.Val})");
                InOrderDisplay(current.Right);
            }
        }

        private string InOrderDisplayStr(Node current)
        {
            string s = "";
            if (current != null)
            {
                s += InOrderDisplayStr(current.Left);
                s +=$"({current.Val}) ";
                s += InOrderDisplayStr(current.Right);
            }
            return s;
        }

        private string InOrderDisplayColorStr(Node current)
        {
            string s = "";
            if (current != null)
            {
                s += InOrderDisplayColorStr(current.Left);
                s += $"({current.Val}{current.Colour}) ";
                s += InOrderDisplayColorStr(current.Right);
            }
            return s;
        }

        private string InOrderFull(Node current)
        {
            string s = "";
            if (current != null)
            {
                s += InOrderFull(current.Left);
                s += $"({current.Val}, {current.Colour}, h={current.getNodeHeight()})\n";
                s += InOrderFull(current.Right);
            }
            return s;
        }


        public void DisplayTree()
        {
            if (this.root is null)
            {
                Console.WriteLine("The tree is empty! :(");
                return;
            }
            else InOrderDisplay(root);
            Console.WriteLine("\n");
        }

        public string DisplayTreeStr()
        {
            if (this.root is null)
            {
                Console.WriteLine("The tree is empty! :(");
                return "";
            }
            else return InOrderDisplayStr(root);
        }

        public string DisplayTreeColorStr()
        {
            if (this.root is null)
            {
                Console.WriteLine("The tree is empty! :(");
                return "";
            }
            else return InOrderDisplayColorStr(this.root);
        }

        public string DisplayFull()
        {
            if (this.root is null)
            {
                Console.WriteLine("The tree is empty! :(");
                return "";
            }
            else return InOrderFull(this.root);
        }


    } //class
} //namespace
