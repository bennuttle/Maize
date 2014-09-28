using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Maize
{
   public class MazeGraph
    {
		public MazeNode[, ,] graph { get; set; }
        ArrayList forest;
        ArrayList edges;

        public MazeGraph(int size)
        {
            //Allocate space for graph
            this.graph = new MazeNode[size, size, size];

            //Set of nodes for Kruskal's algorithm
            forest = new ArrayList();
            //forest = new MazeNode[(int)Math.Pow(size, 3)];

            //Set of edges for Kruskal's algorithm
            edges = new ArrayList();

            //Initializa all the nodes, giving XYZ coordinates
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        //Create the nodes
                        graph[x, y, z] = new MazeNode(x, y, z);

                        //Store them in temporary 'forest' for Kruskal's algorithm
                        //Each node is a singleton tree at this point
                        forest.Add(graph[x, y, z]);

                        //Add edges to a set for consideration. Nodes are not yet connected.
                        /*
                        if(x > 0)
                        {
                            edges.Add(new MazeEdge(graph[x, y, z], graph[x - 1, y, z]));
                        }

                        if (y > 0)
                        {
                            edges.Add(new MazeEdge(graph[x, y, z], graph[x, y - 1, z]));
                        }

                        if (z > 0)
                        {
                            edges.Add(new MazeEdge(graph[x, y, z], graph[x, y, z - 1]));
                        }*/
                    }
                }
            }
            generateEdges(size);
            generateMaze(size);
            //BreadthFirstSearch(graph[0, 0, 0]);
            Console.ReadLine();
        }

        //Connect nodes to form a MST over the graph
        //A MST is our maze, with guaranteed access to every node and no loops.
        private void generateMaze(int size)
        {
            Random rand = new Random();
            int edgeIndex;
            MazeEdge currentEdge;
            int connections = 0;
            while (connections < Math.Pow(size, 3) - 1)
            {
                //Select an edge
                edgeIndex = rand.Next(edges.Count);
                currentEdge = (MazeEdge)edges[edgeIndex];

                if (!(currentEdge.joinedNodes[0].isConnected(currentEdge.joinedNodes[1])))
                {
                    //join the two nodes
                    currentEdge.joinedNodes[0].connect(currentEdge.joinedNodes[1]);

                    //Remove one of the two now-connected nodes from our forest
                    forest.Remove(currentEdge.joinedNodes[0]);
                    connections++;
                }

                //Remove the edge we selected from consideration
                edges.Remove(currentEdge);
            }
        }

        private void generateEdges(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        //Add edges to a set for consideration. Nodes are not yet connected.
                        if (x > 0)
                        {
                            edges.Add(new MazeEdge(graph[x, y, z], graph[x - 1, y, z]));
                        }

                        if (y > 0)
                        {
                            edges.Add(new MazeEdge(graph[x, y, z], graph[x, y - 1, z]));
                        }

                        if (z > 0)
                        {
                            edges.Add(new MazeEdge(graph[x, y, z], graph[x, y, z - 1]));
                        }
                    }
                }
            }
        }

        private bool isSingleTree()
        {
           // return forest.Count == 1;
            return forest.Count == 1;
           /* int treeCount = 0;

            foreach (MazeNode tree in forest)
            {
                    treeCount++;
            }

            return treeCount == 1;*/
        }

        private void BreadthFirstSearch(MazeNode root)
        {
            Queue nodes = new Queue();
            HashSet<MazeNode> vertices = new HashSet<MazeNode>();
            ArrayList children;

            vertices.Add(root);
            nodes.Enqueue(root);

            MazeNode current;

            while (nodes.Count > 0)
            {
                current = (MazeNode)nodes.Dequeue();
                children = current.getAdjacentEdges();

                foreach (MazeNode child in children)
                {
                    if(!vertices.Contains(child))
                    {
                        vertices.Add(child);
                        nodes.Enqueue(child);
                    }
                }
            }

            foreach (MazeNode visit in vertices)
            {
                visit.printLocation();
            }
        }
    }
}
