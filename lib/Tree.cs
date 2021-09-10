using System;

namespace Tree
{

    /// <summary>
    /// Class representing a red-BLK tree
    /// </summary>
    public class RBTree {
        private Node root;

        public RBTree (Node r)
        {
            this.root = r;
        }
 
        /// <summary>
        /// Returns the Node with minimum value of a subtree
        /// starting at the given input node
        /// </summary>
        /// <param name="x"> Root of the subtree</param>
        public static Node subtreeMinimum(Node x)
        {
            while(x.Left!=null)
                x = x.Left;

            return x;
        }

        /// <summary>
        /// Returns the Node with maximum value of a subtree
        /// starting at the given input node
        /// </summary>
        /// <param name="x"> Root of the subtree</param>
        public static Node subtreeMaximum(Node x)
        {
            while (x.Right != null)
                x = x.Right;

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
                if (key == appo.Val)
                {
                    found = true;
                    obj = appo;
                }
            }

            if (found ) return obj;
            else { Console.WriteLine($"Node {key} not found"); return null; }
        }

#nullable enable
        /// <summary>
        /// Insert a new node on a Red BLK Tree. This takes O(lg n)
        /// Note that colours will be automatically balanced
        /// </summary>
        /// <param name="T"> The Tree </param>
        /// <param name="newnode"> The Node you want to insert </param>
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

/*
        /// <summary>
        /// Replace the subtree rooted in the first Node
        /// with the subtree rooted in the second Node
        /// </summary>
        /// <param name="T"> the red-BLK tree</param>
        /// <param name="u"> removed node</param>
        /// <param name="v"> inserted node</param>
        private static void Transplant(RBTree T, Node u, Node v)
        {
            if (u.Parent == null)
                T.root = v;
            else if (u == (u.Parent).Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;
            if (v!= null)
                v.Parent = u.Parent;
        }

        /// <summary>
        /// Delete a node from a red-BLK tree. This takes O(lg n)
        /// </summary>
        /// <param name="T">The Tree</param>
        /// <param name="d">The node to be removed</param>
        public void deleteNode(int dVal)
        {
            Node? d = Search(dVal);
            if (d==null) return;
            Node y = d;
            Node x;
            Col yOriginalColor = y.Colour;

            if (d.Left == null) // no left child
            {
                x = d.Right;
                Transplant(this, d, d.Right);
            }
            else if (d.Right == null) // only left child
            {
                x = d.Left;
                Transplant(this, d, d.Left);
            }
            else                        // two children: difficult case
            {
                y = subtreeMinimum(d.Right);
                yOriginalColor = y.Colour;
                x = y.Right;

                if (y.Parent == d)
                {
                    x.Parent = y;
                }
                else
                {
                    Transplant(this, y, y.Right);
                    y.Right = d.Right;
                    y.Right.Parent = y;
                }

                Transplant(this, d, y);
                y.Left = d.Left;
                y.Left.Parent = y;
                y.Colour = d.Colour;

            }

            if (yOriginalColor == Col.BLK)
                FixColorsDelete(this, x);
        }

        /// <summary>
        /// Eventually fix the colors after deletion of a node
        /// </summary>
        /// <param name="T"> The Tree</param>
        /// <param name="x"> A child of the formerly deleted node</param>
        public static void FixColorsDelete(RBTree T, Node x)
        {
            Node w;

            while (x.Parent is not null && x.Colour == Col.BLK)
            {
                if (x == x.Parent.Left)
                {
                    w = x.Parent.Right;
                    if (w.Colour == Col.RED)
                    {
                        w.Colour = Col.BLK;
                        x.Parent.Colour = Col.RED;
                        leftRotation(T, x.Parent);
                        w = x.Parent.Right;
                    }

                    if (w.Left.Colour == Col.BLK && w.Right.Colour == Col.BLK)
                    {
                        w.Colour = Col.RED;
                        x = x.Parent;
                    }
                    else if (w.Right.Colour == Col.BLK)
                    {
                        w.Left.Colour = Col.BLK;
                        w.Colour = Col.RED;
                        rightRotation(T, w);
                        w = x.Parent.Right;
                    }
                    else
                    {
                        w.Colour = x.Parent.Colour;
                        x.Parent.Colour = Col.BLK;
                        w.Right.Colour = Col.BLK;
                        leftRotation(T, x.Parent);
                        x = T.root;
                    }
                }

                else
                {
                    w = x.Parent.Left;
                    if (w.Colour == Col.RED)
                    {
                        w.Colour = Col.BLK;
                        x.Parent.Colour = Col.RED;
                        rightRotation(T, x.Parent);
                        w = x.Parent.Left;
                    }

                    if (w.Right.Colour == Col.BLK && w.Left.Colour == Col.BLK)
                    {
                        w.Colour = Col.RED;
                        x = x.Parent;
                    }
                    else if (w.Left.Colour == Col.BLK)
                    {
                        w.Right.Colour = Col.BLK;
                        w.Colour = Col.RED;
                        leftRotation(T, w);
                        w = x.Parent.Left;
                    }
                    else
                    {
                        w.Colour = x.Parent.Colour;
                        x.Parent.Colour = Col.BLK;
                        w.Right.Colour = Col.BLK;
                        rightRotation(T, x.Parent);
                        x = T.root;
                    }
                }
                x.Colour = Col.BLK;

            } // while loop
        } // fixColorsDelete

        */

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
