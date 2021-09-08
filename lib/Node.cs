using System;

namespace Tree 
{
    /// <summary>
    /// Enumeration of all possible oolours of a node
    /// </summary>
    public enum Col
    {
        BLACK, RED
    }

    /// <summary>
    /// Class representing a single Node of a red-black tree
    /// </summary>
    public class Node
    {
        private Node parent, left, right;
        private int val;
        private Col colour;

        //Constructors 
        public Node(int key)
        {
            this.val = key;
        }

        public Node(int key, Col colr)
        {
            this.val = key;
            this.colour = colr;
        }
        // public Node(int v) : this(v, null, null) { }

        // public Node() : this(0, null, null) { }

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


        public override string ToString()
        {
            return $"Val: {this.Val}";
        }

        public bool isLeftChild()
        {
            if (this.Parent.Left != null)
                if (this == this.Parent.Left)
                    return true;
                else return false;

            else return false;
        }

        public bool isRightChild()
        {
            if (this.Parent.Right is not null)
                if (this == this.Parent.Right)
                    return true;
                else return false;

            else return false;
        }
        /*
        public bool isRightChild()
        {
            bool ans = this.Val == this.Parent.Right.Val ? true : false;
            return ans;
        }*/

        public bool isRoot()
        { 
            if (Parent is null) return true;
            else return false;
        }



    } //class Node

}