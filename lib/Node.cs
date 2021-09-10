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
    /// Enumeration of all possible Colours of a node
    /// </summary>
    public enum Col { BLK, RED }

    /// <summary>
    /// Class representing a single Node of a red-black tree
    /// </summary>
    public class Node
    {
        private Node parent, left, right;
        private int val, height;
        private Col colour;

        //Constructors 
        public Node(int key)
          =>  this.val = key;
        
        public Node(int key, Col colr)
        {
            this.val = key;
            this.colour = colr;
        }
       
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

        public bool isLeftChild()
        {
            if (this.Parent.Left != null)
                if (this == this.Parent.Left)
                    return true; else return false;
            else return false;
        }

        public bool isRightChild()
        {
            if (this.Parent.Right is not null)
                if (this == this.Parent.Right) 
                return true; else return false;
            else return false;
        }

        public bool isRoot()
        { 
            if (Parent is null) return true;
            else return false;
        }

        public int getNodeHeight()
        {
            Node x = this;
            int height = 0;
            while (x.Parent != null)
            {
                x = x.Parent;
                height++;
            }
            return height;
        }

    } //class Node

} //namespace