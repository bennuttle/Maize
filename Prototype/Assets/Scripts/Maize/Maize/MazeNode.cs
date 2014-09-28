using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Maize
{
   public class MazeNode
    {
        public int x_loc { get; set; }
        public int y_loc { get; set; }
        public int z_loc { get; set; }

        public MazeNode x_plus { get; set; }
        public MazeNode x_minus { get; set; }
        public MazeNode y_plus { get; set; }
        public MazeNode y_minus { get; set; }
        public MazeNode z_plus { get; set; }
        public MazeNode z_minus { get; set; }

        public MazeNode parent { get; set; }

        public MazeNode(int x, int y, int z)
        {
            this.x_loc = x;
            this.y_loc = y;
            this.z_loc = z;

            parent = this;
        }

        /*public bool isConnected(MazeNode other)
        {

        }*/

        public void connect(MazeNode other)
        {
            if (other.x_loc - this.x_loc == 1)
            {
                pairwiseAddXPlus(other);
            }

            else if (this.x_loc - other.x_loc == 1)
            {
                pairwiseAddXMinus(other);
            }

            else if (other.y_loc - this.y_loc == 1)
            {
                pairwiseAddYPlus(other);
            }

            else if (this.y_loc - other.y_loc == 1)
            {
                pairwiseAddYMinus(other);
            }

            else if (other.z_loc - this.z_loc == 1)
            {
                pairwiseAddZPlus(other);
            }

            else if (this.z_loc - other.z_loc == 1)
            {
                pairwiseAddZMinus(other);
            }

            else
            {
                Console.WriteLine("Error: Nodes not adjacent!\n" + "This: " + x_loc + ", " + y_loc + ", " + z_loc + "\n"
                    + "Other: " + other.x_loc + ", " + other.y_loc + ", " + other.z_loc + "\n");
            }

            merge(other);
        }

        private void pairwiseAddXPlus(MazeNode other)
        {
            x_plus = other;
            other.x_minus = this;
        }

        private void pairwiseAddXMinus(MazeNode other)
        {
            x_minus = other;
            other.x_plus = this;
        }

        private void pairwiseAddYPlus(MazeNode other)
        {
            y_plus = other;
            other.y_minus = this;
        }

        private void pairwiseAddYMinus(MazeNode other)
        {
            y_minus = other;
            other.y_plus = this;
        }

        private void pairwiseAddZPlus(MazeNode other)
        {
            z_plus = other;
            other.z_minus = this;
        }

        private void pairwiseAddZMinus(MazeNode other)
        {
            z_minus = other;
            other.z_plus = this;
        }

        public ArrayList getAdjacentEdges()
        {
            ArrayList adjacentEdges = new ArrayList();

            if (x_plus != null)
                adjacentEdges.Add(x_plus);

            if (y_plus != null)
                adjacentEdges.Add(y_plus);

            if (z_plus != null)
                adjacentEdges.Add(z_plus);

            if (x_minus != null)
                adjacentEdges.Add(x_minus);

            if (y_minus != null)
                adjacentEdges.Add(y_minus);

            if (z_minus != null)
                adjacentEdges.Add(z_minus);

            return adjacentEdges;
        }

        public void printLocation()
        {
            Console.WriteLine("Node Location: " + x_loc + ", " + y_loc + ", " + z_loc + "\n");
        }

        public MazeNode find()
        {
            if (parent == this)
            {
                return this;
            }

            return parent.find();
        }

        public void merge(MazeNode other)
        {
            MazeNode my_root = find();
            MazeNode their_root = other.find();

            my_root.parent = their_root;
        }

        public bool isConnected(MazeNode other)
        {
            //Console.Write("me: ");
            //find().printLocation();
            //Console.Write("them: ");
            //other.find().printLocation();
            return find() == other.find();
        }
    }
}
