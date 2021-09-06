using System;

namespace Tree
{

    /// <summary>
    /// Enumeration of all possible oolours of a node
    /// </summary>
    public enum Col{
        BLACK, RED
    }

    /// <summary>
    /// Class representing a single Node of a red-black tree
    /// </summary>
    public class Node
    {
        private Node parent, left, right;
        private int? val;
        private Col colour;

        //Constructors 
        public Node (int? key, Node left, Node right)
        {
            this.val = key ?? null;
            this.left = left;
            this.right = right;
        }
        public Node (int? v) : this(v, null, null) {}
        public Node() : this(null,null,null){}

        // Properties
        
        public Node Left{
            get { return left;}
            set{ left = value;}
        }
        public Node Right{
            get { return right;}
            set { right = value;}
        }

        public Node Parent{
            get { return parent;}
            set { parent = value;}
        }

        public int? Val{
            get{return val;}
            set {val = value;}
        }
        public Col Colour {
            get{ return colour;}
            set {colour = value;}
        }
    
        private class NullNode : Node {

            // This class replaces the null 
            // keyword when dealing with Nodes

        }

  

    }

    // ***************************************************

    /// <summary>
    /// Class representing a red-black tree
    /// </summary>
    public class RBTree {
        public Node root;
        private static Node nil = new Node(null, null, null);
        public static Node Nil {
            get { return nil; }
        }

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
            while(x.Left!=RBTree.Nil)
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
            while (x.Right != RBTree.Nil)
                x = x.Right;

            return x;
        }

        /// <summary>
        /// Rotate  a subtree rooted in node x to the left
        /// </summary>
        /// <param name="T">The tree</param>
        /// <param name="x">The pivot of the rotation</param>
        public static void leftRotation (RBTree T, Node x)
        {
            if (x.Right == RBTree.Nil) throw new ArgumentException($"Node with key {x.Val} must have a right child!");

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
        public static void rightRotation (RBTree T, Node x)
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

        /// <summary>
        /// Insert a new node on a Red Black Tree. This takes O(lg n)
        /// Note that colours will be automatically balanced
        /// </summary>
        /// <param name="T"> The Tree </param>
        /// <param name="newnode"> The Node you want to insert </param>
        public static void Insert (RBTree T, Node newnode)
        {
            Node y = null;
            Node x = T.root;

            while (x != RBTree.Nil){
                y = x;
                if (newnode.Val < x.Val)
                    x = x.Left;
                else
                    x = x.Right;
            }

            newnode.Parent = y;
            if (y == RBTree.Nil){
                T.root = newnode;
            }
            else if (newnode.Val < y.Val)
                y.Left = newnode;
            else
                y.Right = newnode;

            newnode.Left = newnode.Right = null;
            newnode.Colour = Col.RED;

            FixColorsInsert(T, newnode);

        }

        public static void FixColorsInsert(RBTree T, Node z) {

            Node y;
            while (z.Parent.Colour == Col.RED)
            {
                if (z.Parent == z.Parent.Parent.Left) // if z parent is a left child
                {
                    y = z.Parent.Parent.Right;
                    if (y.Colour == Col.RED)
                    {
                        z.Parent.Colour = Col.BLACK;
                        y.Colour = Col.BLACK;
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
                        z.Parent.Colour = Col.BLACK;
                        z.Parent.Parent.Colour = Col.RED;
                        rightRotation(T, z.Parent.Parent);
                    }
                }
                else  // if z parent is a right child
                {
                    y = z.Parent.Parent.Left;
                    if (y.Colour == Col.RED)
                    {
                        z.Parent.Colour = Col.BLACK;
                        y.Colour = Col.BLACK;
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
                        z.Parent.Colour = Col.BLACK;
                        z.Parent.Parent.Colour = Col.RED;
                        leftRotation(T, z.Parent.Parent);
                    }
                }
            }
            T.root.Colour = Col.BLACK;
        }

        /// <summary>
        /// Replace the subtree rooted in the first Node
        /// with the subtree rooted in the second Node
        /// </summary>
        /// <param name="T"> the red-black tree</param>
        /// <param name="u"> removed node</param>
        /// <param name="v"> inserted node</param>
        public static void Transplant(RBTree T, Node u, Node v)
        {
            if (u.Parent == RBTree.Nil)
                T.root = v;
            else if (u == u.Parent.Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;
            v.Parent = u.Parent;
        }

        /// <summary>
        /// Delete a node from a red-black tree. This takes O(lg n)
        /// </summary>
        /// <param name="T">The Tree</param>
        /// <param name="d">The node to be removed</param>
        public void deleteNode(RBTree T, Node d)
        {
            Node y = d, x;
            Col yOriginalColor = y.Colour;

            if (d.Left == RBTree.Nil) // no left child
            {
                x = d.Right;
                Transplant(T, d, d.Right);
            }
            else if (d.Right == RBTree.Nil) // only left child
            {
                x = d.Left;
                Transplant(T, d, d.Left);
            }
            else                        // two childs: difficult case
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
                    Transplant(T, y, y.Right);
                    y.Right = d.Right;
                    y.Right.Parent = y;
                }

                Transplant(T, d, y);
                y.Left = d.Left;
                y.Left.Parent = y;
                y.Colour = d.Colour;

            }

            if (yOriginalColor == Col.BLACK)
                FixColorsDelete(T, x);
        }

