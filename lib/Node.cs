using System;

namespace Tree
{
    /// <summary>
    /// Enumeration of all possible oolours of a node
    /// </summary>
    public enum Col
    {
        BLK = 1, RED = -1
    }

    /// <summary>
    /// Class representing a single Node of a red-black tree
    /// </summary>
    public class Node
    {
        private Node parent, left, right;
        private int val;
        private Col colour;
        private int height;

        //Constructors 
        public Node()
        {
            this.left = new NIL();
            this.right = new NIL();
        }
        public Node(int key)
        {
            this.val = key;
            this.left = new NIL();
            this.right = new NIL();
        }

        public Node(int key, Col colr)
        {
            this.val = key;
            this.colour = colr;
        }

        public class NIL : Node
        { }



        // Properties
        public Node Left
        {
            get { return left; }
            set { left = value; }
        }
        public Node Right
        {
            get { return right; }
            set { right = value; }
        }

        public Node Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public int Val
        {
            get { return val; }
            set { val = value; }
        }
        public Col Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        public int Height
        {
            get { return height; }
            private set { height = value; }
        }


        public override string ToString()
        {
            return $"Val: {this.Val}";
        }

        public static Node nil = new NIL();

        public bool isLeftChild()
        {
            if (this.Parent.Left != nil)
                if (this == this.Parent.Left)
                    return true;
                else return false;

            else return false;
        }

        public bool isRightChild()
        {
            if (this.Parent.Right != nil)
                if (this == this.Parent.Right)
                    return true;
                else return false;

            else return false;
        }

        public bool isRoot()
        {
            if (Parent == nil) return true;
            else return false;
        }

        public int getNodeHeight()
        {
            Node x = this;
            int height = 0;

            while (x.Parent != nil)
            {
                x = x.Parent;
                height++;
            }
            return height;
        }


        public void Recolor()
        {
            if (this.Colour == Col.BLK) this.Colour = Col.RED;
            else if (this.Colour == Col.RED) this.Colour = Col.BLK;
            return;
        }

    } //class Node

}