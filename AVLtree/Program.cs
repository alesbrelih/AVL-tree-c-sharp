using System;

namespace AVLtree
{
    class AVLrotations
    {
        public static AVLnode LeftRotation(AVLnode node)
        {
            AVLnode root = node.RightChild;

            AVLnode pivot = root.LeftChild;

            root.LeftChild = node;
            node.RightChild = pivot;

            return root;
        }
        public static AVLnode RightRotation(AVLnode node)
        {
            AVLnode root = node.LeftChild;

            AVLnode pivot = root.RightChild;

            root.RightChild = node;
            node.LeftChild = pivot;

            return root;
        }
    }
    class AVLtree
    {
        public AVLnode Root { get; set; }

        public AVLtree()
        {

        }
        public AVLtree(AVLnode node)
        {
            this.Root = node;
        }


        public void InsertValue(int value)
        {
            AVLnode node = new AVLnode(value);
            this.Root = AVLtree.InsertRecursive(node, this.Root);
        }

        /**
            Main function
             */
        public static AVLnode InsertRecursive(AVLnode node, AVLnode root)
        {
            if (root == null)
            {
                return node;
            }
            else
            {
                if (node.Value < root.Value)
                {
                    root.LeftChild = AVLtree.InsertRecursive(node, root.LeftChild);
                }
                else
                {
                    root.RightChild = AVLtree.InsertRecursive(node, root.RightChild);
                }
            }

            if (root.BalanceFactor > 1)
            {
                // leva stran poddrevesa
                if (node.Value < root.LeftChild.Value)
                {
                    root = AVLrotations.RightRotation(root);
                }
                else
                {
                    // desna stran poddrevesa
                    root.RightChild = AVLrotations.LeftRotation(root.RightChild);
                    root = AVLrotations.RightRotation(root);

                }
            }
            else if (root.BalanceFactor < -1)
            {
                if (node.Value > root.RightChild.Value)
                {
                    root = AVLrotations.LeftRotation(root);
                }
                else
                {
                    root.RightChild = AVLrotations.RightRotation(root);
                    root = AVLrotations.LeftRotation(root);
                }
            }

            // set balance factor
            return root;
        }

        public static int GetBalanceFactor(AVLnode node)
        {
            return node.LeftChild.Height - node.RightChild.Height;
        }

    }
    class AVLnode
    {
        public AVLnode LeftChild { get; set; }
        public AVLnode RightChild { get; set; }

        public int LeftChildHeight
        {
            get
            {
                return this.LeftChild == null ? 0 : this.LeftChild.Height;
            }
        }

        public int RightChildHeight
        {
            get
            {
                return this.RightChild == null ? 0 : this.RightChild.Height;
            }
        }

        public int BalanceFactor
        {
            get
            {
                return this.LeftChildHeight - this.RightChildHeight;
            }
        }

        public int Height
        {
            get
            {
                return Math.Max(this.LeftChildHeight, this.RightChildHeight) + 1;
            }
        }

        public int Value { get; set; }

        public AVLnode(int value)
        {
            this.Value = value;
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            AVLtree tree = new AVLtree();
            tree.InsertValue(10);
            tree.InsertValue(9);
            tree.InsertValue(8);
            tree.InsertValue(7);
            tree.InsertValue(6);
            tree.InsertValue(5);
            tree.InsertValue(4);
            tree.InsertValue(3);
            tree.InsertValue(2);
            tree.InsertValue(1);
            Console.WriteLine(tree);
        }
    }
}