        /// <summary>
        /// Eventually fix the colors after deletion of a node
        /// </summary>
        /// <param name="T"> The Tree</param>
        /// <param name="x"> A child of the formerly deleted node</param>
        public static void FixColorsDelete(RBTree T, Node x)
        {
            Node w;

            while (x != T.root && x.Colour == Col.BLACK)
            {
                if (x == x.Parent.Left)
                {
                    w = x.Parent.Right;
                    if (w.Colour == Col.RED)
                    {
                        w.Colour = Col.BLACK;
                        x.Parent.Colour = Col.RED;
                        leftRotation(T, x.Parent);
                        w = x.Parent.Right;
                    }

                    if (w.Left.Colour == Col.BLACK && w.Right.Colour == Col.BLACK)
                    {
                        w.Colour = Col.RED;
                        x = x.Parent;
                    }
                    else if (w.Right.Colour == Col.BLACK)
                    {
                        w.Left.Colour = Col.BLACK;
                        w.Colour = Col.RED;
                        rightRotation(T, w);
                        w = x.Parent.Right;
                    }
                    else
                    {
                        w.Colour = x.Parent.Colour;
                        x.Parent.Colour = Col.BLACK;
                        w.Right.Colour = Col.BLACK;
                        leftRotation(T, x.Parent);
                        x = T.root;
                    }
                }

                else
                {
                    w = x.Parent.Left;
                    if (w.Colour == Col.RED)
                    {
                        w.Colour = Col.BLACK;
                        x.Parent.Colour = Col.RED;
                        rightRotation(T, x.Parent);
                        w = x.Parent.Left;
                    }

                    if (w.Right.Colour == Col.BLACK && w.Left.Colour == Col.BLACK)
                    {
                        w.Colour = Col.RED;
                        x = x.Parent;
                    }
                    else if (w.Left.Colour == Col.BLACK)
                    {
                        w.Right.Colour = Col.BLACK;
                        w.Colour = Col.RED;
                        leftRotation(T, w);
                        w = x.Parent.Left;
                    }
                    else
                    {
                        w.Colour = x.Parent.Colour;
                        x.Parent.Colour = Col.BLACK;
                        w.Right.Colour = Col.BLACK;
                        rightRotation(T, x.Parent);
                        x = T.root;
                    }
                }
                x.Colour = Col.BLACK;

            } // while loop
        } // fixColorsDelete

    } //class
} //namespace
